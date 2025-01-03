using TestGUI.Data.DataSources;
using TestGUI.Data.DataSources.Models;
using TestGUI.Data.DataSources.ViewModels;
using DataModelLib.Common;

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
			Personn? changedItem = null;
			int changedIndex = -1;
			TableChangedActions? changedAction = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseModel.PersonnTableChanged += (item, action, index) => { changedItem = item; changedAction = action; changedIndex = index; };
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1).Delete();
			viewModels = testDatabaseViewModel.PersonnViewModelCollection.ToArray();
			Assert.AreEqual(3, viewModels.Length);
			Assert.AreEqual("Homer", viewModels[0].FirstName);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Marje", changedItem.FirstName);
			Assert.AreEqual(TableChangedActions.Remove, changedAction);
			Assert.AreEqual(1, changedIndex);
		}

		[TestMethod]
		public void ShouldRemove()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel[] viewModels;
			PersonnViewModel item;
			Personn? changedItem = null;
			int changedIndex = -1;
			TableChangedActions? changedAction = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseModel.PersonnTableChanged += (item, action, index) => { changedItem = item; changedAction = action; changedIndex = index; };
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			item=testDatabaseViewModel.PersonnViewModelCollection.ElementAt(1);
			testDatabaseViewModel.PersonnViewModelCollection.Remove(item);

			viewModels = testDatabaseViewModel.PersonnViewModelCollection.ToArray();
			Assert.AreEqual(3, viewModels.Length);
			Assert.AreEqual("Homer", viewModels[0].FirstName);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("Marje", changedItem.FirstName);
			Assert.AreEqual(TableChangedActions.Remove, changedAction);
			Assert.AreEqual(1, changedIndex);
		}

		[TestMethod]
		public void ShouldAdd()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel[] viewModels;
			Personn? changedItem = null;
			int changedIndex = -1;
			TableChangedActions? changedAction = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseModel.PersonnTableChanged += (item, action, index) => { changedItem = item; changedAction = action; changedIndex = index; };
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			testDatabaseViewModel.PersonnViewModelCollection.Add(new Personn(5,"FN1","LN1",55));

			viewModels = testDatabaseViewModel.PersonnViewModelCollection.ToArray();
			Assert.AreEqual(5, viewModels.Length);
			Assert.AreEqual("FN1", viewModels[4].FirstName);

			Assert.IsNotNull(changedItem);
			Assert.AreEqual("FN1", changedItem.FirstName);
			Assert.AreEqual(TableChangedActions.Add, changedAction);
			Assert.AreEqual(4, changedIndex);
		}

		[TestMethod]
		public void ShouldGetSetProperty()
		{
			TestDatabaseModel testDatabaseModel;
			TestDatabaseViewModel testDatabaseViewModel;
			PersonnViewModel viewModel;
			string? propertyName = null;

			testDatabaseModel = new TestDatabaseModel(Utils.CreateTestDatabase());
			testDatabaseViewModel = new TestDatabaseViewModel(testDatabaseModel);

			viewModel = testDatabaseViewModel.PersonnViewModelCollection.First();
			viewModel.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };

			Assert.AreEqual("Simpson", viewModel.LastName);
			viewModel.LastName= "LN1";
			Assert.AreEqual("LN1", viewModel.LastName);
			Assert.AreEqual("LastName", propertyName);
		}
	}
}