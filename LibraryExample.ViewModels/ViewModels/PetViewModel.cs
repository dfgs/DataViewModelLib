using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintLib.Attributes;
using DataViewModelLib;

namespace LibraryExample.ViewModels
{
	[TableViewModel, DTO("Pet"),Blueprint("TableViewModel"), Blueprint("TableViewModelCollection"), Using("LibraryExample.Tables"), Using("LibraryExample.Models")]
	public partial class PetViewModel
	{
	}
}
