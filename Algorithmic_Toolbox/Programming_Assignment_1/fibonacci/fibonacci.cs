using System;

namespace fibonacci
{
	class Program
	{
		static int fibonacci_naive(int n) 
		{
			if (n <= 1)
				return n;

			return fibonacci_naive(n - 1) + fibonacci_naive(n - 2);
		}

		static int fibonacci_fast(int n) 
		{
			if (n<= 1)
				return n;
			
			int[] fib = new int[n + 1];
			fib[0] = 0;
			fib[1] = 1;
			for (int i=2; i<=n; i++)
			{
				fib[i] = fib[i-1] + fib[i - 2];
			}

			return fib[n];
		}

		static void Main(string[] args)
		{
			int n = int.Parse(Console.ReadLine());
			Console.WriteLine(fibonacci_fast(n));			
		}
	}	
}