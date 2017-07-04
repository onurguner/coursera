using System;
using System.Collections.Generic;

namespace tree_height
{
	public class Node {
		public int key;
		public Node parent;
		public List<Node> children;

		public Node() {
		  this.parent = null;
		  this.children = new List<Node>();
		}

		public void setParent(Node theParent) {
		  parent = theParent;
		  parent.children.Add(this);
		}
	};	
	
	class Program
	{
		static void Main(string[] args)
        {
			int n = int.Parse(Console.ReadLine());
			var input = Console.ReadLine();
            var tokens = input.Split(' ');
			
			int parent_index = 0, root_index = 0;
			
			List<Node> nodes = new List<Node>();
			for (int i = 0; i < n; i++)
			{
				nodes.Add(new Node());				
			}
			
			for (int child_index = 0; child_index < tokens.Length; child_index++)
			{
				parent_index = int.Parse(tokens[child_index]);
				if (parent_index >= 0)
					nodes[child_index].setParent(nodes[parent_index]);
				else
					root_index = child_index;
				nodes[child_index].key = child_index;
			}
			
			int height = getHeight(nodes[root_index]);
			Console.WriteLine(height);
		}
		
		static int getHeight(Node root)
		{
			int currLevelNodeCnt = 1, nextLevelNodeCnt = 0, height = 0;
			Queue<Node> queue = new Queue<Node>();
			queue.Enqueue(root);
			while (queue.Count != 0)
			{
				height++;
				while (currLevelNodeCnt > 0)
				{
					Node node = queue.Dequeue();
					foreach (Node child in node.children)
					{
						queue.Enqueue(child);
						nextLevelNodeCnt++;
					}
					currLevelNodeCnt--;
				}
				currLevelNodeCnt = nextLevelNodeCnt;
				nextLevelNodeCnt = 0;
			}
			
			return height;
		}
	}
}