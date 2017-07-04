using System;
using System.Collections.Generic;

namespace bfs
{
	class Program {
		static void Main(string[] args) {
			var tokens = Console.ReadLine().Split(' ');
			int n = int.Parse(tokens[0]);
			int m = int.Parse(tokens[1]);
			List<int>[] adj = new List<int>[n];
			for (int i = 0; i < n; i++) {
				adj[i] = new List<int>();
			}
			int x, y;				
			for (int i = 0; i < m; i++) {
				tokens = Console.ReadLine().Split(' ');
				x = int.Parse(tokens[0]);
				y = int.Parse(tokens[1]);
				adj[x - 1].Add(y - 1);
				adj[y - 1].Add(x - 1);
			}
			
			tokens = Console.ReadLine().Split(' ');
			x = int.Parse(tokens[0]) - 1;
			y = int.Parse(tokens[1]) - 1;
			
			Console.WriteLine(distance(adj, x, y));
		}
		
		static int distance(List<int>[] adj, int s, int t) {
			
			int[] dist = new int[adj.Length];
			for (int i=0; i<adj.Length; ++i) {
				dist[i] = -1;
			}
			dist[s] = 0;
			
			Queue<int> queue = new Queue<int>();			
			queue.Enqueue(s);
			
			while (queue.Count != 0) {
				int u = queue.Dequeue();
				foreach (int v in adj[u]) {
					if (dist[v] == -1) {
						queue.Enqueue(v);
						dist[v] = dist[u] + 1;
					}
				}
			}
			
			return dist[t];
		}
	}
}