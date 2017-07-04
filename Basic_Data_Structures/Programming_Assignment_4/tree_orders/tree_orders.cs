using System;
using System.Collections.Generic;

namespace tree_orders
{
	class Program {
		static void Main(string[] args) {
			TreeOrders orders = new TreeOrders();
			orders.read();
			print(orders.inOrder());
			print(orders.preOrder());
			print(orders.postOrder());
		}
		
		static void print(List<int> x) {
			foreach (int a in x) {
				Console.Write(a + " ");
			}
			Console.WriteLine("");
		}
	}
	
	public class TreeOrders {
		public int n;
		public int[] key, left, right;
		
		public void read() {
			n = int.Parse(Console.ReadLine());
			key = new int[n];
			left = new int[n];
			right = new int[n];
			for (int i = 0; i < n; i++) { 
				var tokens = Console.ReadLine().Split(' ');			
				key[i] = int.Parse(tokens[0]);
				left[i] = int.Parse(tokens[1]);
				right[i] = int.Parse(tokens[2]);
			}
		}
		
		public List<int> inOrder() {
			List<int> result = new List<int>();
            inOrder(0, ref result);                        
			return result;
		}
		
		private void inOrder(int index, ref List<int> result) {
			if (index == -1)
				return;
			inOrder(left[index], ref result);
			result.Add(key[index]);
			inOrder(right[index], ref result);
		}

		public List<int> preOrder() {
			List<int> result = new List<int>();
            preOrder(0, ref result);                        
			return result;
		}
		
		private void preOrder(int index, ref List<int> result) {
			if (index == -1)
				return;
			result.Add(key[index]);
			preOrder(left[index], ref result);
			preOrder(right[index], ref result);
		}
		
		public List<int> postOrder() {
			List<int> result = new List<int>();
            postOrder(0, ref result);  
			return result;
		}
		
		private void postOrder(int index, ref List<int> result) {
			if (index == -1)
				return;
			postOrder(left[index], ref result);
			postOrder(right[index], ref result);
			result.Add(key[index]);
		}
	}
}