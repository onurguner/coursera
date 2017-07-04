/*
function inverseBWT (string s)
   create empty table
   repeat length(s) times
       // first insert creates first column
       insert s as a column of table before first column of the table
       sort rows of the table alphabetically
   return (row that ends with the 'EOF' character)
*/

using System;
using System.Linq;
using System.Collections.Generic;

namespace bwtinverse
{
	class Program {
		static void Main(string[] args) {
			string text = Console.ReadLine();
			Console.WriteLine(inverseBWT(text));
		}
		
		static string inverseBWT(string bwt) {
			string data = bwt;
            List<KeyValuePair<int, char>> rotations = new List<KeyValuePair<int, char>>();
            for (int i = 0; i < data.Length; ++i)
            {
                KeyValuePair<int, char> current = new KeyValuePair<int, char>(i, data[i]);
                rotations.Add(current);
            }
			
			rotations = rotations.OrderBy(s => s.Value).ToList();
			
            int index = rotations[0].Key;
            string res = string.Empty;
            for (int i = 0; i < rotations.Count - 1; ++i)
            {
                res += rotations[index].Value;
                index = rotations[index].Key;
            }
            return res + "$";
		}
	}
}