using System;
using System.Collections.Generic;

namespace bwmatching
{
	class Program {
		static void Main(string[] args) {
			new BWMatching().run();
		}
	}
	
	class BWMatching {
		public void run() {
			string bwt = Console.ReadLine();
			// Start of each character in the sorted list of characters of bwt,
			// see the description in the comment about function PreprocessBWT
			Dictionary<char, int> starts = new Dictionary<char, int>();
			// Occurrence counts for each character and each position in bwt,
			// see the description in the comment about function PreprocessBWT
			Dictionary<char, int[]> occ_counts_before = new Dictionary<char, int[]>();
			// Preprocess the BWT once to get starts and occ_count_before.
			// For each pattern, we will then use these precomputed values and
			// spend only O(|pattern|) to find all occurrences of the pattern
			// in the text instead of O(|pattern| + |text|).
			PreprocessBWT(bwt, ref starts, ref occ_counts_before);
       
			int patternCount = int.Parse(Console.ReadLine());
			int[] result = new int[patternCount];
			
			var patterns = Console.ReadLine().Split(' ');			
			for (int i = 0; i < patternCount; ++i) {
				result[i] = CountOccurrences(patterns[i], bwt, ref starts, ref occ_counts_before);
			}
			print(result);
		}
		
		private void PreprocessBWT(string bwt, ref Dictionary<char, int> starts, 
									ref Dictionary<char, int[]> occ_counts_before) {
			
			char[] sortedBWT = bwt.ToCharArray();
			Array.Sort(sortedBWT);

			for (int i = 0; i < sortedBWT.Length; i++) {
				if (starts.ContainsKey(sortedBWT[i]) == false) starts.Add(sortedBWT[i], i);
			}
			
			foreach (char ch in starts.Keys) {
				occ_counts_before.Add(ch, new int[bwt.Length + 1]);
			}
			
			for (int i = 1; i <= bwt.Length; i++) {
				char current = bwt[i - 1];
				foreach (KeyValuePair<char, int[]> ch_entry in occ_counts_before) {
					if (ch_entry.Key == current) {
						ch_entry.Value[i] = ch_entry.Value[i-1] + 1;
					} else {
						ch_entry.Value[i] = ch_entry.Value[i-1];
					}
				}
			}
		}
	
		private int CountOccurrences(string pattern, string bwt, ref Dictionary<char, int> starts, 
									ref Dictionary<char, int[]> occ_counts_before) {
			int top = 0;
			int bottom = bwt.Length - 1;
			int patternLength = pattern.Length;
			while (top <= bottom) {
				if (patternLength > 0) {
					patternLength--;
					char letter = pattern[patternLength];
					if (occ_counts_before.ContainsKey(letter) == false)
						return 0;

					int topOccurency = occ_counts_before[letter][top];
					int bottomOccurency = occ_counts_before[letter][bottom + 1];
					int start = starts[letter];
					if (bottomOccurency > topOccurency) {
						top = start + topOccurency;
						bottom = start + bottomOccurency - 1;
					} else return 0;
				} else return (bottom - top + 1);
			}
			return 0;
		}
		
		private void print(int[] x) {
			foreach (int a in x) {
				Console.Write(a + " ");
			}
			Console.WriteLine("");
		}
	}
 }