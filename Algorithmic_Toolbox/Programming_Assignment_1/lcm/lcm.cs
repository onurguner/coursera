using System;

namespace gcd
{
	class Program
	{
		static int get_gcd(int a, int b)
		{
			if (b == 0)
				return a;
			int remainder = a % b;
			return get_gcd(b, remainder);
		}
		
		static long get_lcm(int a, int b)
		{
			int gcd = get_gcd(a, b);
			return (long)((a / gcd) * (long)b);
		}

		static void Main(string[] args)
		{
			var input = Console.ReadLine();
			var tokens = input.Split(' ');
			
			int a = int.Parse(tokens[0]);
			int b = int.Parse(tokens[1]);
			
			Console.WriteLine(get_lcm(a, b));			
		}
	}	
	
}