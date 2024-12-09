using System;

namespace DataViewModelLib
{
	public static class StringHelpers
	{

		public static string Indent(this string Input, byte Indents)
		{
			return $"{new string('\t', Indents)}{Input.Replace("\r\n", "\r\n" + new string('\t', Indents))}";
		}



	}
}
