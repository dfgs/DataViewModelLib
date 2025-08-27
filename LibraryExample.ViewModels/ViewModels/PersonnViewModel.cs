using BlueprintLib.Attributes;
using DataViewModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryExample.ViewModels
{
	[TableViewModel, DTO("Personn"),Blueprint("TableViewModel"), Blueprint("TableViewModelCollection")]
	public partial class PersonnViewModel
	{
	}
}
