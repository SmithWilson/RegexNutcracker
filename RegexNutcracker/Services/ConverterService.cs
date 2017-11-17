using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexNutcracker.Services
{
	public static class ConverterService
	{
		public static string StringToRegex(this string value, string model)
		{
			string pattern;

			value = value
				.TrimStart()
				.Trim()
				.TrimEnd();

			model = model
				.TrimStart()
				.Trim()
				.TrimEnd();

			if (value != model)
			{
				pattern = $"({value})|({model})";
			}
			else
			{
				pattern = value;
			}

			// . - + №
			var result = pattern
				.TrimStart()
				.Trim()
				.TrimEnd()
				.Insert(0, "#")
				.Insert(pattern.Length + 1, "#")
				.Replace("-", @"(#|\-)?")
				.Replace("/", @"(#|\/|\-)?")
				.Replace(" ", @"(#|\-)?")
				.Replace(".", @"(#|\.|\-)?")
				.Replace(",", @"(#|\,)?")
				.Replace("+", @"(#|\+|\-)?")
				.Replace("№", @"(#|\№)?")
				.Replace("\"", @"(#|\"")?")
				.Replace("*", @"(#|\*)?");
			
			return result;
		}
	}
}
