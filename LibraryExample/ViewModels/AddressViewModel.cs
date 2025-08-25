using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintLib.Attributes;

namespace LibraryExample.ViewModels
{
	[DTO("Address"),Blueprint("TableViewModel"), Blueprint("TableViewModelCollection"), Using("LibraryExample.Models")]
	public partial class AddressViewModel
	{
	}
}
