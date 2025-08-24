using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataViewModelLib
{
	public abstract class ComboBoxViewModelProperty : IViewModelProperty
	{

		public string Name
		{
			get;
			private set;
		}
		public bool IsReadOnly
		{
			get;
			private set;
		}
		public ComboBoxViewModelProperty(string Name, bool IsReadOnly)
		{
			this.Name = Name;
			this.IsReadOnly = IsReadOnly;
		}
	}
	public class ComboBoxViewModelProperty<PropertyT> : ComboBoxViewModelProperty
	{
		private Func<PropertyT> getter;
		private Action<PropertyT> setter;
		private Func<object> itemsSourceGetter;

		public string SelectedValuePath
		{
			get;
			private set;
		}

		public object ItemsSource
		{
			get => itemsSourceGetter();
		}


		public PropertyT Value
		{
			get => getter();
			set => setter(value);
		}

		public ComboBoxViewModelProperty(string Name, bool IsReadOnly, Func<object> ItemsSourceGetter, string SelectedValuePath, Func<PropertyT> Getter, Action<PropertyT> Setter) : base(Name, IsReadOnly)
		{
			this.getter = Getter;
			this.setter = Setter;
			this.itemsSourceGetter = ItemsSourceGetter;
			this.SelectedValuePath = SelectedValuePath;
		}
	}
}
