using DataViewModelLib.Schema;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace DataViewModelLib
{

	
	[Generator]
	public class DataModelGenerator : IIncrementalGenerator
	{
		
		private const string Namespace = "DataModelGenerator";

		

		public void Initialize(IncrementalGeneratorInitializationContext context)
		{
			/*context.RegisterPostInitializationOutput(ctx => ctx.AddSource("Delegates/TableChangedDelegate.g.cs", SourceText.From(TableChangedEventHandlerSourceCode, Encoding.UTF8)));
			context.RegisterPostInitializationOutput(ctx => ctx.AddSource("Attributes/DatabaseAttribute.g.cs", SourceText.From(DatabaseAttributeSourceCode, Encoding.UTF8)));
			context.RegisterPostInitializationOutput(ctx => ctx.AddSource("Attributes/TableAttribute.g.cs", SourceText.From(TableAttributeSourceCode, Encoding.UTF8)));
			context.RegisterPostInitializationOutput(ctx => ctx.AddSource("Attributes/ColumnAttribute.g.cs", SourceText.From(ColumnAttributeSourceCode, Encoding.UTF8)));
			context.RegisterPostInitializationOutput(ctx => ctx.AddSource("Attributes/PrimaryKeyAttribute.g.cs", SourceText.From(PrimaryKeyAttributeSourceCode, Encoding.UTF8)));
			context.RegisterPostInitializationOutput(ctx => ctx.AddSource("Attributes/ForeignKeyAttribute.g.cs", SourceText.From(ForeignKeyAttributeSourceCode, Encoding.UTF8)));*/

			IncrementalValuesProvider<(SyntaxNode,DataModelType)> syntaxNodeProvider = context.SyntaxProvider.CreateSyntaxProvider
			(
				(syntaxNode,cancellationToken) => (syntaxNode is ClassDeclarationSyntax classDeclatationSyntax) && classDeclatationSyntax.AttributeLists.Count > 0,
				transform: static (ctx, _) => GetDeclarationForSourceGen(ctx)
			)
			.Where(classDeclarationSyntax=>classDeclarationSyntax.DataModelType!=DataModelType.Undefined)
			.Select((t, _) => t);


			context.RegisterSourceOutput
			(
				context.CompilationProvider.Combine(syntaxNodeProvider.Collect()),
				(ctx, t) => GenerateCode(ctx, t.Left,t.Right)
			);

			
		}
	
		private static (SyntaxNode Node, DataModelType DataModelType) GetDeclarationForSourceGen(GeneratorSyntaxContext context)
		{
			SyntaxNode currentNode = context.Node;

			if (currentNode.ContainsAttribute(context.SemanticModel, $"{Namespace}.TableAttribute")) return (currentNode, DataModelType.Table);
			if (currentNode.ContainsAttribute(context.SemanticModel, $"{Namespace}.DatabaseAttribute")) return (currentNode, DataModelType.Database);

			return (currentNode, DataModelType.Undefined);
		}

		private static Database? CreateDatabaseModel(SourceProductionContext context, Compilation compilation, TypeDeclarationSyntax? DatabaseDeclarationSyntax)
		{
			INamedTypeSymbol? databaseSymbol;
			string nameSpace;
			string databaseClassName;

			// no database defined, cannot proceed
			if (DatabaseDeclarationSyntax == null) return null;

			// On récupère le modèle sémantique pour pouvoir manipuler les méta données et le contenu de nos objets 
			databaseSymbol = DatabaseDeclarationSyntax.GetTypeSymbol<INamedTypeSymbol>(compilation);
			if (databaseSymbol == null) return null;

			// On récupère le namespace, le nom du noeud courant et on créé le nom du futur DTO
			nameSpace = databaseSymbol.ContainingNamespace.ToDisplayString();
			databaseClassName = DatabaseDeclarationSyntax.Identifier.Text;

			return new Database(nameSpace, databaseClassName);
		}
		private static void CreateTableModels(SourceProductionContext context, Compilation compilation, Database Database, IEnumerable<SyntaxNode> TableDeclarationSyntaxList)
		{
			string columnName;
			string columnType;
			string tableName;
			string nameSpace;
			Table tableModel;
			INamedTypeSymbol? tableSymbol;
			IPropertySymbol? propertySymbol;
			bool isNullable;
			Column columnModel;
			//AttributeData? tableAttributeData;

			// on enumère une première fois les tables et les colonnes pour les ajouter à la collection
			foreach (TypeDeclarationSyntax tableDeclarationSyntax in TableDeclarationSyntaxList)
			{
				// On récupère le modèle sémantique pour pouvoir manipuler les méta données et le contenu de nos objets 
				tableSymbol = tableDeclarationSyntax.GetTypeSymbol<INamedTypeSymbol>(compilation);
				if (tableSymbol == null) continue;

				// On récupère le namespace, le nom du noeud courant et on créé le nom du futur DTO
				nameSpace = tableSymbol.ContainingNamespace.ToDisplayString();
				tableName = tableDeclarationSyntax.Identifier.Text;

				//tableAttributeData = tableSymbol.GetAttribute($"{Namespace}.TableAttribute");
				//if ((tableAttributeData == null) || (tableAttributeData.ConstructorArguments.Length==0)) tableName = $"{tableClassName}s";
				//else tableName = tableAttributeData.ConstructorArguments[0].Value?.ToString() ?? $"{tableClassName}s";

				tableModel = new Table(nameSpace, Database.DatabaseName,  tableName);

				// on recherche les colonnes pour les ajouter à la table
				foreach (PropertyDeclarationSyntax propertyDeclarationSyntax in tableDeclarationSyntax.ChildNodes().OfType<PropertyDeclarationSyntax>())
				{
					propertySymbol = propertyDeclarationSyntax.GetTypeSymbol<IPropertySymbol>(compilation);
					if (propertySymbol == null) continue;

					if (!propertyDeclarationSyntax.ContainsAttribute(compilation, $"{Namespace}.ColumnAttribute")) continue;

					columnName = propertyDeclarationSyntax.Identifier.Text;
					columnType = propertyDeclarationSyntax.Type.ToString();
					isNullable = propertyDeclarationSyntax.Type is NullableTypeSyntax;

					columnModel = new Column(tableModel, columnName, columnType, isNullable);
					tableModel.Columns.Add(columnModel);

					if (propertyDeclarationSyntax.ContainsAttribute(compilation, $"{Namespace}.PrimaryKeyAttribute")) tableModel.PrimaryKey=columnModel;

				}
				Database.Tables.Add(tableModel);

			}
		}
		private static void CreateRelationModels(SourceProductionContext context, Compilation compilation, Database Database, IEnumerable<SyntaxNode> TableDeclarationSyntaxList)
		{
			string foreignColumnName;
			string foreignTableName;
			string nameSpace;
			string? primaryPropertyName,foreignPropertyName, primaryTableName, primaryColumnName,cascadeActionName;

			CascadeTriggers cascadeTrigger;

			Table? foreignTable,primaryTable;
			Column? foreignColumn,primaryColumn;

			INamedTypeSymbol? tableSymbol;
			IPropertySymbol? propertySymbol;
			//bool isNullable;
			Relation relation;
			//AttributeData? tableAttributeData;
			AttributeData? relationAttributeData;
			

			// on enumère une première fois les tables et les colonnes pour les ajouter à la collection
			foreach (TypeDeclarationSyntax tableDeclarationSyntax in TableDeclarationSyntaxList)
			{
				// On récupère le modèle sémantique pour pouvoir manipuler les méta données et le contenu de nos objets 
				tableSymbol = tableDeclarationSyntax.GetTypeSymbol<INamedTypeSymbol>(compilation);
				if (tableSymbol == null) continue;

				// On récupère le namespace, le nom du noeud courant et on créé le nom du futur DTO
				nameSpace = tableSymbol.ContainingNamespace.ToDisplayString();
				foreignTableName = tableDeclarationSyntax.Identifier.Text;
				//tableAttributeData = tableSymbol.GetAttribute($"{Namespace}.TableAttribute");
				//if ((tableAttributeData == null) || (tableAttributeData.ConstructorArguments.Length == 0)) foreignTableName = $"{foreignTableClassName}s";
				//else foreignTableName = tableAttributeData.ConstructorArguments[0].Value?.ToString() ?? $"{foreignTableClassName}s";

				foreignTable = Database.Tables.FirstOrDefault(item => item.TableName == foreignTableName);
				if (foreignTable == null) continue;	

				// on recherche les relations pour les ajouter à la table
				foreach (PropertyDeclarationSyntax propertyDeclarationSyntax in tableDeclarationSyntax.ChildNodes().OfType<PropertyDeclarationSyntax>())
				{
					propertySymbol = propertyDeclarationSyntax.GetTypeSymbol<IPropertySymbol>(compilation);
					if (propertySymbol == null) continue;
					foreignColumnName = propertyDeclarationSyntax.Identifier.Text;

					foreignColumn = foreignTable.Columns.FirstOrDefault(item => item.ColumnName == foreignColumnName);
					if (foreignColumn == null) continue;

					relationAttributeData = propertySymbol.GetAttribute($"{Namespace}.ForeignKeyAttribute");
					if ((relationAttributeData == null) || (relationAttributeData.ConstructorArguments.Length<5)) continue;

					foreignPropertyName = relationAttributeData.ConstructorArguments[0].Value?.ToString();
					if (foreignPropertyName == null) continue;

					primaryPropertyName = relationAttributeData.ConstructorArguments[1].Value?.ToString();
					if (primaryPropertyName == null) continue;

					primaryTableName = relationAttributeData.ConstructorArguments[2].Value?.ToString();
					if (primaryTableName == null) continue;

					primaryColumnName = relationAttributeData.ConstructorArguments[3].Value?.ToString();
					if (primaryColumnName == null) continue;
					
					cascadeActionName = relationAttributeData.ConstructorArguments[4].Value?.ToString();
					if (cascadeActionName == null) continue;
					if (!Enum.TryParse<CascadeTriggers>(cascadeActionName, out cascadeTrigger)) continue;


					primaryTable = Database.Tables.FirstOrDefault(item => item.TableName == primaryTableName);
					if (primaryTable == null) continue;

					primaryColumn = primaryTable.Columns.FirstOrDefault(item => item.ColumnName == primaryColumnName);
					if (primaryColumn == null) continue;

				
					relation = new Relation(primaryPropertyName,  primaryColumn, foreignPropertyName,  foreignColumn, cascadeTrigger);
					foreignTable.Relations.Add(relation);
					primaryTable.Relations.Add(relation);

					//columnType = propertyDeclarationSyntax.Type.ToString();
					//isNullable = propertyDeclarationSyntax.Type is NullableTypeSyntax;

					
				}

			}
		}

		private static void GenerateCode(SourceProductionContext context, Compilation compilation,  IEnumerable<(SyntaxNode,DataModelType)> Declarations)
		{
			Database? database;
			string source;

			database = CreateDatabaseModel(context, compilation, Declarations.FirstOrDefault(item => item.Item2 == DataModelType.Database).Item1 as TypeDeclarationSyntax);
			if (database == null) return;

			CreateTableModels(context, compilation, database, Declarations.Where(item => item.Item2 == DataModelType.Table).Select(item => item.Item1));
			CreateRelationModels(context, compilation, database, Declarations.Where(item => item.Item2 == DataModelType.Table).Select(item => item.Item1));
			
			source = database.GenerateDatabaseViewModelClass();
			context.AddSource($"ViewModels/{database.DatabaseName}ViewModel.g.cs", SourceText.From(source, Encoding.UTF8));

			/*
			// On ajoute le code source des tables
			for (int i = 0; i < database.Tables.Count; i++)
			{
				Table table = database.Tables[i];
				source = table.GenerateTableModelClass();
				context.AddSource($"ViewModels/{table.TableName}ViewModel.g.cs", SourceText.From(source, Encoding.UTF8));
			}
			*/
		}








	}
}