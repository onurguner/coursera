using System;
using System.Collections.Generic;

namespace sorting 
{
	class Program
	{
		static Random rnd = new Random();
		
		static void swap(int[] a, int i, int j)
		{
			int t = a[i];
			a[i] = a[j];
			a[j] = t;
		}
		
		static int[] partition3(int[] a, int l, int r) {
		  //write your code here
		  int key = a[l];
		  int m1 = l, m2 = r;
		  int i = l;
		  while (i <= m2)
		  {
			  if (a[i] < key)
			  {
				  swap(a, m1, i);
				  m1++;
				  i++;
			  }
			  else if (a[i] > key)
			  {
				  swap(a, i, m2);
				  m2--;
			  }
			  else
			  {
				  i++;
			  }
		  }
		  
		  int[] m = {m1, m2};
		  return m;
		}
		
		static int partition2(int[] a, int l, int r) {
			int x = a[l];
			int j = l, t = 0;
			for (int i = l + 1; i <= r; i++) {
				if (a[i] <= x) {
					j++;
					t = a[i];
					a[i] = a[j];
					a[j] = t;
				}
			}
			t = a[l];
			a[l] = a[j];
			a[j] = t;
			return j;
		}
		
		static void randomizedQuickSort(int[] a, int l, int r) {
			if (l >= r) {
				return;
			}
			int k = rnd.Next(l, r + 1);
			swap(a, l, k);			
			//use partition3
			int[] m = partition3(a, l, r);
			//int m = partition2(a, l, r);
			randomizedQuickSort(a, l, m[0] - 1);
			randomizedQuickSort(a, m[1] + 1, r);
		}
		
		static void Main(string[] args)
        {
			int n = int.Parse(Console.ReadLine());		
			var tokens = Console.ReadLine().Split(' ');
			int[] a  = new int[n];
			for (int i = 0; i < n; i++)
			{
				a[i] = int.Parse(tokens[i]);
			}
			
			randomizedQuickSort(a, 0, n - 1);
			for (int i = 0; i < n; i++) {
				Console.Write(a[i] + " ");
			}
        }		
	}
}