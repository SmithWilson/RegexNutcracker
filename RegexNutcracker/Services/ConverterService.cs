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
		/// <summary>
		/// Преобразует входные данные в регулярное выражение.
		/// Распознает . - + № , / * " пробел ( )
		/// </summary>
		/// <param name="value"></param>
		/// <param name="model"></param>
		/// <returns></returns>
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

			if (value != model && !String.IsNullOrWhiteSpace(value) && !String.IsNullOrWhiteSpace(model))
			{
				pattern = $"({value.ParseBracket()})|({model.ParseBracket()})";
			}
			else if (String.IsNullOrWhiteSpace(value))
			{
				pattern = model.ParseBracket();
			}
			else
			{
				pattern = value.ParseBracket();
			}

			var result = pattern
				.TrimStart()
				.Trim()
				.TrimEnd()
				.Insert(0, "#")
				.Insert(pattern.Length + 1, "#")
				.Replace("-", @"(#|\-)?")
				.Replace("/", @"(#|\/|\-)?")
				.Replace(" ", @"(#|\-)?")
				.Replace("_", @"(#|\_)?")
				.Replace("&", @"(#|\&)?")
				.Replace(".", @"(#|\.|\-)?")
				.Replace(",", @"(#|\,)?")
				.Replace("+", @"(#|\+|\-)?")
				.Replace("№", @"(#|\№)?")
				.Replace("\"", @"(#|\"")?")
				.Replace("*", @"(#|\*)?");
			
			return result;
		}

		private static string ParseBracket(this string value)
		{
			return value
				.Replace(")", @"(#|\))?")
				.Replace("(", @"(#|\()?")
				.Replace("(#|\\()?#|\\))?", @"(#|\))?");
		}
	}
}
