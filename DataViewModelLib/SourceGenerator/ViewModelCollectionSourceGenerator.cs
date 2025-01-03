﻿using DataModelLib.Common;
using DataModelLib.Common.Schema;
using DataModelLib.Common.SourceGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataViewModelLib.SourceGenerator
{
	public class ViewModelCollectionSourceGenerator : SourceGenerator<Table>
	{
		public override string GenerateSource(Table Table)
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
			using {{Table.Namespace}}.Models;			
										
			namespace {{Table.Namespace}}.ViewModels
			{
				public partial class {{Table.TableName}}ViewModelCollection : IViewModelCollection, IEnumerable<{{Table.TableName}}ViewModel>, INotifyCollectionChanged
				{
					#nullable enable
					public event NotifyCollectionChangedEventHandler? CollectionChanged;
					#nullable disable
						
					private {{Table.DatabaseName}}Model databaseModel;
					private {{Table.DatabaseName}}ViewModel databaseViewModel;
			
					private List<{{Table.TableName}}ViewModel> items;
								
					public {{Table.TableName}}ViewModelCollection({{Table.DatabaseName}}ViewModel DatabaseViewModel, {{Table.DatabaseName}}Model DatabaseModel)
					{
						this.databaseModel=DatabaseModel; 
						this.databaseViewModel=DatabaseViewModel; 
			
						this.items=new List<{{Table.TableName}}ViewModel>();
						this.items.AddRange( databaseModel.Get{{Table.TableName}}Table().Select( item => databaseViewModel.Create{{Table.TableName}}ViewModel(item) ));
			
						this.databaseModel.{{Table.TableName}}TableChanged += On{{Table.TableName}}TableChanged;
					}

					protected virtual void On{{Table.TableName}}TableChanged({{Table.TableName}} Item,TableChangedActions Action, int Index)
					{
						{{Table.TableName}}ViewModel item;
						
						switch(Action)
						{
							case TableChangedActions.Remove:
								item=items[Index];		
								items.RemoveAt(Index);
								OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, Index));
								break;
							case TableChangedActions.Add:
								item = databaseViewModel.Create{{Table.TableName}}ViewModel(databaseModel.Create{{Table.TableName}}Model(Item));
								items.Insert(Index,item);
								OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, Index));
								break;
							default:
								break;
						}
					}

					#region IEnumerable
					public IEnumerator<{{Table.TableName}}ViewModel> GetEnumerator()
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
			
					public void Remove({{Table.TableName}}ViewModel Item)
					{
						Item.Delete();
					}

					public void Add({{Table.TableName}} Item)
					{
						databaseModel.Add{{Table.TableName}}(Item);
					}

					void IViewModelCollection.Add(object Item)
					{
						#nullable enable
						{{Table.TableName}}? convertedItem;
						#nullable disable
			
						convertedItem = Item as {{Table.TableName}};
						if (convertedItem==null) return;
						Add(convertedItem);
					}

				}
			}
			""";

			return source;
		}

	}
}