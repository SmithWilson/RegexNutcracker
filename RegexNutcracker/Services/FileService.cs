using System;
using System.Collections.Generic;
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
				using (var sw = new StreamWriter(path + "/" + fileName, false, Encoding.UTF8))
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
			});
		}
	}
}
