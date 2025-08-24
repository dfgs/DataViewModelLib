using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataViewModelLib
{
	public abstract class TextViewModelProperty : IViewModelProperty
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
		public TextViewModelProperty(string Name, bool IsReadOnly)
		{
			this.Name = Name;
			this.IsReadOnly = IsReadOnly;
		}
	}
	public class TextViewModelProperty<PropertyT> : TextViewModelProperty
	{
		private Func<PropertyT> getter;
		private Action<PropertyT> setter;



		public PropertyT Value
		{
			get => getter();
			set => setter(value);
		}

		public TextViewModelProperty(string Name, bool IsReadOnly, Func<PropertyT> Getter, Action<PropertyT> Setter) : base(Name, IsReadOnly)
		{
			this.getter = Getter;
			this.setter = Setter;
		}
	}
}
