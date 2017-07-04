using System;

namespace fibonacci_huge
{
	class Program
	{
		static long get_pisano_period(long m) {
			long a = 0, b = 1, c = a + b;
			for (int i = 0; i < m * m; i++) {
				c = (a + b) % m;
				a = b;
				b = c;
				if (a == 0 && b == 1) return i + 1;
			}
			return 1;
		}

		static long getFibonacciHuge(long n, long m) 
		{
			if (n <= 1)
				return n;
			
			long period = get_pisano_period(m);			
			n = n % period;
			
			if (n <= 1)
				return n;

			long previous = 0;
			long current  = 1;

			for (long i = 0; i < n - 1; ++i) {
				long tmp_previous = previous;
				previous = current;
				current = (tmp_previous + current) % m;
			}

			return current % m;
		}

		static void Main(string[] args)
		{
			var input = Console.ReadLine();
            var tokens = input.Split(' ');
			long n = long.Parse(tokens[0]);
			long m = long.Parse(tokens[1]);
			
			Console.WriteLine(getFibonacciHuge(n, m));			
		}
	}	
}