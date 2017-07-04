using System;
using System.Collections.Generic;

namespace dijkstra
{
	class Program {
		static void Main(string[] args) {
			var tokens = Console.ReadLine().Split(' ');
			int n = int.Parse(tokens[0]);
			int m = int.Parse(tokens[1]);
			List<int>[] adj = new List<int>[n];
			List<int>[] cost = new List<int>[n];
			for (int i = 0; i < n; i++) {
				adj[i] = new List<int>();
				cost[i] = new List<int>();
			}
			int x, y, w;			
			for (int i = 0; i < m; i++) {
				tokens = Console.ReadLine().Split(' ');
				x = int.Parse(tokens[0]);
				y = int.Parse(tokens[1]);
				w = int.Parse(tokens[2]);
				adj[x - 1].Add(y - 1);
				cost[x - 1].Add(w);
			}
			
			tokens = Console.ReadLine().Split(' ');
			x = int.Parse(tokens[0]) - 1;
			y = int.Parse(tokens[1]) - 1;
			
			Console.WriteLine(distance(adj, cost, x, y));
		}
		
		static int distance(List<int>[] adj, List<int>[] cost, int s, int t) {
			List<int> minQueue = new List<int>();
			int[] dist = new int[adj.Length];
			int[] prev = new int[adj.Length];
			for (int i=0; i<adj.Length; ++i) {
				prev[i] = -1;
				if (i == s)
					dist[i] = 0;
				else
					dist[i] = int.MaxValue;
				minQueue.Add(i);
			}
			
			while (minQueue.Count != 0) {
				//sort to find min distance vertex
				minQueue.Sort((v1, v2) => dist[v1] - dist[v2]);
				var u = minQueue[0];
				minQueue.Remove(u);
				
				if (u == t) {
					break;
				}
				
				if (dist[u] == int.MaxValue) {
					break;
				}
				
				for (int i=0; i<adj[u].Count; ++i) {
					var v = adj[u][i];
					var w = cost[u][i];
					if (dist[v] > dist[u] + w) {
						dist[v] = dist[u] + w;
						prev[v] = u;
					}
				}
			}
			
			return (dist[t] == int.MaxValue) ? -1 : dist[t];
		}
	}
}