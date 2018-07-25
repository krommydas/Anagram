using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagram
{
	public class Configuration
	{
		public enum IOMode : short
		{
			Console = 0,
			File = 1
		}

		public IOMode InputMode { get; private set; }
		public IOMode OutputMode { get; private set; }

		public String InputFile { get; private set; }
		public String OutputFile { get; private set; }

		public String FilesLocation { get; private set; }

		public Configuration(String[] arguments)
		{
			if (arguments.Length == 0)
				throw new MissingArgumentsException();

			try
			{
				short count = 0;
				this.InputMode = (IOMode)short.Parse(arguments[count++]);

				if (this.InputMode == IOMode.File)
					this.InputFile = arguments[count++];

				this.OutputMode = (IOMode)short.Parse(arguments[count++]);

				if (this.OutputMode == IOMode.File)
					this.OutputFile = arguments[count++];

				if ((this.InputMode == IOMode.File || this.OutputMode == IOMode.File) && arguments.Length == count + 1)
					this.FilesLocation = arguments[count];
			}
			catch(Exception e)
			{
				if (e is InvalidCastException)
					throw new NotValidArgumentsException();
				else if (e is IndexOutOfRangeException)
					throw new MissingArgumentsException();
				else throw e;
			}
		}
	}
}
