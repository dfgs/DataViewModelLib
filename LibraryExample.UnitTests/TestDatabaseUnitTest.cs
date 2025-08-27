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
	[DTO("TestDatabase"), Database, Blueprint("DatabaseModel.UnitTest.Mock"), Blueprint("DatabaseViewModel.UnitTest.*"), TestClass]
	public partial class TestDatabaseUnitTest
	{

	
	}

}