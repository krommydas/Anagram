using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagram
{
	public class MissingArgumentsException : Exception
	{
		public override string Message => "Missing Arguments";
	}

	public class NotValidArgumentsException : Exception
	{
		public override string Message => " Not Valid Arguments";
	}

	public class NotSupportedModeException : Exception
	{
		public override string Message => "Not Supported Mode";
	}

	public class FileProblemException : Exception
	{
		public FileProblemException(String fileName) : base()
		{
			exceptionFile = fileName;
		}

		public FileProblemException() : base() { }

		private String exceptionFile;

		public override string Message => "There was a problem with the file: " + exceptionFile;
	}
}
