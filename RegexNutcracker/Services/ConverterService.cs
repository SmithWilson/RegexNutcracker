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
        private static List<string> _charCase = new List<string> { ".", "-", "+", "№", ",", "/", "\"", "*", "(", ")", "®", " " };

        /// <summary>
        /// Преобразует входные данные в регулярное выражение.
        /// Распознает . - + № , / * " пробел ( ) ®
        /// </summary>
        /// <param name="value"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string StringToRegex(this string value, string model)
        {
            #region Fields
            string pattern; 
            #endregion


            value = value
				.TrimStart()
				.Trim()
				.TrimEnd()
                .ParseBracket();

			model = model
				.TrimStart()
				.Trim()
				.TrimEnd()
                .ParseBracket();


			if (!String.IsNullOrWhiteSpace(value) && !String.IsNullOrWhiteSpace(model) && value.ToLower().Trim() != model.ToLower().Trim())
			{
				pattern = $"({value})|({model})";
			}
			else 
			{
                if (String.IsNullOrWhiteSpace(value))
                {
                    pattern = model;
                }
                else
                {
                    pattern = value;
                }
            }
			

            #region Main
            if (String.IsNullOrWhiteSpace(pattern) || pattern == "()|()")
            {
                return "";
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
                .Replace("*", @"(#|\*)?")
                .Replace("®", @"(#|\®)?"); 
            #endregion

            return result;
		}

        public static string StringToRegex(this string value)
        {
            #region Fields
            string pattern;
            #endregion

            if (value == "!@#")
            {
                return "Выход...";
            }

            value = value
                .TrimStart()
                .Trim()
                .TrimEnd()
                .ParseBracket();
            
            if (!String.IsNullOrWhiteSpace(value))
            {
                pattern = $"{value}";
            }
            else
            {
                return "Входная строка пустая или она содержит только пробелы между словами.";
            }


            #region Main
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
                .Replace("*", @"(#|\*)?")
                .Replace("®", @"(#|\®)?");
            #endregion

            return result;
        }
        #region Non-public Methods
        private static string ParseBracket(this string value)
        {
            var flag = false;
            foreach (var item in _charCase)
            {
                if (value.Contains(item))
                {
                    flag = true;
                    break;
                }
            }

            value = value
                .Replace("|", @"\|?")
                .Replace(")", @"(#|\))?")
                .Replace("(", @"#?(#|\()?")
                .Replace("(#|\\()?#|\\))?", @"(#|\))?");

            return flag ? value : "";
        } 
        #endregion
    }
}
