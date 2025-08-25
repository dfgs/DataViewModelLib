using System;
using System.Collections.Generic;
using DataModelLib;
using BlueprintLib.Attributes;

namespace LibraryExample.ViewModels
{
	[DTO("TestDatabase"),  Blueprint("DatabaseViewModel"), Blueprint("RelationViewModelCollection"),Using("LibraryExample.Models")]
	public partial class TestDatabaseViewModel
	{



	}
}
