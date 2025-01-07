using TestGUI.Data.DataSources;
using TestGUI.Data.DataSources.Models;
using TestGUI.Data.DataSources.ViewModels;
using DataModelLib.Common;
using System.Collections.Specialized;

namespace TestGUI.UnitTests
{
	[TestClass]
	public class PersonnUnitTest
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


		[TestMethod]
		public void ShouldDelete()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel[] viewModels;
			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1).Delete();
			viewModels = testDatabaseViewModel.PersonnViewModelCollection.ToArray();
			Assert.AreEqual(3, viewModels.Length);
			Assert.AreEqual("Homer", viewModels[0].FirstName);
			
		}

		[TestMethod]
		public void ShouldRaiseTableChangedOnDelete()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel[] viewModels;
			object? changedItem = null;
			int changedIndex = -1;
			NotifyCollectionChangedAction? changedAction = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);
			testDatabaseViewModel.PersonnViewModelCollection.CollectionChanged += (sender, e) => { changedItem = e.OldItems?[0]; changedAction = e.Action; changedIndex = e.OldStartingIndex; }; ;

			testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1).Delete();
			viewModels = testDatabaseViewModel.PersonnViewModelCollection.ToArray();
			Assert.AreEqual(3, viewModels.Length);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Marje", ((PersonnViewModel)changedItem).FirstName);
			Assert.AreEqual(NotifyCollectionChangedAction.Remove, changedAction);
			Assert.AreEqual(1, changedIndex);
		}


		[TestMethod]
		public void ShouldRemove()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel[] viewModels;
			PersonnViewModel item;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			item=testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1);
			testDatabaseViewModel.PersonnViewModelCollection.Remove(item);

			viewModels = testDatabaseViewModel.PersonnViewModelCollection.ToArray();
			Assert.AreEqual(3, viewModels.Length);
			Assert.AreEqual("Homer", viewModels[0].FirstName);
	
		}

		[TestMethod]
		public void ShouldRaiseTableChangedOnRemove()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel[] viewModels;
			PersonnViewModel item;
			object? changedItem = null;
			int changedIndex = -1;
			NotifyCollectionChangedAction? changedAction = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);
			testDatabaseViewModel.PersonnViewModelCollection.CollectionChanged += (sender, e) => { changedItem = e.OldItems?[0]; changedAction = e.Action; changedIndex = e.OldStartingIndex; }; ;

			item = testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1);
			testDatabaseViewModel.PersonnViewModelCollection.Remove(item);

			viewModels = testDatabaseViewModel.PersonnViewModelCollection.ToArray();
			Assert.AreEqual(3, viewModels.Length);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Marje", ((PersonnViewModel)changedItem).FirstName);
			Assert.AreEqual(NotifyCollectionChangedAction.Remove, changedAction);
			Assert.AreEqual(1, changedIndex);
		}
		[TestMethod]
		public void ShouldAdd()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel[] viewModels;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			testDatabaseViewModel.PersonnViewModelCollection.Add(new Personn(5,"FN1","LN1",55));

			viewModels = testDatabaseViewModel.PersonnViewModelCollection.ToArray();
			Assert.AreEqual(5, viewModels.Length);
			Assert.AreEqual("FN1", viewModels[4].FirstName);
		}
		[TestMethod]
		public void ShouldRaiseTableChangedOnAdd()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel[] viewModels;
			object? changedItem = null;
			int changedIndex = -1;
			NotifyCollectionChangedAction? changedAction = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);
			testDatabaseViewModel.PersonnViewModelCollection.CollectionChanged += (sender, e) => { changedItem = e.NewItems?[0]; changedAction = e.Action; changedIndex = e.NewStartingIndex; }; ;

			testDatabaseViewModel.PersonnViewModelCollection.Add(new Personn(5, "FN1", "LN1", 55));

			viewModels = testDatabaseViewModel.PersonnViewModelCollection.ToArray();
			Assert.AreEqual(5, viewModels.Length);
	
			Assert.IsNotNull(changedItem);
			Assert.AreEqual("FN1", ((PersonnViewModel)changedItem).FirstName);
			Assert.AreEqual(NotifyCollectionChangedAction.Add, changedAction);
			Assert.AreEqual(4, changedIndex);
		}
		[TestMethod]
		public void ShouldGetSetProperty()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel viewModel;
			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.PersonnViewModelCollection.First();

			Assert.AreEqual("Simpson", viewModel.LastName);
			viewModel.LastName= "LN1";
			Assert.AreEqual("LN1", viewModel.LastName);
		}

		[TestMethod]
		public void ShouldRaisePropertyChanged()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel viewModel;
			string? propertyName = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.PersonnViewModelCollection.First();
			viewModel.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };

			viewModel.LastName = "LN1";
			Assert.AreEqual("LastName", propertyName);
		}


		[TestMethod]
		public void ShouldGetSetSelectedItem()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			string? propertyName = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			testDatabaseViewModel.PersonnViewModelCollection.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };

			Assert.IsNull(testDatabaseViewModel.PersonnViewModelCollection.SelectedItem);
			testDatabaseViewModel.PersonnViewModelCollection.SelectedItem = testDatabaseViewModel.PersonnViewModelCollection.First();
			Assert.IsNotNull(testDatabaseViewModel.PersonnViewModelCollection.SelectedItem);
			Assert.AreEqual("Homer", testDatabaseViewModel.PersonnViewModelCollection.SelectedItem.FirstName);
			Assert.AreEqual("SelectedItem", propertyName);
		}

		[TestMethod]
		public void ShouldGetBillingAddress()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel viewModel;
			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.PersonnViewModelCollection.First();
			Assert.IsNotNull(viewModel.BillingAddress);
			Assert.AreEqual("School", viewModel.BillingAddress.Street);
		}
		[TestMethod]
		public void ShouldNotGetBillingAddress()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel viewModel;
			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.PersonnViewModelCollection.ElementAt(2);
			Assert.IsNull(viewModel.BillingAddress);
		}

		[TestMethod]
		public void ShouldGetDeliveryAddress()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel viewModel;
			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.PersonnViewModelCollection.First();
			Assert.IsNotNull(viewModel.DeliveryAddress);
			Assert.AreEqual("Home", viewModel.DeliveryAddress.Street);
		}
		[TestMethod]
		public void ShouldGetPreferedPetAddress()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel viewModel;
			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.PersonnViewModelCollection.First();
			Assert.IsNotNull(viewModel.PreferedPet);
			Assert.AreEqual("Cat", viewModel.PreferedPet.Name);
		}

		[TestMethod]
		public void ShouldRaiseBillingAddressPropertyChanged()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel viewModel;
			string? propertyName = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.PersonnViewModelCollection.First();
			viewModel.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };

			viewModel.BillingAddressID = 3;
			Assert.AreEqual("BillingAddress", propertyName);
			Assert.AreEqual("Work", viewModel.BillingAddress.Street);
		}
		[TestMethod]
		public void ShouldRaiseDeliveryAddressPropertyChanged()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel viewModel;
			string? propertyName = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.PersonnViewModelCollection.First();
			viewModel.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };

			viewModel.DeliveryAddressID = 3;
			Assert.AreEqual("DeliveryAddress", propertyName);
			Assert.AreEqual("Work", viewModel.DeliveryAddress.Street);
		}
		[TestMethod]
		public void ShouldRaisePreferedPetPropertyChanged()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel viewModel;
			string? propertyName = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.PersonnViewModelCollection.First();
			viewModel.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };

			viewModel.PetID = 3;
			Assert.AreEqual("PreferedPet", propertyName);
			Assert.AreEqual("Turtle", viewModel.PreferedPet.Name);
		}

	}
}