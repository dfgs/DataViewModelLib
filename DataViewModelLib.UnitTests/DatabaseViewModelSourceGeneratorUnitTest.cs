using DataModelLib.Common.Schema;
using DataViewModelLib.SourceGenerator;
using System.Reflection;

namespace DataViewModelLib.UnitTests
{
	[TestClass]
	public class DatabaseViewModelSourceGeneratorUnitTest
	{
		[TestMethod]
		public void ShouldGenerateUsings()
		{
			DatabaseViewModelSourceGenerator sourceGenerator;
			Database database;
			string source;

			sourceGenerator = new DatabaseViewModelSourceGenerator();

			database = new Database("ns", "MyDB");
			database.Tables.Add(new Table("ns1", database.DatabaseName, "Personn1"));
			database.Tables.Add(new Table("ns2", database.DatabaseName, "Personn2"));
			database.Tables.Add(new Table("ns2", database.DatabaseName, "Personn3"));

			source = sourceGenerator.GenerateSource(database);

			Assert.IsTrue(source.Contains("using DataModelLib.Common;"));
			Assert.IsTrue(source.Contains("using ns1.Models"));
			Assert.IsTrue(source.Contains("using ns2.Models"));

		}

		[TestMethod]
		public void ShouldGenerateClass()
		{
			DatabaseViewModelSourceGenerator sourceGenerator;
			Database database;
			string source;

			sourceGenerator = new DatabaseViewModelSourceGenerator();

			database = new Database("ns", "MyDB");

			source = sourceGenerator.GenerateSource(database);

			Assert.IsTrue(source.Contains("namespace ns.ViewModels"));
			Assert.IsTrue(source.Contains("public partial class MyDBViewModel"));
		}
		[TestMethod]
		public void ShouldGenerateProperties()
		{
			DatabaseViewModelSourceGenerator sourceGenerator;
			Database database;
			string source;

			sourceGenerator = new DatabaseViewModelSourceGenerator();

			database = new Database("ns", "MyDB");
			database.Tables.Add(new Table("ns", database.DatabaseName, "Personn"));
			database.Tables.Add(new Table("ns", database.DatabaseName, "Address"));

			source = sourceGenerator.GenerateSource(database);

			Assert.IsTrue(source.Contains("public PersonnViewModelCollection PersonnViewModelCollection"));
			Assert.IsTrue(source.Contains("public AddressViewModelCollection AddressViewModelCollection"));
		}
		[TestMethod]
		public void ShouldGenerateConstructor()
		{
			DatabaseViewModelSourceGenerator sourceGenerator;
			Database database;
			string source;

			sourceGenerator = new DatabaseViewModelSourceGenerator();

			database = new Database("ns", "MyDB");
			database.Tables.Add(new Table("ns", database.DatabaseName, "Personn"));
			database.Tables.Add(new Table("ns", database.DatabaseName, "Address"));

			source = sourceGenerator.GenerateSource(database);

			Assert.IsTrue(source.Contains("public MyDBViewModel(MyDBModel DataSource)"));
			Assert.IsTrue(source.Contains("PersonnViewModelCollection = new PersonnViewModelCollection(this, dataSource)"));
			Assert.IsTrue(source.Contains("AddressViewModelCollection = new AddressViewModelCollection(this, dataSource)"));

		}


		[TestMethod]
		public void ShouldGenerateMethods()
		{
			DatabaseViewModelSourceGenerator sourceGenerator;
			Database database;
			string source;

			sourceGenerator = new DatabaseViewModelSourceGenerator();

			database = new Database("ns", "MyDB");
			database.Tables.Add(new Table("ns", database.DatabaseName, "Personn"));
			database.Tables.Add(new Table("ns", database.DatabaseName, "Address"));

			source = sourceGenerator.GenerateSource(database);

			Assert.IsTrue(source.Contains("public PersonnViewModel CreatePersonnViewModel(PersonnModel Item)"));
			Assert.IsTrue(source.Contains("public AddressViewModel CreateAddressViewModel(AddressModel Item)"));


		}







	}
}