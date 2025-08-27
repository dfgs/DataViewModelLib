using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryExample;
using LibraryExample.Models;
using LibraryExample.Tables;

namespace TestGUI
{
	/// <summary>
	/// Logique d'interaction pour PersonnView.xaml
	/// </summary>
	public partial class PersonnView : UserControl
	{
		public PersonnView()
		{
			InitializeComponent();
		}

		private void CollectionView_Insert(object sender, InsertEventArg e)
		{
			byte id;
			id = (byte)(ViewModels.TestDatabaseViewModel.PersonnViewModelCollection.Select(item => item.PersonnID).Max() + 1);
			e.Item = new Personn(id, "First name", "Last name", 30);
			e.Canceled = false;
		}


	}
}
