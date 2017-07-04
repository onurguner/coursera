using System;
using System.Collections.Generic;

namespace toposort
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
			
			List<int> order = toposort(adj);
			foreach (int s in order) {
				Console.Write((s + 1) + " ");
			}
			Console.WriteLine("");
		}
	
		static List<int> toposort(List<int>[] adj) {
			List<int> order = new List<int>();		
			bool[] used = new bool[adj.Length];
			for (int i=0; i<adj.Length; ++i) {
				if (used[i])
					continue;
				dfs(adj, i, used, ref order);
			}			
			return order;
		}

		static void dfs(List<int>[] adj, int v, bool[] used, ref List<int> order) {
			if (used[v] == false) {
				foreach (int s in adj[v]) {
					dfs(adj, s, used, ref order);
				}
				used[v] = true;
				order.Insert(0, v);
			}		  
		}
	
	}
}