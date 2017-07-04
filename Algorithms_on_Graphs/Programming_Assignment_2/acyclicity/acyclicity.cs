using System;
using System.Collections.Generic;

namespace acyclicity
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
			}
			
			Console.WriteLine(acyclic(adj));
		}
		
		static int acyclic(List<int>[] adj) {			
			bool[] visited = new bool[adj.Length];
			bool[] backedge = new bool[adj.Length];
			
			for (int i=0; i<adj.Length; ++i) {
				if (visited[i])
					continue;
				if (isCyclic(adj, i, visited, backedge))
					return 1;
			}			
			return 0;
		}
		
		static bool isCyclic(List<int>[] adj, int v, bool[] visited, bool[] backedge) {
			if (backedge[v] == true)
				return true;
			if (visited[v] == false) {
				backedge[v] = true;
				foreach (int vertex in adj[v]) {
					if (isCyclic(adj, vertex, visited, backedge))
						return true;
				}
				visited[v] = true;
				backedge[v] = false;
			}
			return false;
		}
		
	}
}