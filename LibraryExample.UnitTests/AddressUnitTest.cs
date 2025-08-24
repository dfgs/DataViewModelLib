using DataViewModelLib;
using LibraryExample;
using LibraryExample.Models;
using LibraryExample.ViewModels;
using System.Collections.Specialized;

namespace LibraryExample.UnitTests
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

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).Delete();
			viewModels = testDatabaseViewModel.AddressViewModelCollection.ToArray();
			Assert.AreEqual(2, viewModels.Length);
			Assert.AreEqual("Home", viewModels[0].Street);
		}

		[TestMethod]
		public void ShouldRaiseTableChangedOnDelete()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			AddressViewModel[] viewModels;
			object? changedItem = null;
			int changedIndex = -1;
			NotifyCollectionChangedAction? changedAction = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);
			testDatabaseViewModel.AddressViewModelCollection.CollectionChanged += (sender, e) => { changedItem = e.OldItems?[0]; changedAction = e.Action; changedIndex = e.OldStartingIndex; }; ;

			testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).Delete();
			viewModels = testDatabaseViewModel.AddressViewModelCollection.ToArray();
			Assert.AreEqual(2, viewModels.Length);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("School", ((AddressViewModel)changedItem).Street);
			Assert.AreEqual(NotifyCollectionChangedAction.Remove, changedAction);
			Assert.AreEqual(1, changedIndex);
		}

		[TestMethod]
		public void ShouldRemove()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			AddressViewModel[] viewModels;
			AddressViewModel item;
			

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			item=testDatabaseViewModel.AddressViewModelCollection.ElementAt(1);
			testDatabaseViewModel.AddressViewModelCollection.Remove(item);

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ToArray();
			Assert.AreEqual(2, viewModels.Length);
			Assert.AreEqual("Home", viewModels[0].Street);

		}

		[TestMethod]
		public void ShouldRaiseTableChangedOnRemove()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			AddressViewModel[] viewModels;
			object? changedItem = null;
			int changedIndex = -1;
			NotifyCollectionChangedAction? changedAction = null;
			AddressViewModel item;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);
			testDatabaseViewModel.AddressViewModelCollection.CollectionChanged += (sender, e) => { changedItem = e.OldItems?[0]; changedAction = e.Action; changedIndex = e.OldStartingIndex; }; ;


			item = testDatabaseViewModel.AddressViewModelCollection.ElementAt(1);
			testDatabaseViewModel.AddressViewModelCollection.Remove(item);

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ToArray();
			Assert.AreEqual(2, viewModels.Length);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("School", ((AddressViewModel)changedItem).Street);
			Assert.AreEqual(NotifyCollectionChangedAction.Remove, changedAction);
			Assert.AreEqual(1, changedIndex);
		}


		[TestMethod]
		public void ShouldAdd()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			AddressViewModel[] viewModels;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			testDatabaseViewModel.AddressViewModelCollection.Add(new Address(4,"Street1"));

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ToArray();
			Assert.AreEqual(4, viewModels.Length);
			Assert.AreEqual("Street1", viewModels[3].Street);

		}

		[TestMethod]
		public void ShouldRaiseTableChangedOnAdd()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			AddressViewModel[] viewModels;
			object? changedItem = null;
			int changedIndex = -1;
			NotifyCollectionChangedAction? changedAction = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);
			testDatabaseViewModel.AddressViewModelCollection.CollectionChanged += (sender, e) => { changedItem = e.NewItems?[0]; changedAction = e.Action; changedIndex = e.NewStartingIndex; }; ;

			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);
			testDatabaseViewModel.AddressViewModelCollection.Add(new Address(4, "Street1"));

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ToArray();
			Assert.AreEqual(4, viewModels.Length);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Street1", ((AddressViewModel)changedItem).Street);
			Assert.AreEqual(NotifyCollectionChangedAction.Add, changedAction);
			Assert.AreEqual(3, changedIndex);
		}


		[TestMethod]
		public void ShouldGetSetProperty()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			AddressViewModel viewModel;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.AddressViewModelCollection.First();

			Assert.AreEqual("Home", viewModel.Street);
			viewModel.Street = "Home2";
			Assert.AreEqual("Home2", viewModel.Street);
		}
		[TestMethod]
		public void ShouldRaisePropertyChanged()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			AddressViewModel viewModel;
			string? propertyName = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.AddressViewModelCollection.First();
			viewModel.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };

			viewModel.Street = "Home2";
			Assert.AreEqual("Street", propertyName);
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