using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestGUI.Data.DataSources.ViewModels;
using TestGUI.Data.DataSources.Models;
using TestGUI.Data.DataSources;

namespace TestGUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private TestDatabaseViewModel testDatabaseViewModel;



		public MainWindow()
		{
			InitializeComponent();

			TestDatabase testDatabase = new TestDatabase();

			testDatabase.PersonnTable.Add(new Personn(1, "Homer", "Simpson", 40) { DeliveryAddressID = 1, BillingAddressID = 2, PetID = 1 });
			testDatabase.PersonnTable.Add(new Personn(2, "Marje", "Simpson", 40) { DeliveryAddressID = 1, BillingAddressID = 2, PetID = 1 });
			testDatabase.PersonnTable.Add(new Personn(3, "Bart", "Simpson", 10) { DeliveryAddressID = 1, PetID = 2 });
			testDatabase.PersonnTable.Add(new Personn(4, "Liza", "Simpson", 9) { DeliveryAddressID = 1, PetID = 2 });

			testDatabase.AddressTable.Add(new Address(1, "Home") { Number = 123 });
			testDatabase.AddressTable.Add(new Address(2, "School") { Number = 44 });
			testDatabase.AddressTable.Add(new Address(3, "Work") { Number = 55 });

			testDatabase.PetTable.Add(new Pet(1, "Cat"));
			testDatabase.PetTable.Add(new Pet(2, "Dog"));
			testDatabase.PetTable.Add(new Pet(3, "Turtle"));

			testDatabaseViewModel = new TestDatabaseViewModel(new TestDatabaseModel(testDatabase));
			this.DataContext= testDatabaseViewModel;
		}


	}
}