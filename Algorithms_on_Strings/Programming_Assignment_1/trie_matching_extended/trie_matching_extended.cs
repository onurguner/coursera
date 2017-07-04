using System;
using System.Collections.Generic;
using System.Linq;

namespace trie_matching_extended
{
	class Program {
		static void Main(string[] args) {			
			new TrieMatchingExtended().run();
		}
		
	}
	
	public class Node
	{
		public static int Letters =  4;
		public static int NA      = -1;
		public int[] next;
		public bool patternEnd;

		public Node() {
			next = new int[Letters];
			for (int i=0; i<Letters; ++i)
				next[i] = NA;
			patternEnd = false;
		}		
	}
	
	public class TrieMatchingExtended {
		public void run() {
			string text = Console.ReadLine();
            int n = int.Parse(Console.ReadLine());
            List<string> patterns = new List<string>();
            for (int i = 0; i < n; i++)
            {
                string s = Console.ReadLine();
                patterns.Add(s);
            }

            List<int> answers = solve(text, n, patterns);
            string answersLine = string.Join(" ", answers);
            Console.WriteLine(answersLine);
		
		}
		
		int letterToIndex (char letter) {
			switch (letter)
			{
				case 'A': return 0;
				case 'C': return 1;
				case 'G': return 2;
				case 'T': return 3;
				default: return Node.NA;
			}
		}
		
		List<Node> buildTrie(List<string> patterns) {
			int nodeCount = 0;
			
			List<Node> trie = new List<Node>();
			trie.Add(new Node());

			foreach (string pattern in patterns) {
				Node currentNode = trie[0];
				for (int i = 0; i < pattern.Length; i++) {
					char currentSymbol = pattern[i];
					int index = currentNode.next[letterToIndex(currentSymbol)];

					if (index != Node.NA) {
						currentNode = trie[index];
					} else {
						Node newNode = new Node();
						trie.Add(newNode);
						currentNode.next[letterToIndex(currentSymbol)] =  ++nodeCount;
						currentNode = newNode;
					}
					if (i == pattern.Length - 1)
						currentNode.patternEnd = true;
				}
			}
			return trie;
		}
		
		int prefixTrieMatching(int cnt, string text, List<Node> trie) {
			int chIndex = 0;
			char symbol = text[chIndex];
			Node current = trie[0];
			while (true) {
				if (current.patternEnd) {
					return cnt;
				} else if (current.next[letterToIndex(symbol)] != Node.NA) {
					current = trie[current.next[letterToIndex(symbol)]];
					if (chIndex < text.Length - 1)
						symbol = text[++chIndex];
					else {
						if (current.patternEnd) {
							return cnt;
						}
						break;
					}
				} else {
					break;
				}
			}
			return -1;
		}
		
		List<int> solve (string text, int n, List<string> patterns) {
			List<int> result = new List<int> ();

			List<Node> trie = buildTrie(patterns);
			
			int count = 0, match = -1;
			while (text.Length != 0) {
				match = prefixTrieMatching(count++, text, trie);
				if (match != -1)
					result.Add(match);
				text = text.Substring(1, text.Length - 1);
			}

			return result;
		}
	}
}