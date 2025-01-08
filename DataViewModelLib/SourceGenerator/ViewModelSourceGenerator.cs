﻿using DataModelLib.Common;
using DataModelLib.Common.Schema;
using DataModelLib.Common.SourceGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataViewModelLib.SourceGenerator
{
	public class ViewModelSourceGenerator : SourceGenerator<Table>
	{
		public override string GenerateSource(Table Table)
		{
			string source =
			$$"""
			// <auto-generated/>
			using System;
			using System.Collections.Generic;
			using System.Linq;
			using System.ComponentModel;
			using System.Windows;
			using DataModelLib.Common;
			using {{Table.Namespace}}.Models;			
						
			namespace {{Table.Namespace}}.ViewModels
			{
				public partial class {{Table.TableName}}ViewModel : IViewModel, INotifyPropertyChanged
				{
					#nullable enable
					public event PropertyChangedEventHandler? PropertyChanged;
					#nullable disable
			
					private {{Table.TableName}}Model dataSource
					{
						get;
						set;
					}

					private List<IViewModelProperty> properties;
					public IEnumerable<IViewModelProperty> Properties
					{
						get => properties;
					}

					private {{Table.DatabaseName}}Model databaseModel;
					private {{Table.DatabaseName}}ViewModel databaseViewModel;
						
			{{Table.Columns.Select(item => GenerateProperty(item)).Join().Indent(2)}}
			{{Table.Relations.Select(item => GenerateProperty(Table, item)).Join().Indent(2)}}
			
					public {{Table.TableName}}ViewModel({{Table.DatabaseName}}ViewModel DatabaseViewModel, {{Table.DatabaseName}}Model DatabaseModel, {{Table.TableName}}Model DataSource)
					{
						this.databaseModel=DatabaseModel; 
						this.databaseViewModel=DatabaseViewModel; 
						this.dataSource=DataSource;
						this.properties=new List<IViewModelProperty>();
			{{Table.Columns.Select(item => GenerateAddViewModelProperty(item)).Join().Indent(3)}}
			
			{{Table.Relations.Where(item => item.PrimaryTable == Table).Select(item => $"this._{item.PrimaryPropertyName} = new {item.PrimaryPropertyName}ViewModelCollection(databaseViewModel, databaseModel, dataSource);").Join().Indent(3)}}
						
			{{Table.Relations.Where(item => item.ForeignTable == Table).Select(item => $"this.dataSource.{item.ForeignPropertyName}Changed += On{item.ForeignPropertyName}Changed;").Join().Indent(3)}}

						DataSource.PropertyChanged += OnModelPropertyChanged;
					}

			{{Table.Relations.Select(item => GenerateMethod(Table, item)).Join().Indent(2)}}
			
					private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
					{
						OnPropertyChanged(e.PropertyName);
					}

					protected virtual void OnPropertyChanged(string PropertyName)
					{
						if (PropertyChanged!=null) PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
					}
			
					public void Delete()
					{
						this.databaseModel.Remove{{Table.TableName}}(dataSource);
					}
					public override string ToString()
					{
						return dataSource.ToString();
					}
				}
			}
			""";

			return source;
		}
		private string GenerateAddViewModelProperty(Column Column)
		{
			string source =
			$$"""
			properties.Add( new ViewModelProperty<{{Column.TypeName}}>(nameof({{Column.ColumnName}}), () => this.{{Column.ColumnName}}, (val) => this.{{Column.ColumnName}}=val ) );
			""";
			return source;
		}

		private string GenerateProperty(Column Column)
		{
			string source =
			$$"""
			public {{Column.TypeName}} {{Column.ColumnName}}
			{
				get { return dataSource.{{Column.ColumnName}}; }
				set { dataSource.{{Column.ColumnName}} = value; }
			}
			""";
			return source;
		}
		private string GenerateProperty(Table Table, Relation Relation)
		{
			string source;
			if (Relation.ForeignTable == Table)
			{
				source =
				$$"""
				private {{Relation.PrimaryTable.TableName}}ViewModel _{{Relation.ForeignPropertyName}};
				public {{Relation.PrimaryTable.TableName}}ViewModel {{Relation.ForeignPropertyName}}
				{
					get 
					{
						if (this._{{Relation.ForeignPropertyName}}==null)
						{
							{{Relation.PrimaryTable.TableName}}Model model = dataSource.Get{{Relation.ForeignPropertyName}}();
							if (model==null) return null;
							this._{{Relation.ForeignPropertyName}} = databaseViewModel.Create{{Relation.PrimaryTable.TableName}}ViewModel(model);
						}
						return this._{{Relation.ForeignPropertyName}}; 
					}
				}
				""";
			}
			else
			{
				source =
				$$"""
				private {{Relation.PrimaryPropertyName}}ViewModelCollection _{{Relation.PrimaryPropertyName}};
				public {{Relation.PrimaryPropertyName}}ViewModelCollection {{Relation.PrimaryPropertyName}}
				{
					get { return this._{{Relation.PrimaryPropertyName}}; }
				}
				""";
			}
			return source;
		}

		private string GenerateMethod(Table Table, Relation Relation)
		{
			string source;
			if (Relation.PrimaryTable == Table) return "";

			source =
			$$"""
			private void On{{Relation.ForeignPropertyName}}Changed(object sender, EventArgs e)
			{
				this._{{Relation.ForeignPropertyName}} = null;
				OnPropertyChanged(nameof({{Relation.ForeignPropertyName}}));
			}
			""";
			return source;
		}



	}
}
