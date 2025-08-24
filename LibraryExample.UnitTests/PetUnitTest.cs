using DataModelLib;
using DataViewModelLib;
using LibraryExample.Models;
using LibraryExample.ViewModels;
using System.Collections.Specialized;
using BlueprintLib.Attributes;

namespace LibraryExample.UnitTests
{
	[TableUnitTest, DTO("Pet"),  Blueprint("TableViewModel.UnitTest.*"), MockCount(6), TestClass]
	public partial class PetUnitTest
	{

		[TestMethod]
		public void ShouldReturnToString()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PetViewModel? pet;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);
			pet = testDatabaseViewModel.PetViewModelCollection.ElementAt(0);

			Assert.IsNotNull(pet);
			Assert.AreEqual("Cat", pet.ToString());
		}



	}
}