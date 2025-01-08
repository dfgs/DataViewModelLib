using DataModelLib.Common.Schema;
using DataViewModelLib.SourceGenerator;
using System.Reflection;

namespace DataViewModelLib.UnitTests
{
	[TestClass]
	public class ViewModelSourceGeneratorUnitTest
	{
		[TestMethod]
		public void ShouldGenerateUsings()
		{
			ViewModelSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new ViewModelSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns1", database.DatabaseName, "Personn1");
			database.Tables.Add(table);

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("using System.Windows;"));
			Assert.IsTrue(source.Contains("using DataModelLib.Common;"));
			Assert.IsTrue(source.Contains("using ns1.Models;"));
			

		}

		[TestMethod]
		public void ShouldGenerateClass()
		{
			ViewModelSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new ViewModelSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(table);

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("namespace ns.ViewModels"));
			Assert.IsTrue(source.Contains("public partial class PersonnViewModel : IViewModel, INotifyPropertyChanged"));

		}

		[TestMethod]
		public void ShouldGenerateConstructor()
		{
			ViewModelSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new ViewModelSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(table);

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("public PersonnViewModel(MyDBViewModel DatabaseViewModel, MyDBModel DatabaseModel, PersonnModel DataSource)"));

		}

		[TestMethod]
		public void ShouldGenerateMethods()
		{
			ViewModelSourceGenerator sourceGenerator;
			Database database;
			Table parentTable;
			Table childTable;
			Column column1, primaryColumn, foreignColumn;
			Relation relation;
			string source;

			sourceGenerator = new ViewModelSourceGenerator();

			database = new Database("ns", "MyDB");

			childTable = new Table("ns", database.DatabaseName, "Child");
			database.Tables.Add(childTable);
			foreignColumn = new Column(childTable, "PersonnID", "DisplayName", "byte", false);
			childTable.Columns.Add(foreignColumn);

			parentTable = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(parentTable);
			column1 = new Column(parentTable, "FirstName", "DisplayName", "string", false);
			primaryColumn = new Column(parentTable, "PersonnID", "DisplayName", "byte?", true);
			parentTable.Columns.Add(column1);
			parentTable.Columns.Add(primaryColumn);

			relation = new Relation("Childs", primaryColumn, "MyParent", foreignColumn, CascadeTriggers.None);
			parentTable.Relations.Add(relation);
			childTable.Relations.Add(relation); 
			
			source = sourceGenerator.GenerateSource(childTable);

			Assert.IsTrue(source.Contains("protected virtual void OnPropertyChanged(string PropertyName)"));
			Assert.IsTrue(source.Contains("public void Delete()"));
			Assert.IsTrue(source.Contains("public override string ToString()"));
			Assert.IsTrue(source.Contains("private void OnMyParentChanged(object sender, EventArgs e)"));

		}

		[TestMethod]
		public void ShouldGenerateProperties()
		{
			ViewModelSourceGenerator sourceGenerator;
			Database database;
			Table parentTable;
			Table childTable;
			Column column1, primaryColumn,foreignColumn;
			Relation relation;
			string source;

			sourceGenerator = new ViewModelSourceGenerator();

			database = new Database("ns", "MyDB");

			childTable = new Table("ns", database.DatabaseName, "Child");
			database.Tables.Add(childTable);
			foreignColumn = new Column(childTable, "PersonnID", "DisplayName", "byte", false);
			childTable.Columns.Add(foreignColumn);

			parentTable = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(parentTable);
			column1 = new Column(parentTable, "FirstName", "DisplayName", "string", false);
			primaryColumn = new Column(parentTable, "PersonnID", "DisplayName", "byte?", true);
			parentTable.Columns.Add(column1);
			parentTable.Columns.Add(primaryColumn);

			relation = new Relation("Childs", primaryColumn, "MyParent", foreignColumn, CascadeTriggers.None);
			parentTable.Relations.Add(relation);
			childTable.Relations.Add(relation);

			source = sourceGenerator.GenerateSource(parentTable);

			Assert.IsTrue(source.Contains("public IEnumerable<IViewModelProperty> Properties"));
			Assert.IsTrue(source.Contains("public string FirstName"));
			Assert.IsTrue(source.Contains("public byte? PersonnID"));
			Assert.IsTrue(source.Contains("public ChildsViewModelCollection Childs"));

			source = sourceGenerator.GenerateSource(childTable);
			Assert.IsTrue(source.Contains("public IEnumerable<IViewModelProperty> Properties"));
			Assert.IsTrue(source.Contains("public byte PersonnID"));
			Assert.IsTrue(source.Contains("public PersonnViewModel MyParent"));

		}






	}
}