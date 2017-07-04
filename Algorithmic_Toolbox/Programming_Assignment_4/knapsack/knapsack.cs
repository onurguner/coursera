using System;

namespace knapsack
{
	class Program {
		static void Main(string[] args) {
			var tokens = Console.ReadLine().Split(' ');
			int W = int.Parse(tokens[0]);
			int n = int.Parse(tokens[1]);
			int[] w = new int[n];
			tokens = Console.ReadLine().Split(' ');
			for (int i=0; i<n; i++) {
				w[i] = int.Parse(tokens[i]);
			}
			Console.WriteLine(optimalWeight(W, w));			
		}
		
		static int optimalWeight(int W, int[] w) {
			int n = w.Length;
			int[,] values = new int[W + 1, n + 1];
			for (int i=1; i<=n; i++) {
				for (int j=1; j<=W; j++) {
					values[j, i] = values[j, i -1];
					if (w[i-1] <= j) {
						int val = values[j-w[i-1], i-1] + w[i-1];
						if (values[j, i] < val)
							values[j, i] = val;
					}
				}
			}
			return values[W,n];
		}
	}
}