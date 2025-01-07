using TestGUI.Data.DataSources;
using TestGUI.Data.DataSources.Models;
using TestGUI.Data.DataSources.ViewModels;
using DataModelLib.Common;
using System.Collections.Specialized;

namespace TestGUI.UnitTests
{
	[TestClass]
	public class PetUnitTest
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


		[TestMethod]
		public void ShouldDelete()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PetViewModel[] viewModels;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			testDatabaseViewModel.PetViewModelCollection.ElementAt(1).Delete();
			viewModels = testDatabaseViewModel.PetViewModelCollection.ToArray();
			Assert.AreEqual(2, viewModels.Length);
			Assert.AreEqual("Cat", viewModels[0].Name);

		}

		[TestMethod]
		public void ShouldRaiseTableChangedOnDelete()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PetViewModel[] viewModels;
			object? changedItem = null;
			int changedIndex = -1;
			NotifyCollectionChangedAction? changedAction = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);
			testDatabaseViewModel.PetViewModelCollection.CollectionChanged += (sender, e) => { changedItem = e.OldItems?[0]; changedAction = e.Action; changedIndex = e.OldStartingIndex; }; ;

			testDatabaseViewModel.PetViewModelCollection.ElementAt(1).Delete();
			viewModels = testDatabaseViewModel.PetViewModelCollection.ToArray();
			Assert.AreEqual(2, viewModels.Length);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Dog", ((PetViewModel)changedItem).Name);
			Assert.AreEqual(NotifyCollectionChangedAction.Remove, changedAction);
			Assert.AreEqual(1, changedIndex);
		}


		[TestMethod]
		public void ShouldRemove()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PetViewModel[] viewModels;
			PetViewModel item;
	
			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			item = testDatabaseViewModel.PetViewModelCollection.ElementAt(1);
			testDatabaseViewModel.PetViewModelCollection.Remove(item);

			viewModels = testDatabaseViewModel.PetViewModelCollection.ToArray();
			Assert.AreEqual(2, viewModels.Length);
			Assert.AreEqual("Cat", viewModels[0].Name);
		}
		[TestMethod]
		public void ShouldRaiseTableChangedOnRemove()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PetViewModel[] viewModels;
			object? changedItem = null;
			int changedIndex = -1;
			NotifyCollectionChangedAction? changedAction = null;
			PetViewModel item;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);
			testDatabaseViewModel.PetViewModelCollection.CollectionChanged += (sender, e) => { changedItem = e.OldItems?[0]; changedAction = e.Action; changedIndex = e.OldStartingIndex; }; ;

			item = testDatabaseViewModel.PetViewModelCollection.ElementAt(1);
			testDatabaseViewModel.PetViewModelCollection.Remove(item);
			viewModels = testDatabaseViewModel.PetViewModelCollection.ToArray();
			Assert.AreEqual(2, viewModels.Length);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Dog", ((PetViewModel)changedItem).Name);
			Assert.AreEqual(NotifyCollectionChangedAction.Remove, changedAction);
			Assert.AreEqual(1, changedIndex);
			
		}

		[TestMethod]
		public void ShouldAdd()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PetViewModel[] viewModels;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			testDatabaseViewModel.PetViewModelCollection.Add(new Pet(4,"Pet1"));

			viewModels = testDatabaseViewModel.PetViewModelCollection.ToArray();
			Assert.AreEqual(4, viewModels.Length);
			Assert.AreEqual("Pet1", viewModels[3].Name);

		}

		[TestMethod]
		public void ShouldRaiseTableChangedOnAdd()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PetViewModel[] viewModels;
			object? changedItem = null;
			int changedIndex = -1;
			NotifyCollectionChangedAction? changedAction = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);
			testDatabaseViewModel.PetViewModelCollection.CollectionChanged += (sender, e) => { changedItem = e.NewItems?[0]; changedAction = e.Action; changedIndex = e.NewStartingIndex; }; ;

			testDatabaseViewModel.PetViewModelCollection.Add(new Pet(4, "Pet1"));

			viewModels = testDatabaseViewModel.PetViewModelCollection.ToArray();
			Assert.AreEqual(4, viewModels.Length);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Pet1", ((PetViewModel)changedItem).Name);
			Assert.AreEqual(NotifyCollectionChangedAction.Add, changedAction);
			Assert.AreEqual(3, changedIndex);
		}

		[TestMethod]
		public void ShouldGetSetProperty()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PetViewModel viewModel;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.PetViewModelCollection.First();

			Assert.AreEqual("Cat", viewModel.Name);
			viewModel.Name = "Pet2";
			Assert.AreEqual("Pet2", viewModel.Name);
		}
		[TestMethod]
		public void ShouldRaisePropertyChanged()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PetViewModel viewModel;
			string? propertyName = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.PetViewModelCollection.First();
			viewModel.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };

			viewModel.Name = "Pet2";
			Assert.AreEqual("Name", propertyName);
		}

		[TestMethod]
		public void ShouldGetOwners()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel[] owners;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			owners = testDatabaseViewModel.PetViewModelCollection.ElementAt(0).Owners.ToArray();
			Assert.AreEqual(2, owners.Length);
			Assert.AreEqual("Homer", owners[0].FirstName);
			Assert.AreEqual("Marje", owners[1].FirstName);

			owners = testDatabaseViewModel.PetViewModelCollection.ElementAt(1).Owners.ToArray();
			Assert.AreEqual(2, owners.Length);
			Assert.AreEqual("Bart", owners[0].FirstName);
			Assert.AreEqual("Liza", owners[1].FirstName);
		}

	}
}