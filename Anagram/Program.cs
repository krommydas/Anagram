using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagram
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Configuration conf = new Configuration(args);

				IOHandler dataHandler = new IOHandler(conf.FilesLocation);

				if (conf.InputMode == Configuration.IOMode.Console)
					throw new NotSupportedModeException();

				IEnumerable<String> words = dataHandler.ImportWordsFromFile(conf.InputFile);
				if (words == null)
					throw new FileProblemException(conf.InputFile);

				IEnumerable<IEnumerable<String>> anagrams = FindAnagrams(words);

				IEnumerable<String> lengthiest = anagrams.OrderBy(x => x.Count()).Last();
				IEnumerable<String> longest = anagrams.OrderBy(x => x.First().Count()).Last();

				switch(conf.OutputMode)
				{
					case Configuration.IOMode.Console:
						dataHandler.ExportWordSetsToConsole(anagrams, longest, lengthiest);
						break;
					case Configuration.IOMode.File:
						Boolean success = dataHandler.ExportWordSetsToFile(anagrams, longest, lengthiest, conf.OutputFile);
						if (!success)
							throw new FileProblemException(conf.OutputFile);
						break;
					default:
						throw new NotSupportedModeException();
				}

				Console.WriteLine("Success");
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private static IEnumerable<IEnumerable<String>> FindAnagrams(IEnumerable<String> words)
		{
			Dictionary<int, List<String>> anagrams = new Dictionary<int, List<string>>();

			foreach (String word in words)
			{
				String sorted = new String(word.OrderBy(x => x).ToArray());
				int hashed = sorted.GetHashCode();

				List<String> belongingSet;
				if (!anagrams.TryGetValue(hashed, out belongingSet))
				{
					belongingSet = new List<string>();
					anagrams.Add(hashed, belongingSet);
				}

				belongingSet.Add(word);
			}

			return anagrams.Values;
		}
	}
}
