using LibraryExample;
using LibraryExample.Models;
using LibraryExample.ViewModels;
using System.Collections.Specialized;

namespace LibraryExample.UnitTests
{
	[TestClass]
	public class BilledPeopleUnitTest
	{

	

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
			testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).BilledPeople.CollectionChanged += (sender, e) => { changedItem = e.OldItems?[0]; changedAction = e.Action; changedIndex = e.OldStartingIndex; }; ;

			item = testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1);
			testDatabaseViewModel.PersonnViewModelCollection.Remove(item);

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).BilledPeople.ToArray();
			Assert.AreEqual(1, viewModels.Length);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Marje", ((PersonnViewModel)changedItem).FirstName);
			Assert.AreEqual(NotifyCollectionChangedAction.Remove, changedAction);
			Assert.AreEqual(1, changedIndex);
		}

		[TestMethod]
		public void ShouldRaiseTableChangedOnChange1()
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
			testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).BilledPeople.CollectionChanged += (sender, e) => { changedItem = e.OldItems?[0]; changedAction = e.Action; changedIndex = e.OldStartingIndex; }; ;

			item = testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1);
			item.BillingAddressID = 1;

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).BilledPeople.ToArray();
			Assert.AreEqual(1, viewModels.Length);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Marje", ((PersonnViewModel)changedItem).FirstName);
			Assert.AreEqual(NotifyCollectionChangedAction.Remove, changedAction);
			Assert.AreEqual(1, changedIndex);
		}

		[TestMethod]
		public void ShouldRaiseTableChangedOnChange2()
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
			testDatabaseViewModel.AddressViewModelCollection.ElementAt(0).BilledPeople.CollectionChanged += (sender, e) => { changedItem = e.NewItems?[0]; changedAction = e.Action; changedIndex = e.NewStartingIndex; }; ;

			item = testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1);
			item.BillingAddressID = 1;

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ElementAt(0).BilledPeople.ToArray();
			Assert.AreEqual(1, viewModels.Length);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Marje", ((PersonnViewModel)changedItem).FirstName);
			Assert.AreEqual(NotifyCollectionChangedAction.Add, changedAction);
			Assert.AreEqual(0, changedIndex);
		}

		[TestMethod]
		public void ShouldAdd()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel[] viewModels;
	
			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			testDatabaseViewModel.PersonnViewModelCollection.Add(new Personn(5, "Ned", "Flanders", 55) { BillingAddressID = 2 });

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).BilledPeople.ToArray();
			Assert.AreEqual(3, viewModels.Length);
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
			testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).BilledPeople.CollectionChanged += (sender, e) => { changedItem = e.NewItems?[0]; changedAction = e.Action; changedIndex = e.NewStartingIndex; }; ;

			testDatabaseViewModel.PersonnViewModelCollection.Add(new Personn(5, "Ned", "Flanders", 55) {  BillingAddressID=2});

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).BilledPeople.ToArray();
			Assert.AreEqual(3, viewModels.Length);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Ned", ((PersonnViewModel)changedItem).FirstName);
			Assert.AreEqual(NotifyCollectionChangedAction.Add, changedAction);
			Assert.AreEqual(2, changedIndex);
		}

		[TestMethod]
		public void ShouldGetSetSelectedItem()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			BilledPeopleViewModelCollection collection;


			string? propertyName = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			collection = testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).BilledPeople;
			collection.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };

			Assert.IsNull(collection.SelectedItem);
			collection.SelectedItem = collection.First();
			Assert.IsNotNull(collection.SelectedItem);
			Assert.AreEqual("Homer", collection.SelectedItem.FirstName);
			Assert.AreEqual("SelectedItem", propertyName);
		}

		[TestMethod]
		public void ShouldRemove()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel item;
			BilledPeopleViewModelCollection billedPeople;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			billedPeople = testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).BilledPeople;
			item = billedPeople.First();
			Assert.AreEqual(2, billedPeople.Count);
			Assert.AreEqual(4, testDatabaseViewModel.PersonnViewModelCollection.Count);
			billedPeople.Remove(item);
			Assert.AreEqual(1, billedPeople.Count);	// should remove from foreign collection
			Assert.AreEqual(4, testDatabaseViewModel.PersonnViewModelCollection.Count);	// should not remove from personn table
			Assert.IsNull(item.BillingAddressID);
		}

	}
}