using System;
using System.Diagnostics.Contracts;

namespace Test
{
	public class Example
	{
		public static void Main(string[] arg)
		{
			//Contract.Assume(args != null);
			//Contract.Requires<Exception>(arg != null);
			Contract.Requires<Exception>(arg == null);			
			//Contract.Ensures(Contract.Result<string>() != null);

			//return "Test";
		}
	}
}
