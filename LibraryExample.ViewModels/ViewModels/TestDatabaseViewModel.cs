using System;
using System.Collections.Generic;
using DataModelLib;
using BlueprintLib.Attributes;
using DataViewModelLib;

namespace LibraryExample.ViewModels
{
	[DatabaseViewModel, DTO("TestDatabase"),  Blueprint("DatabaseViewModel"), Blueprint("RelationViewModelCollection")]
	public partial class TestDatabaseViewModel
	{



	}
}
