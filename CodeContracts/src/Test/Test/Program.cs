using System;

namespace Test
{
	class Program
	{		
		static void Main(string[] args)
		{
			DoSomethingCool(null);
			
			var res = ReturnSomethingCool();
			if (res == null)
			{
				throw new InvalidOperationException();
			}


		}

		static void DoSomethingCool(string arg)
		{
			if (arg == null)
			{
				throw new ArgumentNullException("arg");
			}
		}

		static string ReturnSomethingCool()
		{
			return null;
		}
	}
}
