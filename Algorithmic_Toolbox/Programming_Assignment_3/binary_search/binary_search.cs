using System;
using System.Collections.Generic;

namespace binary_search 
{
	class Program
	{
		static int binary_search(int[] a, int x)
		{
			int left = 0, right = a.Length - 1, mid = 0; 
			//write your code here
			while (left <= right)
			{
				mid = left + (right - left) / 2;
				if (a[mid] == x)
					return mid;
				else if (x < a[mid])
					right = mid - 1;
				else
					left = mid + 1;
			}
			return -1;
		}
		
		static void Main(string[] args)
        {
			var tokens = Console.ReadLine().Split(' ');
			int n = int.Parse(tokens[0]);
			int[] a  = new int[n];
			for (int i = 0; i < n; i++)
			{
				a[i] = int.Parse(tokens[i + 1]);
			}
			
			tokens = Console.ReadLine().Split(' ');
			int m = int.Parse(tokens[0]);
			int[] b  = new int[m];
			for (int i = 0; i < m; i++)
			{
				b[i] = int.Parse(tokens[i + 1]);
			}
			
			for (int i = 0; i < m; i++)
			{
				Console.Write(binary_search(a, b[i]) + " ");
			}
        }		
	}
}