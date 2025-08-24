using DataModelLib;
using DataViewModelLib;
using LibraryExample;
using LibraryExample.Models;
using LibraryExample.ViewModels;
using System.Collections.Specialized;
using BlueprintLib.Attributes;

namespace LibraryExample.UnitTests
{
	[DTO("Pet"), Blueprint("TableViewModel.UnitTest.*"), MockCount(6), TestClass]
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

		[TestMethod]
		public void ShouldGetSetSelectedItem()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			string? propertyName = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			testDatabaseViewModel.PetViewModelCollection.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };

			Assert.IsNull(testDatabaseViewModel.PetViewModelCollection.SelectedItem);
			testDatabaseViewModel.PetViewModelCollection.SelectedItem = testDatabaseViewModel.PetViewModelCollection.First();
			Assert.IsNotNull(testDatabaseViewModel.PetViewModelCollection.SelectedItem);
			Assert.AreEqual("Cat", testDatabaseViewModel.PetViewModelCollection.SelectedItem.Name);
			Assert.AreEqual("SelectedItem", propertyName);
		}

		[TestMethod]
		public void ShouldCreateViewModelProperties()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			IViewModelProperty[] properties;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			properties = testDatabaseViewModel.PetViewModelCollection.First().Properties.ToArray();

			Assert.AreEqual(3, properties.Length);
			Assert.AreEqual("Pet id", properties[0].Name);
			Assert.AreEqual("Name", properties[1].Name);
			Assert.AreEqual("IsValid", properties[2].Name);
		}


	}
}