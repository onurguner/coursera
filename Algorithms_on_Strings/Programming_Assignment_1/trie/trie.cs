using System;
using System.Collections.Generic;
using System.Linq;

namespace trie
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<string> patterns = new List<string>();
            for (int i = 0; i < n; i++)
            {
                string s = Console.ReadLine();
                patterns.Add(s);
            }

            var trie = BuildTrie(patterns);

            for (int i = 0; i < trie.Count(); i++)
            {
                foreach (var edge in trie[i])
                {
                    Console.WriteLine("{0}->{1}:{2}", i, edge.Value.ToString(), edge.Key);
                }
            }
        }

        static List<Dictionary<char, int>> BuildTrie(List<string> patterns)
        {
			int nodeCount = 0;
			
            var trie = new List<Dictionary<char, int>>();
			
			Dictionary<char, int> root = new Dictionary<char, int>();
			trie.Add(root);
				
			foreach (string pattern in patterns) {
				Dictionary<char, int> current = root;											
				foreach (char ch in pattern) {				
					if (current != null && current.ContainsKey(ch)) {
						int index = current[ch];
						if (index >= 0 && index < trie.Count)
							current = trie[current[ch]];
						else
							current = null;
					} else {
						Dictionary<char, int> newNode = new Dictionary<char, int>();
						trie.Add(newNode);
						current.Add(ch, ++nodeCount);
						current = newNode;
					}
				}				
			}

            return trie;
        }
    }
}