using TestGUI.Data.DataSources;
using TestGUI.Data.DataSources.Models;
using TestGUI.Data.DataSources.ViewModels;
using DataModelLib.Common;

namespace TestGUI.UnitTests
{
	[TestClass]
	public class AddressUnitTest
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
		public void ShouldDelete()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			AddressViewModel[] viewModels;
			Address? changedItem = null;
			int changedIndex = -1;
			TableChangedActions? changedAction = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseModel.AddressTableChanged += (item, action, index) => { changedItem = item; changedAction = action; changedIndex = index; };
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).Delete();
			viewModels = testDatabaseViewModel.AddressViewModelCollection.ToArray();
			Assert.AreEqual(2, viewModels.Length);
			Assert.AreEqual("Home", viewModels[0].Street);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("School", changedItem.Street);
			Assert.AreEqual(TableChangedActions.Remove, changedAction);
			Assert.AreEqual(1, changedIndex);
		}

		[TestMethod]
		public void ShouldRemove()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			AddressViewModel[] viewModels;
			AddressViewModel item;
			Address? changedItem = null;
			int changedIndex = -1;
			TableChangedActions? changedAction = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseModel.AddressTableChanged += (item, action, index) => { changedItem = item; changedAction = action; changedIndex = index; };
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			item=testDatabaseViewModel.AddressViewModelCollection.ElementAt(1);
			testDatabaseViewModel.AddressViewModelCollection.Remove(item);

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ToArray();
			Assert.AreEqual(2, viewModels.Length);
			Assert.AreEqual("Home", viewModels[0].Street);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("School", changedItem.Street);
			Assert.AreEqual(TableChangedActions.Remove, changedAction);
			Assert.AreEqual(1, changedIndex);
		}

		[TestMethod]
		public void ShouldAdd()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			AddressViewModel[] viewModels;
			Address? changedItem = null;
			int changedIndex = -1;
			TableChangedActions? changedAction = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseModel.AddressTableChanged += (item, action, index) => { changedItem = item; changedAction = action; changedIndex = index; };
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			testDatabaseViewModel.AddressViewModelCollection.Add(new Address(4,"Street1"));

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ToArray();
			Assert.AreEqual(4, viewModels.Length);
			Assert.AreEqual("Street1", viewModels[3].Street);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Street1", changedItem.Street);
			Assert.AreEqual(TableChangedActions.Add, changedAction);
			Assert.AreEqual(3, changedIndex);
		}

		[TestMethod]
		public void ShouldGetSetProperty()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			AddressViewModel viewModel;
			string? propertyName = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.AddressViewModelCollection.First();
			viewModel.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };

			Assert.AreEqual("Home", viewModel.Street);
			viewModel.Street = "Home2";
			Assert.AreEqual("Home2", viewModel.Street);
			Assert.AreEqual("Street", propertyName);
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



	}
}