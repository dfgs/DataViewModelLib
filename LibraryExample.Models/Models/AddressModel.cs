using BlueprintLib.Attributes;
using DataModelLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryExample.Models
{

	[TableModel, DTO("Address"),  Blueprint("TableModel"), Using("LibraryExample.Tables")]
	public partial class AddressModel
	{

		


	}
}
