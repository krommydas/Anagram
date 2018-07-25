using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Anagram
{
	public class IOHandler
	{
		public IOHandler() { }

		public IOHandler(string filesLocation)
		{
			this.filesLocation = filesLocation;
		}

		#region Files Handling

		private String filesLocation;
		private String outputFileName = "Results.txt";

		public IEnumerable<String> ImportWordsFromFile(string fileName)
		{
			try
			{
				return File.ReadLines(this.FilePath(fileName));
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
		}


		public Boolean ExportWordSetsToFile(IEnumerable<IEnumerable<String>> sets,
			IEnumerable<String> longest, IEnumerable<String> lengthiest)
		{
			return this.ExportWordSetsToFile(sets, longest, lengthiest, null);
		}

		public Boolean ExportWordSetsToFile(IEnumerable<IEnumerable<String>> sets, 
			IEnumerable<String> longest, IEnumerable<String> lengthiest, String fileName)
		{	
			try
			{
				IEnumerable<String> lines = this.ConcatenateWordsForMany(sets).Concat(this.GetAdditionalLines(longest, lengthiest));
				String outputFile = String.IsNullOrEmpty(fileName) ? this.outputFileName : fileName;

				File.WriteAllLines(this.FilePath(outputFile), lines);
				return true;
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}

		}

		#endregion

		#region Console Handling

		// TODO: Reading words from console

		public void ExportWordSetsToConsole(IEnumerable<IEnumerable<String>> sets,
			IEnumerable<String> longest, IEnumerable<String> lengthiest)
		{
			IEnumerable<String> lines = this.ConcatenateWordsForMany(sets).Concat(this.GetAdditionalLines(longest, lengthiest));

			foreach (var line in lines)
				Console.WriteLine(line);
		}

		#endregion

		#region Helpers

		private IEnumerable<String> ConcatenateWordsForMany(IEnumerable<IEnumerable<String>> sets)
		{
			List<String> lines = new List<string>();

			foreach (var set in sets)
				lines.Add(this.ConcatenateWords(set));

			return lines;
		}

		private String ConcatenateWords(IEnumerable<String> words)
		{
			String line = String.Empty;

			foreach (var word in words)
				line += ' ' + word;

			return line;
		}

		private String FilePath(string fileName)
		{
			return String.IsNullOrEmpty(filesLocation) ? fileName : filesLocation + '\\' + fileName;
		}

		private IEnumerable<String> GetAdditionalLines(IEnumerable<String> longest, IEnumerable<String> lengthiest)
		{
			List<String> result = new List<String>();

			String line = "Longest: " + this.ConcatenateWords(longest);
			result.Add(line);

			line = "Most Words: " + this.ConcatenateWords(lengthiest);
			result.Add(line);

			return result;
		}

		#endregion
	}
}
