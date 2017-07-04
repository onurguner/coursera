using System;
using System.Collections.Generic;

namespace bipartite
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
			
			Console.WriteLine(bipartite(adj));
		}
		
		static int bipartite(List<int>[] adj) {
			
			int[] colors = new int[adj.Length];
			for (int i=0; i<adj.Length; ++i) {
				colors[i] = -1;
			}
			colors[0] = 0;
			
			Queue<int> queue = new Queue<int>();			
			queue.Enqueue(0);
			
			while (queue.Count != 0) {
				int u = queue.Dequeue();
				foreach (int v in adj[u]) {
					if (colors[v] == -1) {
						queue.Enqueue(v);
						colors[v] = (colors[u] == 0) ? 1 : 0;
					}
					else if (colors[v] == colors[u]) {
						return 0;
					}
				}
			}
			return 1;
		}
	}
}