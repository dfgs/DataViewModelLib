using DataModelLib.Common.Schema;
using DataViewModelLib.SourceGenerator;
using System.Reflection;

namespace DataViewModelLib.UnitTests
{
	[TestClass]
	public class TableViewModelCollectionSourceGeneratorUnitTest
	{
		[TestMethod]
		public void ShouldGenerateUsings()
		{
			TableViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new TableViewModelCollectionSourceGenerator();

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
			TableViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new TableViewModelCollectionSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(table);

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("namespace ns.ViewModels"));
			Assert.IsTrue(source.Contains("public partial class PersonnViewModelCollection : IEnumerable<PersonnViewModel>, INotifyCollectionChanged"));

		}

		[TestMethod]
		public void ShouldGenerateConstructor()
		{
			TableViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new TableViewModelCollectionSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(table);

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("public PersonnViewModelCollection(MyDBModel DatabaseModel, IEnumerable<PersonnModel> Items)"));

		}

		[TestMethod]
		public void ShouldGenerateIEnumerableMethods()
		{
			TableViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new TableViewModelCollectionSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(table);

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("public IEnumerator<PersonnViewModel> GetEnumerator()"));
			Assert.IsTrue(source.Contains("protected virtual void OnPersonnTableChanged(Personn Item,TableChangedActions Action, int Index)"));
		}

		[TestMethod]
		public void ShouldGenerateINotifyCollectionChangedMethods()
		{
			TableViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new TableViewModelCollectionSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(table);

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)"));

		}




	}
}