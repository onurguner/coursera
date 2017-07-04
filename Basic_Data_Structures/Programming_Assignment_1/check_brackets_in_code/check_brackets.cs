using System;
using System.Collections.Generic;

namespace check_brackets 
{
	public class Bracket {
		public Bracket(char type, int position) {
			this.type = type;
			this.position = position;
		}

		public bool Match(char c) {
			if (this.type == '[' && c == ']')
				return true;
			if (this.type == '{' && c == '}')
				return true;
			if (this.type == '(' && c == ')')
				return true;
			return false;
		}

		public char type;
		public int position;
	}
	
	class Program
	{
		static void Main(string[] args)
        {
			string text = Console.ReadLine();
			Stack<Bracket> opening_brackets_stack = new Stack<Bracket>();
			for (int position = 0; position < text.Length; ++position) 
			{
				char next = text[position];

				if (next == '(' || next == '[' || next == '{') {
					// Process opening bracket, write your code here
					opening_brackets_stack.Push(new Bracket(next, position));
				}

				if (next == ')' || next == ']' || next == '}') {
					// Process closing bracket, write your code here
					if (opening_brackets_stack.Count == 0)
					{
						Console.WriteLine(position + 1);
						return;
					}
					Bracket bracket = opening_brackets_stack.Pop();
					if (bracket.Match(next) == false)
					{
						Console.WriteLine(position + 1);
						return;
					}
				}
			}
			// Printing answer, write your code here
			if (opening_brackets_stack.Count == 0)
			{
				Console.WriteLine("Success");
			}
			else
			{
				Bracket bracket = null; 
				while (opening_brackets_stack.Count != 0)
				{
					bracket = opening_brackets_stack.Pop();
				}
				Console.WriteLine(bracket.position + 1);
			}
        }		
	}
}


