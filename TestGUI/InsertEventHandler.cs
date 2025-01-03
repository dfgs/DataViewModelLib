using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGUI
{
	public class InsertEventArg
	{
		public bool Canceled
		{
			get;
			set;
		}
		public object? Item
		{
			get;
			set;
		}
		
		public InsertEventArg()
		{
			Canceled = true;
			Item = null;
		}
	}

	public delegate void InsertEventHandler(object sender, InsertEventArg e);
}
