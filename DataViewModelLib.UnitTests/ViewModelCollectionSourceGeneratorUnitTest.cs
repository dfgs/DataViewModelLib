using DataModelLib.Common.Schema;
using DataViewModelLib.SourceGenerator;
using System.Reflection;

namespace DataViewModelLib.UnitTests
{
	[TestClass]
	public class ViewModelCollectionSourceGeneratorUnitTest
	{
		[TestMethod]
		public void ShouldGenerateUsings()
		{
			ViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new ViewModelCollectionSourceGenerator();

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
			ViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new ViewModelCollectionSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(table);

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("namespace ns.ViewModels"));
			Assert.IsTrue(source.Contains("public partial class PersonnViewModelCollection : IViewModelCollection, IEnumerable<PersonnViewModel>, INotifyCollectionChanged"));

		}

		[TestMethod]
		public void ShouldGenerateConstructor()
		{
			ViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new ViewModelCollectionSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(table);

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("public PersonnViewModelCollection(MyDBViewModel DatabaseViewModel, MyDBModel DatabaseModel)"));

		}

		[TestMethod]
		public void ShouldGenerateIEnumerableMethods()
		{
			ViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new ViewModelCollectionSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(table);

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("public IEnumerator<PersonnViewModel> GetEnumerator()"));
			Assert.IsTrue(source.Contains("protected virtual void OnPersonnTableChanged(Personn Item,TableChangedActions Action, int Index)"));
			Assert.IsTrue(source.Contains("public void Remove(PersonnViewModel Item)"));
			Assert.IsTrue(source.Contains("public void Add(Personn Item)"));
			Assert.IsTrue(source.Contains("void IViewModelCollection.Add(object Item)"));
		}

		[TestMethod]
		public void ShouldGenerateINotifyCollectionChangedMethods()
		{
			ViewModelCollectionSourceGenerator sourceGenerator;
			Database database;
			Table table;
			string source;

			sourceGenerator = new ViewModelCollectionSourceGenerator();

			database = new Database("ns", "MyDB");
			table = new Table("ns", database.DatabaseName, "Personn");
			database.Tables.Add(table);

			source = sourceGenerator.GenerateSource(table);

			Assert.IsTrue(source.Contains("protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)"));

		}




	}
}