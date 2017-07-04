using System;
using System.Collections.Generic;

namespace suffix_array_matching
{
	class Program {
		static void Main(string[] args) {
			new SuffixArrayMatching().run();
		}
	}
	
	class SuffixArrayMatching {
		public void run() {
			string text = Console.ReadLine() + "$";
			int[] suffix_array = computeSuffixArray(text);
			
			int patternCount = int.Parse(Console.ReadLine());			
			var patterns = Console.ReadLine().Split(' ');			
			for (int i = 0; i < patternCount; ++i) {
				List<int> occurrences = findOccurrences(patterns[i], text, suffix_array);
				foreach (int x in occurrences) {
					Console.Write(x + " ");
				}
			}
			Console.WriteLine("");
		}
		
		public List<int> findOccurrences(string pattern, string text, int[] suffixArray) {
			List<int> result = new List<int>();

			int min = 0;
			int max = text.Length;
			int length = text.Length;
			int patternLength = pattern.Length;
			while (min < max) {
				int mid = (min + max) / 2;
				//string suffix = text.Substring(suffixArray[mid], Math.Min(suffixArray[mid] + patternLength, length) - suffixArray[mid]);
				//if (pattern.CompareTo(suffix) > 0) {
				if (compare(pattern, text, suffixArray[mid]) > 0) {
					min = mid + 1;
				} else {
					max = mid;
				}
			}
			int start = min;
			max = text.Length;
			while (min < max) {
				int mid = (min + max) / 2;
				//string suffix = text.Substring(suffixArray[mid], Math.Min(suffixArray[mid] + patternLength, length) - suffixArray[mid]);
				//if (pattern.CompareTo(suffix) < 0) {
				if (compare(pattern, text, suffixArray[mid]) < 0) {
					max = mid;
				} else {
					min = mid + 1;
				}
			}
			int end = max;
			if (start <= end) {
				for (int i = start; i < end; i++) {
					result.Add(suffixArray[i]);
				}
			}
        
			return result;
		}
		
		private int compare(string pattern, string text, int start) {
			for (int i=0; i<pattern.Length; i++) {
				if (pattern[i] > text[start + i])
					return 1;
				else if (pattern[i] < text[start + i])
					return -1;
			}
			return 0;
		}
		
		private int[] computeLcpArray(string text, int[] order) {
			int[] result = new int[text.Length];
			int lcp = 0;
			int[] posInOrder = invertSuffixArray(order);
			int suffix = order[0];
			for (int i=0; i<text.Length; i++) {
				int orderIndex = posInOrder[suffix];
				if (orderIndex == text.Length - 1) {
					lcp = 0;
					suffix = (suffix + 1) % text.Length;
					continue;
				}
				int nextSuffix = order[orderIndex + 1];
				lcp = lcpOfSuffixes(text, suffix, nextSuffix, lcp - 1);
				result[orderIndex] = lcp;
				suffix = (suffix + 1) % text.Length;
			}
			return result;
		}
		
		private int[] invertSuffixArray(int[] order) {
			int[] result = new int[order.Length];
			for (int i=0; i<order.Length; i++) {
				result[order[i]] = i;
			}
			return result;
		}
		
		private int lcpOfSuffixes(string text, int i, int j, int equal) {
			int lcp = Math.Max(0, equal);
			int length = text.Length;
			
			while ((i+lcp)<length && (j+lcp)<length) {
				if (text[i+lcp] == text[j+lcp])
					lcp++;
				else 
					break;
			}
			
			return lcp;
		}
		
		private int[] computeSuffixArray(string text) {
			int[] order = sortCharacters(text);
			int[] clazz = computeCharClasses(text, order);
			
			int L = 1, textLen = text.Length;
			while (L < textLen) {
				order = sortDoubled(text, L, order, clazz);
				clazz = updateClasses(order, clazz, L);
				L = 2 * L;
			}
			
			return order;
		}
		
		private int[] sortCharacters(string text) {
			int[] order = new int[text.Length];
			int[] count = new int[6];
			for (int i=0; i<text.Length; i++)
				count[charToNum(text[i])]++;
			for (int i=1; i<6; i++)
				count[i] += count[i-1];
			for (int i=text.Length-1; i>=0; i--) {
				char ch = text[i];
				count[charToNum(ch)]--;
				order[count[charToNum(ch)]] = i;
			}
			return order; 			
		}
		
		private int[] computeCharClasses(string text, int[] order) {
			int[] clazz = new int[text.Length];
			clazz[order[0]] = 0;
			for (int i=1; i<text.Length; i++) {
				if (text[order[i]] != text[order[i-1]]) {
					clazz[order[i]] = clazz[order[i-1]] +1; 
				} else {
					clazz[order[i]] = clazz[order[i-1]];
				}			
			}
			return clazz;
		}
		
		private int[] sortDoubled(string text, int L, int[] order, int[] clazz) {
			int length = text.Length;
			int[] newOrder = new int[length];
			int[] count = new int[length];
			for (int i=0; i<length; i++) {
				count[clazz[i]]++;
			}
			for (int i=1; i<length; i++) {
				count[i] += count[i-1];
			}
			for (int i=length-1; i>=0; i--) {
				int start = (order[i] - L + length) % length;
				int cl = clazz[start];
				count[cl]--;
				newOrder[count[cl]] = start;
			}
			return newOrder;
		}
		
		private int[] updateClasses(int[] order, int[] clazz, int L) {
			int n = order.Length;
			int[] newClass = new int[n];
			newClass[order[0]] = 0;
			for (int i=1; i<n; i++) {
				int cur = order[i];
				int prev = order[i-1];
				int mid = cur+L;
				int midPrev = (prev+L)%n;
				if (clazz[cur] != clazz[prev] || clazz[mid] != clazz[midPrev]) {
					newClass[cur] = newClass[prev] + 1;
				} else {
					newClass[cur] = newClass[prev];
				}
			}
			return newClass;
		}
		
		private int charToNum(char ch) {
			switch (ch) {
				case '$':
				return 1;
				case 'A':
				return 2;
				case 'C':
				return 3;
				case 'G':
				return 4;
				case 'T':
				return 5;
				default:
				return 0;
			}
		}
	}
}