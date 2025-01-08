﻿using DataModelLib.Common;
using DataModelLib.Common.Schema;
using DataModelLib.Common.SourceGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataViewModelLib.SourceGenerator
{
	public class RelationViewModelCollectionSourceGenerator : SourceGenerator<Relation>
	{
		public override string GenerateSource(Relation Relation)
		{
			string source =
			$$"""
			// <auto-generated/>
			using System;
			using System.Collections;
			using System.Collections.Generic;
			using System.Collections.Specialized;
			using System.Linq;
			using System.ComponentModel;
			using System.Windows;
			using DataModelLib.Common;
			using {{Relation.PrimaryTable.Namespace}}.Models;			
										
			namespace {{Relation.PrimaryTable.Namespace}}.ViewModels
			{
				public partial class {{Relation.PrimaryPropertyName}}ViewModelCollection : IViewModelCollection, IAddViewModelCollection, IEnumerable<{{Relation.ForeignTable.TableName}}ViewModel>, INotifyPropertyChanged, INotifyCollectionChanged
				{
					#nullable enable
					public event PropertyChangedEventHandler? PropertyChanged;
					public event NotifyCollectionChangedEventHandler? CollectionChanged;

					private {{Relation.ForeignTable.TableName}}ViewModel? selectedItem;
					public {{Relation.ForeignTable.TableName}}ViewModel? SelectedItem
					{
						get => selectedItem;
						set { selectedItem=value; OnPropertyChanged("SelectedItem"); }
					}
					#nullable disable
						
					private {{Relation.PrimaryTable.DatabaseName}}Model databaseModel;
					private {{Relation.PrimaryTable.DatabaseName}}ViewModel databaseViewModel;
					private {{Relation.PrimaryTable.TableName}}Model primaryRow;

					private List<{{Relation.ForeignTable.TableName}}ViewModel> items;
					public int Count
					{
						get => items.Count;
					}

					public {{Relation.PrimaryPropertyName}}ViewModelCollection({{Relation.PrimaryTable.DatabaseName}}ViewModel DatabaseViewModel, {{Relation.PrimaryTable.DatabaseName}}Model DatabaseModel, {{Relation.PrimaryTable.TableName}}Model PrimaryRow)
					{
						this.databaseModel=DatabaseModel; 
						this.databaseViewModel=DatabaseViewModel;
						this.primaryRow=PrimaryRow;
			
						this.items=new List<{{Relation.ForeignTable.TableName}}ViewModel>();
						this.items.AddRange( PrimaryRow.Get{{Relation.PrimaryPropertyName}}().Select( item => databaseViewModel.Create{{Relation.ForeignTable.TableName}}ViewModel(item) ));
						
						this.primaryRow.{{Relation.PrimaryPropertyName}}Changed += On{{Relation.PrimaryPropertyName}}Changed;

					}

					protected virtual void OnPropertyChanged(string PropertyName)
					{
						if (PropertyChanged!=null) PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
					}

					#region IEnumerable
					public IEnumerator<{{Relation.ForeignTable.TableName}}ViewModel> GetEnumerator()
					{
						return items.GetEnumerator();
					}
					IEnumerator IEnumerable.GetEnumerator()
					{
						return items.GetEnumerator();
					}
					#endregion
					
					#region INotifyCollectionChanged
					protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
					{
						if (CollectionChanged != null) CollectionChanged(this, e);
					}
					#endregion
			
					private void On{{Relation.PrimaryPropertyName}}Changed({{Relation.ForeignTable.TableName}} Item,TableChangedActions Action, int Index)
					{
						{{Relation.ForeignTable.TableName}}ViewModel item;

						switch(Action)
						{
							case TableChangedActions.Remove:
								item=items[Index];		
								items.RemoveAt(Index);
								OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, Index));
								break;
							case TableChangedActions.Add:
								item = databaseViewModel.Create{{Relation.ForeignTable.TableName}}ViewModel(databaseModel.Create{{Relation.ForeignTable.TableName}}Model(Item));
								items.Insert(Index,item);
								OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, Index));
								break;
							default:
								break;
						}
					}

					public void Add({{Relation.ForeignTable.TableName}} Item)
					{
						Item.{{Relation.ForeignKey.ColumnName}} = primaryRow.{{Relation.PrimaryKey.ColumnName}};
						databaseModel.Add{{Relation.ForeignTable.TableName}}(Item);
					}
			
					void IAddViewModelCollection.Add(object Item)
					{
						#nullable enable
						{{Relation.ForeignTable.TableName}}? convertedItem;
						#nullable disable
			
						convertedItem = Item as {{Relation.ForeignTable.TableName}};
						if (convertedItem==null) return;
						Add(convertedItem);
					}
			{{GenerateRemoveMethod(Relation).Indent(2)}}

				}
			}
			""";

			return source;
		}

		public string GenerateRemoveMethod(Relation Relation)
		{
			string source;
			
			if (!Relation.ForeignKey.IsNullable) return ""; // cannot generate remove method if foreign key is not nullable
			
			source =
			$$"""
			public void Remove({{Relation.ForeignTable.TableName}}ViewModel Item)
			{
				Item.{{Relation.ForeignKey.ColumnName}} = null;
			}
			""";

			return source;
		}

	}
}
