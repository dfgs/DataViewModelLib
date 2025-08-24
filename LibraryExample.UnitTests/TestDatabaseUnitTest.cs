using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryExample;
using LibraryExample.Models;
using LibraryExample.ViewModels;


namespace LibraryExample.UnitTests
{
	[TestClass]
	public class TestDatabaseUnitTest
	{

		#region AddressTable
		[TestMethod]
		public void ShouldNotCreateUniqueAddressViewModel()
		{
			TestDatabaseViewModel testDatabaseViewModel;
			TestDatabaseModel testDatabaseModel;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			Assert.ThrowsException<ArgumentNullException>(()=> testDatabaseViewModel.CreateAddressViewModel(null));
		}

		[TestMethod]
		public void ShouldCreateUniqueAddressViewModel()
		{
			TestDatabaseViewModel testDatabaseViewModel;
			TestDatabaseModel testDatabaseModel;
			AddressModel item12, item3;
			AddressViewModel viewModel1, viewModel2, viewModel3;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			item12 = testDatabaseModel.CreateAddressModel(new Address(55, "Street12"));
			item3 = testDatabaseModel.CreateAddressModel(new Address(66, "Street3"));
			viewModel1 = testDatabaseViewModel.CreateAddressViewModel(item12);
			viewModel2 = testDatabaseViewModel.CreateAddressViewModel(item12);
			viewModel3 = testDatabaseViewModel.CreateAddressViewModel(item3);
			Assert.AreEqual(viewModel1, viewModel2);
			Assert.AreNotEqual(viewModel1, viewModel3);
			Assert.AreNotEqual(viewModel2, viewModel3);
		}

		[TestMethod]
		public void ShouldGetAddressTable()
		{
			TestDatabaseViewModel testDatabaseViewModel;
			TestDatabaseModel testDatabaseModel;
			AddressViewModel[] viewModels;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModels = testDatabaseViewModel.AddressViewModelCollection.ToArray();
			Assert.AreEqual(3, viewModels.Length);
			Assert.AreEqual("Home", viewModels[0].Street);
			Assert.AreEqual("School", viewModels[1].Street);
			Assert.AreEqual("Work", viewModels[2].Street);
		}
		#endregion

		#region PersonnTable
		[TestMethod]
		public void ShouldNotCreateUniquePersonnViewModel()
		{
			TestDatabaseViewModel testDatabaseViewModel;
			TestDatabaseModel testDatabaseModel;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			Assert.ThrowsException<ArgumentNullException>(() => testDatabaseViewModel.CreatePersonnViewModel(null));
		}


		[TestMethod]
		public void ShouldCreateUniquePersonnViewModel()
		{
			TestDatabaseViewModel testDatabaseViewModel;
			TestDatabaseModel testDatabaseModel;
			PersonnModel item12, item3;
			PersonnViewModel viewModel1, viewModel2, viewModel3;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			item12 = testDatabaseModel.CreatePersonnModel(new Personn(55, "FN12","LN12",55));
			item3 = testDatabaseModel.CreatePersonnModel(new Personn(66, "FN3", "LN3", 66));
			viewModel1 = testDatabaseViewModel.CreatePersonnViewModel(item12);
			viewModel2 = testDatabaseViewModel.CreatePersonnViewModel(item12);
			viewModel3 = testDatabaseViewModel.CreatePersonnViewModel(item3);
			Assert.AreEqual(viewModel1, viewModel2);
			Assert.AreNotEqual(viewModel1, viewModel3);
			Assert.AreNotEqual(viewModel2, viewModel3);
		}

		[TestMethod]
		public void ShouldGetPersonnTable()
		{
			TestDatabaseViewModel testDatabaseViewModel;
			TestDatabaseModel testDatabaseModel;
			PersonnViewModel[] viewModels;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModels = testDatabaseViewModel.PersonnViewModelCollection.ToArray();
			Assert.AreEqual(4, viewModels.Length);
			Assert.AreEqual("Homer", viewModels[0].FirstName);
			Assert.AreEqual("Marje", viewModels[1].FirstName);
			Assert.AreEqual("Bart", viewModels[2].FirstName);
			Assert.AreEqual("Liza", viewModels[3].FirstName);
		}
		#endregion

		#region PetTable
		[TestMethod]
		public void ShouldNotCreateUniquePetViewModel()
		{
			TestDatabaseViewModel testDatabaseViewModel;
			TestDatabaseModel testDatabaseModel;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			Assert.ThrowsException<ArgumentNullException>(() => testDatabaseViewModel.CreatePetViewModel(null));
		}
		[TestMethod]
		public void ShouldCreateUniquePetViewModel()
		{
			TestDatabaseViewModel testDatabaseViewModel;
			TestDatabaseModel testDatabaseModel;
			PetModel item12, item3;
			PetViewModel viewModel1, viewModel2, viewModel3;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			item12 = testDatabaseModel.CreatePetModel(new Pet(55, "Pet12"));
			item3 = testDatabaseModel.CreatePetModel(new Pet(66, "Pet3"));
			viewModel1 = testDatabaseViewModel.CreatePetViewModel(item12);
			viewModel2 = testDatabaseViewModel.CreatePetViewModel(item12);
			viewModel3 = testDatabaseViewModel.CreatePetViewModel(item3);
			Assert.AreEqual(viewModel1, viewModel2);
			Assert.AreNotEqual(viewModel1, viewModel3);
			Assert.AreNotEqual(viewModel2, viewModel3);
		}

		[TestMethod]
		public void ShouldGetPetTable()
		{
			TestDatabaseViewModel testDatabaseViewModel;
			TestDatabaseModel testDatabaseModel;
			PetViewModel[] viewModels;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModels = testDatabaseViewModel.PetViewModelCollection.ToArray();
			Assert.AreEqual(3, viewModels.Length);
			Assert.AreEqual("Cat", viewModels[0].Name);
			Assert.AreEqual("Dog", viewModels[1].Name);
			Assert.AreEqual("Turtle", viewModels[2].Name);
		}
		#endregion
	}

}