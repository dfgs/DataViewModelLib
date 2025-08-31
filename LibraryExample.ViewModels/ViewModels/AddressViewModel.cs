using BlueprintLib.Attributes;
using DataViewModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryExample.ViewModels
{
	[TableViewModel, DTO("Address"),Blueprint("TableViewModel"), Blueprint("TableViewModelCollection"), Using("LibraryExample.Tables"), Using("LibraryExample.Models")]
	public partial class AddressViewModel
	{
	}
}
