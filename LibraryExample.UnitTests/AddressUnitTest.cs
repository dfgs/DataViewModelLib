using DataModelLib;
using DataViewModelLib;
using LibraryExample;
using LibraryExample.Models;
using LibraryExample.ViewModels;
using System.Collections.Specialized;
using BlueprintLib.Attributes;
using DataLib;

namespace LibraryExample.UnitTests
{
	[TableUnitTest, DTO("Address"), Blueprint("TableViewModel.UnitTest.*"), MockCount(4), TestClass, Using("LibraryExample.Tables"), Using("LibraryExample.Models"), Using("LibraryExample.ViewModels")]
	public partial class AddressUnitTest
	{

		


		
	}
}