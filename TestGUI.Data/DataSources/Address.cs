﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using DataModelLib.Common;

namespace TestGUI.Data.DataSources
{
	[Table]
	public class Address 
	{
		[Column,PrimaryKey]
		public byte AddressID { get; set; }

		[Column]
		public string Street { get; set; }
		[Column]
		public byte? Number { get; set; }

		public Address(byte AddressID, string Street)
		{
			this.AddressID = AddressID; this.Street = Street;
		}

		public override string ToString()
		{
			return $"{Number} {Street}";
		}


	}
}
