using System;
using System.Collections.Generic;

namespace connecting_points
{
	class Program {
		static void Main(string[] args) {
			int n = int.Parse(Console.ReadLine());
			int[] x = new int[n];
			int[] y = new int[n];
			for (int i = 0; i < n; i++) {
				var tokens = Console.ReadLine().Split(' ');
				x[i] = int.Parse(tokens[0]);
				y[i] = int.Parse(tokens[1]);
			}			
			Console.WriteLine(minimumDistance(x, y));
		}
		
		static double minimumDistance(int[] x, int[] y) {
			double result = 0.0;			
			int n = x.Length;
			
			List<int>[] adj = new List<int>[n];
			List<double>[] cost = new List<double>[n];
			for (int i = 0; i < n; i++) {
				adj[i] = new List<int>();
				cost[i] = new List<double>();
			}
			
			for (int i=0; i<n; i++) {
				for (int j=i+1; j<n; j++) {
					adj[i].Add(j);
					adj[j].Add(i);					
					double w = getDistance(x, y, i, j);
					cost[i].Add(w);
					cost[j].Add(w);
				}			
			}
			
			List<int> minQueue = new List<int>();
			double[] dist = new double[adj.Length];
			bool[] mst_vertex = new bool[adj.Length];
			for (int i=0; i<adj.Length; ++i) {				
				minQueue.Add(i);
				dist[i] = double.MaxValue;
			}
			dist[0] = 0.0; 
			
			while (minQueue.Count != 0) {	
				minQueue.Sort((v1, v2) => dist[v1] - dist[v2] >= 0 ? 1 : -1);			
				var v = minQueue[0];
				minQueue.Remove(v);
				
				mst_vertex[v] = true;
				result += dist[v];
				
				for (int i=0; i<adj[v].Count; ++i) {
					var z = adj[v][i];
					var w = cost[v][i];
					if (!mst_vertex[z] && dist[z] > w) {
						dist[z] = w;
					}
				}
			}
			
			return result;
		}
		
		static double getDistance(int[] x, int[] y, int i, int j) {
			return Math.Sqrt(Math.Pow(x[j]-x[i], 2) + Math.Pow(y[j]-y[i], 2));
		}
	}
}