using DataModelLib.Common.Schema;
using DataViewModelLib.SourceGenerator;
using System.Reflection;

namespace DataViewModelLib.UnitTests
{
	[TestClass]
	public class TableViewModelSourceGeneratorUnitTest
	{
		[TestMethod]
		public void ShouldGenerateUsings()
		{
			TableViewModelSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new TableViewModelSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns1", database.DatabaseName, "Personn1");
			table = new Table("ns2", database.DatabaseName, "Personn2");
			database.Tables.Add(table);

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("using System.Windows;"));

		}

		[TestMethod]
		public void ShouldGenerateClass()
		{
			TableViewModelSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new TableViewModelSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(table);

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("public partial class PersonnViewModel"));

		}

		[TestMethod]
		public void ShouldGenerateConstructor()
		{
			TableViewModelSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new TableViewModelSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(table);

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("public PersonnViewModel(MyDBViewModel DatabaseViewModel,PersonnModel DataSource)"));

		}








	}
}