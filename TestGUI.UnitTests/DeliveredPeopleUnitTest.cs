using TestGUI.Data.DataSources;
using TestGUI.Data.DataSources.Models;
using TestGUI.Data.DataSources.ViewModels;
using DataModelLib.Common;
using System.Collections.Specialized;

namespace TestGUI.UnitTests
{
	[TestClass]
	public class DeliveredPeopleUnitTest
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
			testDatabaseViewModel.AddressViewModelCollection.ElementAt(0).DeliveredPeople.CollectionChanged += (sender, e) => { changedItem = e.OldItems?[0]; changedAction = e.Action; changedIndex = e.OldStartingIndex; }; ;

			item = testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1);
			testDatabaseViewModel.PersonnViewModelCollection.Remove(item);

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ElementAt(0).DeliveredPeople.ToArray();
			Assert.AreEqual(3, viewModels.Length);

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
			testDatabaseViewModel.AddressViewModelCollection.ElementAt(0).DeliveredPeople.CollectionChanged += (sender, e) => { changedItem = e.OldItems?[0]; changedAction = e.Action; changedIndex = e.OldStartingIndex; }; ;

			item = testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1);
			item.DeliveryAddressID = 2;

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ElementAt(0).DeliveredPeople.ToArray();
			Assert.AreEqual(3, viewModels.Length);

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
			testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).DeliveredPeople.CollectionChanged += (sender, e) => { changedItem = e.NewItems?[0]; changedAction = e.Action; changedIndex = e.NewStartingIndex; }; ;

			item = testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1);
			item.DeliveryAddressID = 2;

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ElementAt(1).DeliveredPeople.ToArray();
			Assert.AreEqual(1, viewModels.Length);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Marje", ((PersonnViewModel)changedItem).FirstName);
			Assert.AreEqual(NotifyCollectionChangedAction.Add, changedAction);
			Assert.AreEqual(0, changedIndex);
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
			testDatabaseViewModel.AddressViewModelCollection.ElementAt(0).DeliveredPeople.CollectionChanged += (sender, e) => { changedItem = e.NewItems?[0]; changedAction = e.Action; changedIndex = e.NewStartingIndex; }; ;

			testDatabaseViewModel.PersonnViewModelCollection.Add(new Personn(5, "Ned", "Flanders", 55) {  DeliveryAddressID=1});

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ElementAt(0).DeliveredPeople.ToArray();
			Assert.AreEqual(5, viewModels.Length);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Ned", ((PersonnViewModel)changedItem).FirstName);
			Assert.AreEqual(NotifyCollectionChangedAction.Add, changedAction);
			Assert.AreEqual(4, changedIndex);
		}

	}
}