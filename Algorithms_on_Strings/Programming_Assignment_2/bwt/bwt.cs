using System;
using System.Collections.Generic;

namespace bwt
{
	class Program {
		static void Main(string[] args) {
			string text = Console.ReadLine();
			Console.WriteLine(BWT(text));
		}
		
		static string BWT(string text) {
			List<string> tree = new List<string>();
			int lastCharIndex = text.Length - 1; 
			for (int i=0; i<text.Length; i++) {
				tree.Add(text);
				text = text.Substring(1, text.Length - 1) + text[0];
			}
			tree.Sort();
			
			
			string result = "";
			for (int i=0; i<tree.Count; i++) {
				result += tree[i][lastCharIndex];
			}
			
			return result;
		}
	}
}