using System;
using System.Collections.Generic;
using DataModelLib;
using BlueprintLib.Attributes;

namespace LibraryExample
{
	[Database, Blueprint("Database"), Blueprint("DatabaseModel"), Blueprint("DatabaseViewModel"), Blueprint("RelationViewModelCollection")]
	public partial class TestDatabase
	{



	}
}
