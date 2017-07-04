using System;
using System.Collections.Generic;
using System.Linq;

namespace trie_matching
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            int n = int.Parse(Console.ReadLine());
            List<string> patterns = new List<string>();
            for (int i = 0; i < n; i++)
            {
                string s = Console.ReadLine();
                patterns.Add(s);
            }

            List<int> answers = Solve(text, patterns);
            string answersLine = string.Join(" ", answers);
            Console.WriteLine(answersLine);
        }
		
        static List<int> Solve(string text, List<string> patterns)
        {
            List<int> ans = new List<int>();
            //write your code here
			var trie = BuildTrie(patterns);
			
			int count = 0, match = -1;
			while (text.Length != 0) {
				match = PrefixTrieMatching(count++, text, trie);
				if (match != -1)
					ans.Add(match);
				text = text.Substring(1, text.Length - 1);
			}

            return ans;
        }
		
		static int PrefixTrieMatching(int cnt, string text, List<Dictionary<char, int>> trie) {
			int chIndex = 0;
			char symbol = text[chIndex];
			Dictionary<char, int> node = trie[0];
			while (true) {
				if (IsLeaf(node)) {
					return cnt;
				} else if (node.ContainsKey(symbol)) {
					node = trie[node[symbol]];
					if (chIndex < text.Length - 1)
						symbol = text[++chIndex];
					else
						symbol = ' ';
				} else {
					break;
				}
			}
			return -1;
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
		
		static bool IsLeaf(Dictionary<char, int> node) {
			return (node.Count == 0);
		}
		
    }
}