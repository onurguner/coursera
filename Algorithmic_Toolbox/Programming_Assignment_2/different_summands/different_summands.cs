using System;
using System.Collections.Generic;

namespace different_summands 
{
	class Program
	{
		static List<int> optimalSummands(int n) {
			List<int> summands = new List<int>();
			
			int l = 1;
			while (n > 0)
			{
				if (n <= 2*l)
				{
					summands.Add(n);
					n = 0;
				}
				else
				{
					summands.Add(l);
					n = n - l;
					l++;
				}
			}
			
			return summands;
		}
		
		static void Main(string[] args)
        {
			var input = Console.ReadLine();
			int n = int.Parse(input);
			
			List<int> summands = optimalSummands(n);
			Console.WriteLine(summands.Count);
			foreach (int summand in summands) 
			{
				Console.Write(summand + " ");
			}
        }		
	}
}