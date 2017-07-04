using System;
using System.Collections.Generic;

namespace kmp
{
	class Program {
		static void Main(string[] args) {
			new KnuthMorrisPratt().run();
		}
	}
	
	class KnuthMorrisPratt {
		public void run() {
			string pattern = Console.ReadLine();
			string text = Console.ReadLine();
			findPattern(pattern, text);
			//KMPSearch(pattern, text);
			Console.WriteLine("");
		}
		
		private void print(List<int> x) {
			foreach (int a in x) {
				Console.Write(a + " ");
			}
			Console.WriteLine("");
		}
		
		// Find all the occurrences of the pattern in the text and return
		// a list of all positions in the text (starting from 0) where
		// the pattern starts in the text.
		private void findPattern(string pattern, string text) {
			int patternLen = pattern.Length;
			int textLen = text.Length;
			if (textLen < patternLen)
				return;
			
			string str = pattern + '$' + text;
			int[] s = computePrefixFunction(str);
			for (int i=patternLen+1; i<str.Length; i++) {
				if (s[i] == patternLen) {
					Console.Write(i - 2*patternLen + " ");
				}
			}
		}
		
		private int[] computePrefixFunction(string P) {
			int lenP = P.Length;			
			int[] s = new int[lenP];
			s[0] = 0;
			int border = 0;
			for (int i=1; i<lenP; i++) {
				while (border > 0 && P[i] != P[border]) {
					border = s[border-1];
				}
				if (P[i] == P[border])
					border++;
				else
					border = 0;
				s[i] = border;
			}
			return s;
		}
	
		private void KMPSearch(string pattern, string text) {
			int N = text.Length;
			int M = pattern.Length;
			int[] lps = new int[M];
			computeLpsArray(pattern, lps);
			int i=0, j=0;
			while (i < N) {
				if (pattern[j] == text[i]) {
					i++;
					j++;
				}
				if (j == M) {
					Console.Write(i - j + " ");
					j = lps[j-1];
				} else if (i < N && pattern[j] != text[i]) {
					if (j != 0) {
						j = lps[j-1];
					} else {
						i = i + 1;
					}
				}
			}
		}
	
		private void computeLpsArray(string pattern, int[] lps) {
			int M = pattern.Length;
			int len = 0, i=1;
			lps[0] = 0;
			while (i < M) {
				if (pattern[i] == pattern[len]) {
					len++;
					lps[i] = len;
					i++;
				} else {
					if (len != 0) {
						len = lps[len-1];
					} else {
						lps[i] = 0;
						i++;
					}
				}
			}			
		}
	
	}
}