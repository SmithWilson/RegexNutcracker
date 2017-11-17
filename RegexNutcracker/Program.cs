using RegexNutcracker.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexNutcracker
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			

			#region Fields
			var _path = Directory.GetCurrentDirectory();
			var _modelFile = "model.txt";
			var _unEncodedFile = "unEncoded.txt";
			var _outputFile = "output.txt";
			#endregion

			await FileService.DeleteFromFile(_path, _outputFile);

			var models = await FileService.ReadFromFile(_path, _modelFile);
			var unEncoded = await FileService.ReadFromFile(_path, _unEncodedFile);

			var Observer = new Stopwatch();
			Observer.Start();

			var Encoded = new List<string>(unEncoded.Count);

			for (var i = 0; i < unEncoded.Count; i++)
			{
				Encoded.Add(unEncoded[i].StringToRegex(models[i]));
				Debugger.Log(1, "line", $"{i}\n");
			}
			Observer.Stop();
			Console.WriteLine($"Создание регулярных выражений : {Observer.ElapsedMilliseconds} ms");

			await FileService.WriteToFile(_path, _outputFile, Encoded);

			Console.ReadKey();
		}
	}
}
