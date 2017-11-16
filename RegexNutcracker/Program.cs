using RegexNutcracker.Services;
using System;
using System.Collections.Generic;
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
			var Encoded = new List<string> { "hello", ",", "world", "." };

			#region Fields
			var _path = Directory.GetCurrentDirectory();
			var _modelFile = "model.txt";
			var _unEncodedFile = "unEncoded.txt";
			var _outputFile = "output.txt"; 
			#endregion

			var models = await FileService.ReadFromFile(_path, _modelFile);
			var unEncoded = await FileService.ReadFromFile(_path, _unEncodedFile);

			await FileService.WriteToFile(_path, _outputFile, Encoded);
		}
	}
}
