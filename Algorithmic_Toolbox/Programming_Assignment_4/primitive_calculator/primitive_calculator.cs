using System;
using System.Collections.Generic;

namespace primitive_calculator
{
	class Program {
		static void Main(string[] args)
		{
			int n = int.Parse(Console.ReadLine());
			int[] C = new int[n + 1];
			int min = get_minimum_number_of_operations(C, n);
			Console.WriteLine(min);
			int[] seq = new int[min + 1];
			get_sequence(C, n, seq, min);
			foreach (int item in seq)
				Console.Write(item + " ");
			Console.WriteLine("");
		}	

		static int get_minimum_number_of_operations(int[] C, int n) {		
			
			for (int i=2; i<=n; ++i) {				
				if (i%2==0 && i%3==0) {
					C[i] = Math.Min(C[i/2] + 1, C[i/3] + 1);
					C[i] = Math.Min(C[i], C[i-1] + 1);
				}
				else if (i%2==0) {
					C[i] = Math.Min(C[i/2] + 1, C[i-1] + 1);
				}
				else if (i%3==0) {
					C[i] = Math.Min(C[i/3] + 1, C[i-1] + 1);
				}
				else {
					C[i] = C[i-1] + 1;
				}
			}
			
			return C[n];
		}

		static void get_sequence(int[] C, int n, int[] seq, int m) {			
			int index = m;
			seq[index--] = n;
			
			while (n > 1) {
				if (n%2==0 && n%3==0) {
					if (C[n/2] <= C[n/3])
						n = n/2;
					else if (C[n/3] <= C[n-1])
						n = n/3;
					else
						n = n-1;
				} else if (n%2==0) {
					if (C[n/2] <= C[n-1])
						n = n/2;
					else
						n = n-1;
				} else if (n%3==0) {
					if (C[n/3] <= C[n-1])
						n = n/3;
					else
						n = n-1;
				} else {
					n = n-1;						
				}
				seq[index--] = n;
			}
		}
	}
}

