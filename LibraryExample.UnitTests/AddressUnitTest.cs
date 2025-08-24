using DataModelLib;
using DataViewModelLib;
using LibraryExample;
using LibraryExample.Models;
using LibraryExample.ViewModels;
using System.Collections.Specialized;
using BlueprintLib.Attributes;

namespace LibraryExample.UnitTests
{
	[DTO("Address"), Blueprint("TableViewModel.UnitTest.*"), MockCount(4), TestClass]
	public partial class AddressUnitTest
	{

		[TestMethod]
		public void ShouldReturnToString()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			AddressViewModel? address;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);
			address = testDatabaseViewModel.AddressViewModelCollection.ElementAt(0);

			Assert.IsNotNull(address);
			Assert.AreEqual("123 Home", address.ToString());
		}


		
	}
}