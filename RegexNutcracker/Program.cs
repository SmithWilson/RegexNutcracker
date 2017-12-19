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
            var flag = false;
			var _path = Directory.GetCurrentDirectory();
			var _modelFile = "model.txt";
			var _unEncodedFile = "unEncoded.txt";
			var _outputFile = "output.txt";
            #endregion

            Console.Write("Выбор режима F1 - одиночный, F2 - множественная выборка : ");
            ConsoleKeyInfo consoleKey = Console.ReadKey();
            switch (consoleKey.Key)
            {
                case ConsoleKey.F1: Console.WriteLine("Приложение переведено в одиночный режим");
                    flag = true;
                    break;
                case ConsoleKey.F2: Console.WriteLine("Приложение переведено в режим множественной выборки.");
                    flag = false;
                    break;
                default:
                    break;
            }

            if (flag)
            {
                Single();
            }
            else
            {
                Multy();
            }

            void Single()
            {
                var value = "";
                while (value != "!@#")
                {
                    Console.Write("\nДля выхода введите !@# для получения регулярного выражения введите строку : ");
                    value = Console.ReadLine();
                    value = value.StringToRegex();
                    Console.WriteLine($"Регулярное выражение : {value}\nПомещено в буфер обмена.");
                    Clipboard.SetText(value.ToString());
                }
            }

            async void Multy()
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
                Console.ReadKey();
            }
            Console.ReadKey();
		}
	}
}
