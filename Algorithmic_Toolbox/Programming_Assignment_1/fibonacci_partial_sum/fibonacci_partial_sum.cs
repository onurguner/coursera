using System;

namespace fibonacci_partial_sum
{
	class Program
	{
		static long get_fib(long n)
		{
			if (n <= 1)
				return n;
			
			long previous = 0;
			long current  = 1;

			for (long i = 0; i < n - 1; ++i) {
				long tmp_previous = previous;
				previous = current;
				current = tmp_previous + current;
			}
			return current;
		}
		
		static long get_fibonacci_partial_sum(long m, long n) 
		{
			long pisano = 60;
			n = (n + 2) % pisano;
			m = (m + 1) % pisano;
			
			long result = get_fib(n) - get_fib(m);
			return result % 10;
		}

		static void Main(string[] args)
		{
			var input = Console.ReadLine();
            var tokens = input.Split(' ');
			long m = long.Parse(tokens[0]);
			long n = long.Parse(tokens[1]);
			
			Console.WriteLine(get_fibonacci_partial_sum(m, n));			
		}
	}	
}