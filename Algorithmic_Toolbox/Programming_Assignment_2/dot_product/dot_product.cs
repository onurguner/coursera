using System;

namespace dot_product 
{
	class Program
	{
		static long maxDotProduct(int[] a, int[] b) {
			//write your code here
			Array.Sort(a);
			Array.Sort(b);
			
			long result = 0;
			for (int i = 0; i < a.Length; i++) {
				result += (long)a[i] * (long)b[i];
			}
			return result;
		}
		
		static void Main(string[] args)
        {
			var input = Console.ReadLine();
			int n = int.Parse(input);
			
			input = Console.ReadLine();
			var tokens = input.Split(' ');
			int[] a = new int[n];
			for (int i = 0; i < n; i++) 
			{
				a[i] = int.Parse(tokens[i]);
			}
			
			input = Console.ReadLine();
			tokens = input.Split(' ');
			int[] b = new int[n];
			for (int i = 0; i < n; i++) 
			{
				b[i] = int.Parse(tokens[i]);
			}
			Console.WriteLine(maxDotProduct(a, b));
        }		
	}
}