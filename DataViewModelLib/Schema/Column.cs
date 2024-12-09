using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataViewModelLib.Schema
{
	public class Column : SchemaObject
	{
			
		public string ColumnName { get; private set; }
		public string TypeName { get; private set; }
		public bool IsNullable { get; private set; }
		public Table TableModel { get; private set; }

		public Column(Table TableModel,string ColumnName, string TypeName, bool IsNullable) : base()
		{
			this.TableModel = TableModel;
			this.ColumnName = ColumnName;
			this.TypeName = TypeName;
			this.IsNullable = IsNullable;
		}
		public string GenerateTableModelProperties()
		{
			string source =
			$$"""
			public {{TypeName}} {{ColumnName}} 
			{
				get => dataSource.{{ColumnName}};
				set {{{TypeName}} oldValue=value; dataSource.{{ColumnName}} = value; databaseModel.Notify{{TableModel.TableName}}RowChanged(dataSource,nameof({{ColumnName}}), oldValue,value ); }
			}
			""";

			return source;
		}
		


	}
}
