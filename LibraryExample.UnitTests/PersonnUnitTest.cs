using DataModelLib;
using DataViewModelLib;
using LibraryExample.Models;
using LibraryExample.ViewModels;
using System.Collections.Specialized;
using BlueprintLib.Attributes;

namespace LibraryExample.UnitTests
{
	[DTO("Personn"), Blueprint("TableViewModel.UnitTest.*"), MockCount(10), TestClass]
	public partial class PersonnUnitTest
	{

		[TestMethod]
		public void ShouldReturnToString()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel? personn;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);
			personn = testDatabaseViewModel.PersonnViewModelCollection.ElementAt(0);

			Assert.IsNotNull(personn);
			Assert.AreEqual("Homer Simpson", personn.ToString());
		}




	}
}