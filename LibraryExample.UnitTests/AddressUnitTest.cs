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





		[TestMethod]
		public void ShouldGetSetSelectedItem()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			string? propertyName = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			testDatabaseViewModel.AddressViewModelCollection.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };

			Assert.IsNull(testDatabaseViewModel.AddressViewModelCollection.SelectedItem);
			testDatabaseViewModel.AddressViewModelCollection.SelectedItem = testDatabaseViewModel.AddressViewModelCollection.First();
			Assert.IsNotNull(testDatabaseViewModel.AddressViewModelCollection.SelectedItem);
			Assert.AreEqual("Home", testDatabaseViewModel.AddressViewModelCollection.SelectedItem.Street);
			Assert.AreEqual("SelectedItem", propertyName);
		}

		[TestMethod]
		public void ShouldGetBilledPeople()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel[] billedPeople;
	
			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);


			billedPeople = testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).BilledPeople.ToArray();
			Assert.AreEqual(2, billedPeople.Length);
			Assert.AreEqual("Homer", billedPeople[0].FirstName);
			Assert.AreEqual("Marje", billedPeople[1].FirstName);

			billedPeople = testDatabaseViewModel.AddressViewModelCollection.ElementAt(2).BilledPeople.ToArray();
			Assert.AreEqual(0, billedPeople.Length);

		}
		[TestMethod]
		public void ShouldGetDeliveredPeople()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel[] billedPeople;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);


			billedPeople = testDatabaseViewModel.AddressViewModelCollection.ElementAt(0).DeliveredPeople.ToArray();
			Assert.AreEqual(4, billedPeople.Length);
			Assert.AreEqual("Homer", billedPeople[0].FirstName);
			Assert.AreEqual("Marje", billedPeople[1].FirstName);
			Assert.AreEqual("Bart", billedPeople[2].FirstName);
			Assert.AreEqual("Liza", billedPeople[3].FirstName);

			billedPeople = testDatabaseViewModel.AddressViewModelCollection.ElementAt(2).DeliveredPeople.ToArray();
			Assert.AreEqual(0, billedPeople.Length);

		}

		[TestMethod]
		public void ShouldCreateViewModelProperties()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			IViewModelProperty[] properties;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			properties = testDatabaseViewModel.AddressViewModelCollection.First().Properties.ToArray();

			Assert.AreEqual(3, properties.Length);
			Assert.AreEqual("Address id", properties[0].Name);
			Assert.AreEqual("Street", properties[1].Name);
			Assert.AreEqual("Number", properties[2].Name);
		}


	}
}