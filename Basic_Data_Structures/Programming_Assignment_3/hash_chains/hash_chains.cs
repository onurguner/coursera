using System;
using System.Collections.Generic;

namespace hash_chains
{
	class Program {
		static void Main(string[] args) {
			int bucketCount = int.Parse(Console.ReadLine());
			new HashChains(bucketCount).processQueries();
        }		
	}
	
	public class HashChains {
		private int bucketCount;
		private int prime = 1000000007;
		private int multiplier = 263;
		private List<string>[] elems = null;
		
		public HashChains(int bucketCount) {
			this.bucketCount = bucketCount;
			elems = new List<string>[bucketCount];
			for (int i=0; i<bucketCount; ++i) {
				elems[i] = new List<string>();
			}
		}
		
		public void processQueries() {
			int queryCount = int.Parse(Console.ReadLine());
			for (int i = 0; i < queryCount; ++i) {
				processQuery(readQuery());
			}
		}
		
		private int hashFunc(string s) {
			long hash = 0;
			for (int i = s.Length - 1; i >= 0; --i)
				hash = ((hash * multiplier + s[i]) % prime + prime) % prime;
			return (int)hash % bucketCount;
		}
		
		private Query readQuery() {
			var tokens = Console.ReadLine().Split(' ');
			string type = tokens[0];
			if (type != "check") {
				string s = tokens[1];
				return new Query(type, s);
			} else {
				int ind = int.Parse(tokens[1]);
				return new Query(type, ind);
			}
		}
		
		private void processQuery(Query query) {
			int h = 0;
			switch (query.type) {
				case "add":
					h = hashFunc(query.s);
					if (!elems[h].Contains(query.s))
						elems[h].Insert(0, query.s);
					break;
				case "del":
					h = hashFunc(query.s);
					if (elems[h].Contains(query.s))
						elems[h].Remove(query.s);
					break;
				case "find":
					h = hashFunc(query.s);
					if (elems[h].Contains(query.s))
						Console.WriteLine("yes");
					else
						Console.WriteLine("no");
					break;
				case "check":
					h = query.ind; 
					if (h < 0 || h >= bucketCount)
						Console.WriteLine(" ");
					else {
						if (elems[h].Count == 0)
							Console.WriteLine(" ");
						else {
							foreach (string item in elems[h])
								Console.Write(item + " ");
							Console.WriteLine("");
						}
					}
					break;
				default:
					Console.WriteLine("Unknown query: " + query.type);
					break;
			}
		}
	}
	
	public class Query {
        public string type;
        public string s;
        public int ind;

        public Query(String type, String s) {
            this.type = type;
            this.s = s;
        }

        public Query(String type, int ind) {
            this.type = type;
            this.ind = ind;
        }
    }
}