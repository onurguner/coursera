using System;

namespace fractional_knapsack 
{
	class Program
	{
		static int getMax(int[] values, int[] weights)
		{
			double max = 0.0, curr = 0.0;
			int n = values.Length, index = 0;
			for (int i=0; i<n; i++)
			{
				if (weights[i] == 0)
					continue;
				
				curr = (double)values[i] / weights[i];
				if (max < curr) {
					max = curr;
					index = i;
				}
			}	
			return index;			
		}
		
		static double getOptimalValue(int capacity, int[] values, int[] weights) 
		{
			double val = 0.0;
			int maxIndex = 0, amount = 0;
			for (int i=0; i<values.Length; i++)
			{
				if (capacity == 0)
				{
					return val;
				}
				maxIndex = getMax(values, weights);
				amount = Math.Min(weights[maxIndex], capacity);
				val += amount * (double)values[maxIndex] / weights[maxIndex];
				weights[maxIndex] -= amount;
				capacity -= amount;
			}			
			return val;
		}
		
		static void Main(string[] args)
        {
			var input = Console.ReadLine();
			var tokens = input.Split(' ');
			int n = int.Parse(tokens[0]);
			int capacity = int.Parse(tokens[1]);
			int[] values = new int[n];
			int[] weights = new int[n];
			for (int i=0; i<n; i++)
			{
				input = Console.ReadLine();
				tokens = input.Split(' ');
				values[i] = int.Parse(tokens[0]);
				weights[i] = int.Parse(tokens[1]);
			}
			double val = getOptimalValue(capacity, values, weights);
			Console.WriteLine(val.ToString("0.00000"));
        }		
	}
}