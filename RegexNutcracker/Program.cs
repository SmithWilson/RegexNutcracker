using RegexNutcracker.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegexNutcracker
{
	public class Program
	{
        [STAThread]
        public static void Main(string[] args)
		{
            #region Fields
            var flag = WorkMode.Single;
			var _path = Directory.GetCurrentDirectory();
			var _modelFile = "model.txt";
			var _unEncodedFile = "unEncoded.txt";
			var _outputFile = "output.txt";
            #endregion

            flag = Modes(flag);

            if (flag == WorkMode.Single)
            {
                Single();
            }
            else if (flag == WorkMode.Multy)
            {
                Multy(_path, _outputFile, _modelFile, _unEncodedFile);
            }
            else
            {
                ManyDictionaries();
            }

            Console.ReadKey();
		}

        public static WorkMode Modes(WorkMode flag)
        {
            Console.Write("Выбор режима F1 - одиночный,\n F2 - множественная выборка,\n F3 - работа с множеством словарей : ");
            ConsoleKeyInfo consoleKey = Console.ReadKey();
            switch (consoleKey.Key)
            {
                case ConsoleKey.F1:
                    Console.WriteLine("Приложение переведено в одиночный режим");
                    return WorkMode.Single;
                case ConsoleKey.F2:
                    Console.WriteLine("Приложение переведено в режим множественной выборки.");
                    return WorkMode.Multy;
                case ConsoleKey.F3:
                    Console.WriteLine("Приложение переведено в режим создание из нескольких словарей.");
                    return WorkMode.ManyDictionaries;
                default:
                    return WorkMode.Single;
            }
        }

        public static void Single()
        {
            var value = "";
            while (1 > 0)
            {
                Console.Write("\nДля выхода введите !@# для получения регулярного выражения введите строку : ");
                value = Console.ReadLine();

                value = value.StringToRegex();

                Clipboard.Clear();

                Console.WriteLine($"Регулярное выражение : {value}\nПомещено в буфер обмена.");
                Clipboard.SetText(value.ToString());
            }
        }

        private static void ManyDictionaries()
        {
            var value = "";
            while (1 > 0)
            {
                Console.Write("\nДля выхода введите !@# для получения регулярного выражения введите строку : ");
                value = Console.ReadLine();
                var str = value.Split('|');
                value = string.Empty;
                foreach (var item in str)
                {
                    value += $"({item.TrimStart().Trim().TrimEnd()})";
                    if (!(str.Last() == item))
                    {
                        value += "|";
                    }
                }

                var result = value
               .TrimStart()
               .Trim()
               .TrimEnd()
               .Insert(0, "#")
               .Insert(value.Length + 1, "#")
               .Replace("-", @"(#|\-)?")
               .Replace("!", @"(#|\!)?")
               .Replace(":", @"(#|\:)?")
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
               .Replace("®", @"(#|\®)?")
               .Replace("'", @"(#|\'|\-)?")
               .Replace("`", @"(#|\`|\-)")
               .Replace("’", @"(#|\’|\-)?");

                Clipboard.Clear();

                Console.WriteLine($"Регулярное выражение : {result}\nПомещено в буфер обмена.");
                Clipboard.SetText(result.ToString());
            }
        }

        public async static void Multy(string _path, string _outputFile, string _modelFile, string _unEncodedFile)
        {
            await FileService.DeleteFromFile(_path, _outputFile);

            var models = await FileService.ReadFromFile(_path, _modelFile);
            var unEncoded = await FileService.ReadFromFile(_path, _unEncodedFile);

            var count = Math.Max(models.Count, unEncoded.Count);

            if (models.Count == 0)
            {
                for (var i = 0; i < count; i++)
                {
                    models.Add("");
                }
            }

            if (unEncoded.Count == 0)
            {
                for (var i = 0; i < count; i++)
                {
                    unEncoded.Add("");
                }
            }

            var Observer = new Stopwatch();
            Observer.Start();

            var Encoded = new List<string>(count);


            try
            {
                for (var i = 0; i < count; i++)
                {
                    Encoded.Add(models[i].StringToRegex(unEncoded[i]));
                    Debugger.Log(1, "line", $"{i}\n");
                }
            }
            catch (Exception)
            {
                Console.WriteLine(
                    "\n* * * * * * * * * * * * * * * * * \n" +
                    "Разное количество входных данных." +
                    "\n* * * * * * * * * * * * * * * * *\n");
            }


            Observer.Stop();
            Console.WriteLine($"Создание регулярных выражений : {Observer.ElapsedMilliseconds} ms");

            await FileService.WriteToFile(_path, _outputFile, Encoded);

            Console.WriteLine("\nВсе готово. Нажмите любую клавишу, чтобы выйти.");

            Process.Start("notepad.exe", $@"{_path}\{_outputFile}");

            Console.ReadKey();
        }
    }
}
