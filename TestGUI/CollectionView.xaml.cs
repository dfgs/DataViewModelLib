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
using DataViewModelLib;

namespace TestGUI
{
	/// <summary>
	/// Logique d'interaction pour PersonnView.xaml
	/// </summary>
	public partial class CollectionView : UserControl
	{
		public event InsertEventHandler? Insert;

		public CollectionView()
		{
			InitializeComponent();
		}

		private void DeleteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute=(DataContext is IRemoveViewModelCollection) && (listBox?.SelectedItem!=null);
        }

		private void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			IRemoveViewModelCollection? collection;

			collection=DataContext as IRemoveViewModelCollection;
			if (collection==null) return;
			collection.Remove(listBox.SelectedItem);
        }

		private void InsertCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = Insert!= null;
		}

		private void InsertCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			InsertEventArg insertEvent;

			insertEvent = new InsertEventArg();
			if (Insert!= null) Insert(this,insertEvent);

			if ((insertEvent.Canceled) || (insertEvent.Item == null)) return;

			IAddViewModelCollection? collection;
			collection=DataContext as IAddViewModelCollection;
			if (collection == null) return;
			collection.Add(insertEvent.Item);
		}

	}
}
