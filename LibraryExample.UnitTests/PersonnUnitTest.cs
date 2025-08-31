using DataModelLib;
using DataViewModelLib;
using LibraryExample.Models;
using LibraryExample.ViewModels;
using System.Collections.Specialized;
using BlueprintLib.Attributes;
using DataLib;

namespace LibraryExample.UnitTests
{
	[TableUnitTest, DTO("Personn"), Blueprint("TableViewModel.UnitTest.*"), MockCount(10), TestClass, Using("LibraryExample.Tables"), Using("LibraryExample.Models"), Using("LibraryExample.ViewModels")]
	public partial class PersonnUnitTest
	{





	}
}