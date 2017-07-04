using System;
using System.Collections.Generic;

namespace is_bst_hard
{
	class Program {
		static void Main(string[] args) {
			IsBST tree = new IsBST();
			tree.read();
			if (tree.isBinarySearchTree())
				Console.WriteLine("CORRECT");
			else
				Console.WriteLine("INCORRECT");
		}
	}
	
	public class IsBST {
        class Node {
            public int key;
            public int left;
            public int right;

            public Node(int key, int left, int right) {
                this.left = left;
                this.right = right;
                this.key = key;
            }
        }
		
		int nodes;
        Node[] tree;
		
		public void read() {
            nodes = int.Parse(Console.ReadLine());
			if (nodes == 0)
				return;
			
            tree = new Node[nodes];
            for (int i = 0; i < nodes; i++) {
				var tokens = Console.ReadLine().Split(' ');
                tree[i] = new Node(int.Parse(tokens[0]), int.Parse(tokens[1]), int.Parse(tokens[2]));
            }
        }
		
		public bool isBinarySearchTree() {
			if (nodes == 0)
				return true;
			
			return IsValidBST(0, -1, -1);
		}
		
		private bool IsValidBST(int nodeInd, int minNodeInd, int maxNodeInd) {
			if (nodeInd == -1) {
				return true;
			}
			if (((minNodeInd != -1) && (tree[nodeInd].key < tree[minNodeInd].key)) || 
				((maxNodeInd != -1) && (tree[nodeInd].key >= tree[maxNodeInd].key))) {
				return false;
			} else {
				return IsValidBST(tree[nodeInd].left, minNodeInd, nodeInd) && IsValidBST(tree[nodeInd].right, nodeInd, maxNodeInd);
			}
		}
	}
}