using System;
using System.Diagnostics.Contracts;

namespace Test
{	
	class Program
	{
		private class A
		{
			private decimal _val;

			public decimal Val
			{
				get { return _val; }
				set
				{
					Contract.Assume(value > 0);
					_val = value;
				}
			}

			//public void Decriment()
			//{
			//    if (_val > 1)
			//    {					
			//        _val--;					
			//    }
			//    Contract.Assume(_val > 0);
			//}

			[ContractInvariantMethod]
			private void ObjectInvariant()
			{
				Contract.Invariant(_val > 0);
			}			
		}

		static void Main(string[] args)
		{			
			Contract.Requires<ArgumentException>(args.Length == 0);
			Contract.Requires<ArgumentException>(args[0] != null);
			Contract.Assume(args[0] != null);
			DoSomethingCool(args[0]);

			//new A {Val = 1.0m};
		}

		static void Rec(bool res)
		{
			Contract.Requires<ArgumentException>(res);
		}

		public static void Test(bool t)
		{
			if (t)
			{
				throw new InvalidOperationException();
			}
		}

		static void DoSomethingCool(string arg)			
		{
			Contract.Requires(arg != null);

			Console.WriteLine(arg.Length);
		}

		static string ReturnSomethingCool()
		{
			//var res = ReturnSomethingCool();
			//if (res == null)
			//{
			//    throw new InvalidOperationException();
			//}

			//Contract.Ensures(Contract.Result<string>() != null);

			Console.WriteLine("Test");			

			return null;
		}
	}
}
