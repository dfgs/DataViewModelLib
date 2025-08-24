using BlueprintLib.Attributes;
using DataModelLib;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Linq;

namespace LibraryExample
{
	[Table, DTO("Pet"), Blueprint("DTO"), Blueprint("TableModel")]
	public partial class Pet
	{

		public Pet(byte PetID, string Name)
		{
			this.PetID = PetID; this.Name = Name;
		}
		public override string ToString()
		{
			return $"{Name}";
		}


	}
}
