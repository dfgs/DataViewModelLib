using DataViewModelLib.Schema;

namespace DataModelLib.UnitTests
{
	[TestClass]
	public class DatabaseViewModelUnitTest
	{

		

		[TestMethod]
		public void ShouldGenerateDatabaseViewModelClass()
		{
			Database model;
			Table table;
			string source;

			model = new Database("ns","MyDB");
			table = new Table("ns1", model.DatabaseName, "Personn1");
			table.PrimaryKey = new Column(table,"PersonnID", "byte", false);
			model.Tables.Add(table);

			table = new Table("ns2", model.DatabaseName, "Personn2"); // no PK
			model.Tables.Add(table);
			source = model.GenerateDatabaseViewModelClass();


			Assert.IsTrue(source.Contains("namespace ns"));
			Assert.IsTrue(source.Contains("using ns1"));
			Assert.IsTrue(source.Contains("using ns2"));
			Assert.IsTrue(source.Contains("public partial class MyDBViewModel"));
			Assert.IsTrue(source.Contains("public MyDBViewModel(MyDBModel DataSource)"));

		}

		[TestMethod]
		public void ShouldGenerateDatabaseViewModelConstructor()
		{
			Database model;
			string source;

			model = new Database("ns", "MyDB");
			source = model.GenerateDatabaseViewModelConstructor();

			Assert.IsTrue(source.Contains("public MyDBViewModel(MyDBModel DataSource)"));

		}

	}
}