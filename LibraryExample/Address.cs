using BlueprintLib.Attributes;
using DataModelLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryExample
{

	[DTO("Address"), Blueprint("DTO"), Blueprint("TableModel")]
	public partial class Address
	{

		public Address(byte AddressID, string Street)
		{
			this.AddressID = AddressID; this.Street = Street;
		}
		public override string ToString()
		{
			return $"{Number} {Street}";
		}


	}
}
