using DataModelLib.Common;
using DataModelLib.Common.Schema;
using DataViewModelLib.SourceGenerator;
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
	public class CodeGenerator : BaseCodeGenerator
	{


		protected override void OnRegisterStaticSources(IncrementalGeneratorInitializationContext context)
		{
			// nothing to register
		}

		protected override void OnGenerateDynamicSources(SourceProductionContext context, Database database)
		{
			string source;
			DatabaseViewModelSourceGenerator databaseSourceViewModelGenerator;
			TableViewModelSourceGenerator tableViewModelSourceGenerator;
			TableViewModelCollectionSourceGenerator tableViewModelCollectionSourceGenerator;

			databaseSourceViewModelGenerator = new DatabaseViewModelSourceGenerator();	
			tableViewModelSourceGenerator = new TableViewModelSourceGenerator();
			tableViewModelCollectionSourceGenerator=new TableViewModelCollectionSourceGenerator();

			source = databaseSourceViewModelGenerator.GenerateSource(database);
			context.AddSource($"ViewModels/{database.DatabaseName}ViewModel.g.cs", SourceText.From(source, Encoding.UTF8));

			
			// On ajoute le code source des tables
			for (int i = 0; i < database.Tables.Count; i++)
			{
				Table table = database.Tables[i];
				source = tableViewModelSourceGenerator.GenerateSource(table);
				context.AddSource($"ViewModels/{table.TableName}ViewModel.g.cs", SourceText.From(source, Encoding.UTF8));
				source = tableViewModelCollectionSourceGenerator.GenerateSource(table);
				context.AddSource($"ViewModels/{table.TableName}ViewModelCollection.g.cs", SourceText.From(source, Encoding.UTF8));
			}
			


		}








	}
}