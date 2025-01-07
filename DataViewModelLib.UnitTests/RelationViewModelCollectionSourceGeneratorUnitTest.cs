using DataModelLib.Common.Schema;
using DataViewModelLib.SourceGenerator;
using System.Reflection;

namespace DataViewModelLib.UnitTests
{
	[TestClass]
	public class RelationViewModelCollectionSourceGeneratorUnitTest
	{
		[TestMethod]
		public void ShouldGenerateUsings()
		{
			RelationViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table foreignTable,primaryTable;
			Column foreignColumn, primaryColumn;
			Relation relation;
			string source;

			sourceGenerator = new RelationViewModelCollectionSourceGenerator();

			database =  new Database("ns", "MyDB");

			foreignTable = new Table("ns", database.DatabaseName, "Child");
			foreignColumn = new Column(foreignTable, "ParentID", "byte", false);
			foreignTable.Columns.Add(foreignColumn);	
			database.Tables.Add(foreignTable);

			primaryTable = new Table("ns", database.DatabaseName, "Parent");
			primaryColumn = new Column(primaryTable, "ParentID", "byte", false);
			primaryTable.Columns.Add(primaryColumn);
			database.Tables.Add(primaryTable);

			relation = new Relation("Childs", primaryColumn, "Parent", foreignColumn, CascadeTriggers.None);
			primaryTable.Relations.Add(relation);
			foreignTable.Relations.Add(relation);
			
			source = sourceGenerator.GenerateSource(relation);

			Assert.IsTrue(source.Contains("using System.Windows;"));
			Assert.IsTrue(source.Contains("using DataModelLib.Common;"));
			Assert.IsTrue(source.Contains("using ns.Models;"));

		}

		[TestMethod]
		public void ShouldGenerateClass()
		{
			RelationViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table foreignTable, primaryTable;
			Column foreignColumn, primaryColumn;
			Relation relation;
			string source;

			sourceGenerator = new RelationViewModelCollectionSourceGenerator();

			database = new Database("ns", "MyDB");

			foreignTable = new Table("ns", database.DatabaseName, "Child");
			foreignColumn = new Column(foreignTable, "ParentID", "byte", false);
			foreignTable.Columns.Add(foreignColumn);
			database.Tables.Add(foreignTable);

			primaryTable = new Table("ns", database.DatabaseName, "Parent");
			primaryColumn = new Column(primaryTable, "ParentID", "byte", false);
			primaryTable.Columns.Add(primaryColumn);
			database.Tables.Add(primaryTable);

			relation = new Relation("Childs", primaryColumn, "Parent", foreignColumn, CascadeTriggers.None);
			primaryTable.Relations.Add(relation);
			foreignTable.Relations.Add(relation);

			source = sourceGenerator.GenerateSource(relation);

			Assert.IsTrue(source.Contains("namespace ns.ViewModels"));
			Assert.IsTrue(source.Contains("public partial class ChildsViewModelCollection : IViewModelCollection, IEnumerable<ChildViewModel>, INotifyPropertyChanged, INotifyCollectionChanged"));

		}

		[TestMethod]
		public void ShouldGenerateProperties()
		{
			RelationViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table foreignTable, primaryTable;
			Column foreignColumn, primaryColumn;
			Relation relation;
			string source;

			sourceGenerator = new RelationViewModelCollectionSourceGenerator();

			database = new Database("ns", "MyDB");

			foreignTable = new Table("ns", database.DatabaseName, "Child");
			foreignColumn = new Column(foreignTable, "ParentID", "byte", false);
			foreignTable.Columns.Add(foreignColumn);
			database.Tables.Add(foreignTable);

			primaryTable = new Table("ns", database.DatabaseName, "Parent");
			primaryColumn = new Column(primaryTable, "ParentID", "byte", false);
			primaryTable.Columns.Add(primaryColumn);
			database.Tables.Add(primaryTable);

			relation = new Relation("Childs", primaryColumn, "Parent", foreignColumn, CascadeTriggers.None);
			primaryTable.Relations.Add(relation);
			foreignTable.Relations.Add(relation);

			source = sourceGenerator.GenerateSource(relation);

			Assert.IsTrue(source.Contains("public ChildViewModel? SelectedItem"));
		}


