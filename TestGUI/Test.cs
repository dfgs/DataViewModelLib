using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestGUI
{
	internal class Test : IEnumerable<int>, INotifyPropertyChanged
	{
		private List<int> items=new List<int>();

		public event PropertyChangedEventHandler? PropertyChanged;

		public IEnumerator<int> GetEnumerator()
		{
			return items.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}
	}
}
