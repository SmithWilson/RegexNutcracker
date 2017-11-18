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
		/// Распознает : . - + № , / * " пробел
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
				pattern = $"({value})|({model})";
			}
			else if (String.IsNullOrWhiteSpace(value))
			{
				pattern = model;
			}
			else
			{
				pattern = value;
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
