using System;

namespace fibonacci_sum_last_digit
{
	class Program
	{
		static long get_fibonacci_sum_last_digit(long n) 
		{
			long pisano = 60;
			n = (n + 2) % pisano;
			
			if (n == 0)
				return 9;
			if (n == 1)
				return 0;
			
			long previous = 0;
			long current  = 1;

			for (long i = 0; i < n - 1; ++i) {
				long tmp_previous = previous;
				previous = current;
				current = tmp_previous + current;
			}
			return (current - 1) % 10;
		}

		static void Main(string[] args)
		{
			long n = long.Parse(Console.ReadLine());
			Console.WriteLine(get_fibonacci_sum_last_digit(n));			
		}
	}	
}