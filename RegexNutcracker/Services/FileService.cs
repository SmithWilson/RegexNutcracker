using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexNutcracker.Services
{
	public static class FileService
	{
		/// <summary>
		/// Считывание из файла.
		/// </summary>
		/// <param name="path">Путь.</param>
		/// <param name="fileName">Имя файла.</param>
		/// <returns></returns>
		public static Task<List<string>> ReadFromFile (string path, string fileName)
		{
			return Task.Run(() =>
			{
				var Observer = new Stopwatch();
				Observer.Start();
				using (var sr = new StreamReader(path + "/" + fileName, Encoding.Default))
				{
					if (!sr.BaseStream.CanRead)
					{
						Console.WriteLine($"Ошибка чтения файла {path}/{fileName}.");
						return new List<string>();
					}

					var input = new List<string>();
					while (!sr.EndOfStream)
					{
						input.Add(sr.ReadLine());
					}

					Observer.Stop();
					Console.WriteLine($"Считывание из файла {fileName} : {Observer.ElapsedTicks} ms");

					return input;
				}
			});
		}

		/// <summary>
		/// Запись в файл.
		/// </summary>
		/// <param name="path">путь к файлу.</param>
		/// <param name="fileName">Имя файла.</param>
		/// <param name="encoded">Закодированые строки.</param>
		/// <returns></returns>
		public static Task WriteToFile(string path, string fileName, List<string> encoded)
		{
			return Task.Run(() =>
			{
				var Observer = new Stopwatch();
				Observer.Start();
				using (var sw = new StreamWriter(path + "/" + fileName, false, Encoding.Default))
				{
					if (!sw.BaseStream.CanWrite)
					{
						Console.WriteLine($"Ошибка записи в файл {path}/{fileName}.");
						return;
					}

					foreach (var line in encoded)
					{
						sw.WriteLine(line);
					}
				}
				Observer.Stop();
				Console.WriteLine($"Запись в файл {fileName} : {Observer.ElapsedTicks} ms");
			});
		}

		public static Task DeleteFromFile(string path, string fileName)
		{
			return Task.Run(() =>
			{

				var Observer = new Stopwatch();
				Observer.Start();
				using (var sw = new StreamWriter(new FileStream(path + "/" + fileName, FileMode.Create, FileAccess.Write)))
				{

				}
				Observer.Stop();

				Console.WriteLine($"Удаление из файла {fileName} : {Observer.ElapsedMilliseconds} ms");
			});
		}
	}
}
