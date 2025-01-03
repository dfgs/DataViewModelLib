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

			Assert.IsTrue(source.Contains("public PersonnViewModel(MyDBModel DatabaseModel,PersonnModel DataSource)"));

		}

		[TestMethod]
		public void ShouldGenerateMethods()
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

			Assert.IsTrue(source.Contains("protected virtual void OnPropertyChanged(string PropertyName)"));
			Assert.IsTrue(source.Contains("public void Delete()"));
			Assert.IsTrue(source.Contains("public override string ToString()"));

		}

		[TestMethod]
		public void ShouldGenerateProperties()
		{
			ViewModelSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new ViewModelSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(table);
			table.Columns.Add(new Column(table, "FirstName", "string", false));
			table.Columns.Add(new Column(table, "MailID", "byte?", true));

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("public string FirstName"));
			Assert.IsTrue(source.Contains("public byte? MailID"));
		}






	}
}