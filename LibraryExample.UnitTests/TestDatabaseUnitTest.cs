using DataModelLib;
using LibraryExample;
using LibraryExample.Models;
using LibraryExample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintLib.Attributes;
using DataLib;

namespace LibraryExample.UnitTests
{
	[DatabaseUnitTest, DTO("TestDatabase"),  Blueprint("DatabaseModel.UnitTest.Mock"), Blueprint("DatabaseViewModel.UnitTest.*"), TestClass, Using("LibraryExample.Tables"), Using("LibraryExample.Models"), Using("LibraryExample.ViewModels")]
	public partial class TestDatabaseUnitTest
	{

	
	}

}