using System;
using System.Collections.Generic;

namespace negative_cycle
{
	class Program {
		const int INFINITY = int.MaxValue / 2;
		
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
			
			Console.WriteLine(negativeCycle(adj, cost));
		}
		
		static int negativeCycle(List<int>[] adj, List<int>[] cost) {
			int vertexCount = adj.Length;
			
			int[] dist = new int[vertexCount];
			for (int i=0; i<vertexCount; ++i) {
				dist[i] = INFINITY;
			}
			
			dist[0] = 0;			
			for (int i=0; i<vertexCount; i++) {
			
				for (int u=0; u<vertexCount; u++) {
					List<int> edgeList = adj[u];
					List<int> costList = cost[u];
					for (int e=0; e<edgeList.Count; e++) {
						int v = edgeList[e];
						int w = costList[e];
						if (dist[v] > dist[u] + w) {
							dist[v] = dist[u] + w;
							if (i == vertexCount -1) {
								return 1;
							}
						}
					}
				}
			}
			
			return 0;
		}
	}
}