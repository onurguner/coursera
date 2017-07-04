using System;

namespace change 
{
	class Program
	{
		static int getChange(int m) 
		{
			int[] coins = {10, 5, 1};
			int index = 0, count = 0;			
			while (m > 0)
			{
				while (coins[index] > m)
					index++;
				
				m = m - coins[index];
				count++;
			}			
			return count;
		}
		
		static void Main(string[] args)
        {
			var input = Console.ReadLine();
			int m = int.Parse(input);
			Console.WriteLine(getChange(m));
        }		
	}
}