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