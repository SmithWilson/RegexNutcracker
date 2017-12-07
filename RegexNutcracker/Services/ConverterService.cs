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
			else if (String.IsNullOrWhiteSpace(value))
			{
				pattern = model;
			}
			else
			{
				pattern = value;
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


        #region Non-public Methods
        private static string ParseBracket(this string value)
        {
            var charCase = new List<string> { ".", "-", "+", "№", ",", "/", "\"", "*", "(", ")", "®" };
            var flag = false;
            foreach (var item in charCase)
            {
                if (value.Contains(item))
                {
                    flag = true;
                    break;
                }
            }

            value = value
                .Replace(")", @"(#|\))?")
                .Replace("(", @"(#|\()?")
                .Replace("(#|\\()?#|\\))?", @"(#|\))?");

            return flag ? value : "";
        } 
        #endregion
    }
}