		[TestMethod]
		public void ShouldGenerateConstructor()
		{
			RelationViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table foreignTable, primaryTable;
			Column foreignColumn, primaryColumn;
			Relation relation;
			string source;

			sourceGenerator = new RelationViewModelCollectionSourceGenerator();

			database = new Database("ns", "MyDB");

			foreignTable = new Table("ns", database.DatabaseName, "Child");
			foreignColumn = new Column(foreignTable, "ParentID", "byte", false);
			foreignTable.Columns.Add(foreignColumn);
			database.Tables.Add(foreignTable);

			primaryTable = new Table("ns", database.DatabaseName, "Parent");
			primaryColumn = new Column(primaryTable, "ParentID", "byte", false);
			primaryTable.Columns.Add(primaryColumn);
			database.Tables.Add(primaryTable);

			relation = new Relation("Childs", primaryColumn, "Parent", foreignColumn, CascadeTriggers.None);
			primaryTable.Relations.Add(relation);
			foreignTable.Relations.Add(relation);

			source = sourceGenerator.GenerateSource(relation);

			Assert.IsTrue(source.Contains("public ChildsViewModelCollection(MyDBViewModel DatabaseViewModel, MyDBModel DatabaseModel, ParentModel PrimaryRow)"));

		}

		[TestMethod]
		public void ShouldGenerateIEnumerableMethods()
		{
			RelationViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table foreignTable, primaryTable;
			Column foreignColumn, primaryColumn;
			Relation relation;
			string source;

			sourceGenerator = new RelationViewModelCollectionSourceGenerator();

			database = new Database("ns", "MyDB");

			foreignTable = new Table("ns", database.DatabaseName, "Child");
			foreignColumn = new Column(foreignTable, "ParentID", "byte", false);
			foreignTable.Columns.Add(foreignColumn);
			database.Tables.Add(foreignTable);

			primaryTable = new Table("ns", database.DatabaseName, "Parent");
			primaryColumn = new Column(primaryTable, "ParentID", "byte", false);
			primaryTable.Columns.Add(primaryColumn);
			database.Tables.Add(primaryTable);

			relation = new Relation("Childs", primaryColumn, "Parent", foreignColumn, CascadeTriggers.None);
			primaryTable.Relations.Add(relation);
			foreignTable.Relations.Add(relation);

			source = sourceGenerator.GenerateSource(relation);

			Assert.IsTrue(source.Contains("public IEnumerator<ChildViewModel> GetEnumerator()"));
			//Assert.IsTrue(source.Contains("public void Remove(PersonnViewModel Item)"));
			Assert.IsTrue(source.Contains("public void Add(Child Item)"));
			Assert.IsTrue(source.Contains("void IViewModelCollection.Add(object Item)"));

		}

		[TestMethod]
		public void ShouldGenerateINotifyCollectionChangedMethods()
		{
			RelationViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table foreignTable, primaryTable;
			Column foreignColumn, primaryColumn;
			Relation relation;
			string source;

			sourceGenerator = new RelationViewModelCollectionSourceGenerator();

			database = new Database("ns", "MyDB");

			foreignTable = new Table("ns", database.DatabaseName, "Child");
			foreignColumn = new Column(foreignTable, "ParentID", "byte", false);
			foreignTable.Columns.Add(foreignColumn);
			database.Tables.Add(foreignTable);

			primaryTable = new Table("ns", database.DatabaseName, "Parent");
			primaryColumn = new Column(primaryTable, "ParentID", "byte", false);
			primaryTable.Columns.Add(primaryColumn);
			database.Tables.Add(primaryTable);

			relation = new Relation("Childs", primaryColumn, "Parent", foreignColumn, CascadeTriggers.None);
			primaryTable.Relations.Add(relation);
			foreignTable.Relations.Add(relation);

			source = sourceGenerator.GenerateSource(relation);

			Assert.IsTrue(source.Contains("protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)"));

		}




	}
}