using System;
using System.Collections.Generic;

namespace reachability
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
			
			Console.WriteLine(reach(adj, x, y));
		}
		
		static int reach(List<int>[] adj, int x, int y) {
			
			bool[] visited = new bool[adj.Length];
			explore(adj, visited, x);
			return (visited[y] == true) ? 1: 0;
		}
		
		static void explore(List<int>[] adj, bool[] visited, int v) {
			visited[v] = true;
			foreach (int vertex in adj[v]) {
				if (visited[vertex] == false) {
					explore(adj, visited, vertex);
				}
			}
		}
	}
}