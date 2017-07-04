using System;
using System.Collections.Generic;

namespace suffix_array_long
{
	class Program {
		static void Main(string[] args) {
			new SuffixArrayLong().run();
		}
	}
	
	class SuffixArrayLong {
		public void run() {
			string text = Console.ReadLine();
			int[] suffix_array = computeSuffixArray(text);
			foreach (int a in suffix_array) {
				Console.Write(a + " ");
			}
			Console.WriteLine("");
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