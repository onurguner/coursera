using System;
using System.Collections.Generic;

namespace shortest_paths
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
			int s = int.Parse(Console.ReadLine()) - 1;
			long[] distance = new long[n];
			int[] reachable = new int[n];
			int[] shortest = new int[n];
			for (int i = 0; i < n; i++) {
				distance[i] = long.MaxValue;
				reachable[i] = 0;
				shortest[i] = 1;
			}
			shortestPaths(adj, cost, s, distance, reachable, shortest);
			for (int i = 0; i < n; i++) {
				if (reachable[i] == 0) {
					Console.WriteLine('*');
				} else if (shortest[i] == 0) {
					Console.WriteLine('-');
				} else {
					Console.WriteLine(distance[i]);
				}
			}
		}
		
		static void shortestPaths(List<int>[] adj, List<int>[] cost, int s, long[] distance, int[] reachable, int[] shortest) {
			distance[s] = 0;
			List<int> reachableVertex = bfs(adj, s);
			foreach (int index in reachableVertex) {
				reachable[index] = 1;
			}
			
			List<int> cycle = new List<int>();
			for (int i=0; i<adj.Length; i++) {
				for (int j=0; j<reachableVertex.Count; j++) {
					int u = reachableVertex[j];
					List<int> neighbours = adj[u];
					List<int> costs = cost[u];
					for (int k=0; k<neighbours.Count; k++) {
						int v = neighbours[k];
						int w = costs[k];
						if (distance[v] > distance[u] + w) {
							distance[v] = distance[u] + w;
							if (i == adj.Length - 1) {
								shortest[v] = 0;
								cycle.Add(v);
							}
						}
					}
				}
			}
			
			if (cycle.Count != 0) {
				foreach (int vertex in bfs(adj, cycle[0])) {
					shortest[vertex] = 0;
				}
			}
		}
		
		static List<int> bfs(List<int>[] adj, int s) {
			List<int> result = new List<int>();			
			Queue<int> queue = new Queue<int>();
			bool[] visited = new bool[adj.Length];
			
			queue.Enqueue(s);
			while (queue.Count != 0) {
				int v = queue.Dequeue();
				if (visited[v] == false) {
					visited[v] = true;
					result.Add(v);
					foreach (int e in adj[v]) {
						queue.Enqueue(e);
					}
				}			
			}
			return result;
		}
	}
}