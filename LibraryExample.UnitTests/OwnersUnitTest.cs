using LibraryExample;
using LibraryExample.Models;
using LibraryExample.ViewModels;
using System.Collections.Specialized;

namespace LibraryExample.UnitTests
{
	[TestClass]
	public class OwnersUnitTest
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
			testDatabaseViewModel.PetViewModelCollection.ElementAt(0).Owners.CollectionChanged += (sender, e) => { changedItem = e.OldItems?[0]; changedAction = e.Action; changedIndex = e.OldStartingIndex; }; ;

			item = testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1);
			testDatabaseViewModel.PersonnViewModelCollection.Remove(item);

			viewModels = testDatabaseViewModel.PetViewModelCollection.ElementAt(0).Owners.ToArray();
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
			testDatabaseViewModel.PetViewModelCollection.ElementAt(0).Owners.CollectionChanged += (sender, e) => { changedItem = e.OldItems?[0]; changedAction = e.Action; changedIndex = e.OldStartingIndex; }; ;

			item = testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1);
			item.PetID = 2;

			viewModels = testDatabaseViewModel.PetViewModelCollection.ElementAt(0).Owners.ToArray();
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
			testDatabaseViewModel.PetViewModelCollection.ElementAt(2).Owners.CollectionChanged += (sender, e) => { changedItem = e.NewItems?[0]; changedAction = e.Action; changedIndex = e.NewStartingIndex; }; ;

			item = testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1);
			item.PetID = 3;

			viewModels = testDatabaseViewModel.PetViewModelCollection.ElementAt(0).Owners.ToArray();
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
			testDatabaseViewModel.PetViewModelCollection.ElementAt(1).Owners.CollectionChanged += (sender, e) => { changedItem = e.NewItems?[0]; changedAction = e.Action; changedIndex = e.NewStartingIndex; }; ;

			testDatabaseViewModel.PersonnViewModelCollection.Add(new Personn(5, "Ned", "Flanders", 55) { PetID = 2});

			viewModels = testDatabaseViewModel.PetViewModelCollection.ElementAt(1).Owners.ToArray();
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
			OwnersViewModelCollection collection;


			string? propertyName = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			collection = testDatabaseViewModel.PetViewModelCollection.ElementAt(0).Owners;
			collection.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };

			Assert.IsNull(collection.SelectedItem);
			collection.SelectedItem = collection.First();
			Assert.IsNotNull(collection.SelectedItem);
			Assert.AreEqual("Homer", collection.SelectedItem.FirstName);
			Assert.AreEqual("SelectedItem", propertyName);
		}


	}
}