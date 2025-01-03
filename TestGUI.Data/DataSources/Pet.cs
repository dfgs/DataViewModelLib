﻿using System;
using System.Collections.Generic;
using System.Text;
using DataModelLib.Common;
using System.Diagnostics.CodeAnalysis;

namespace TestGUI.Data.DataSources
{
	[Table]
	public class Pet
	{
		[Column,PrimaryKey]
		public byte PetID { get; set; }

		[Column]
		public string Name { get; set; }
		
		public Pet(byte PetID, string Name)
		{
			this.PetID = PetID;this.Name = Name;
		}

		public override string ToString()
		{
			return $"{Name}";
		}
	}
}
