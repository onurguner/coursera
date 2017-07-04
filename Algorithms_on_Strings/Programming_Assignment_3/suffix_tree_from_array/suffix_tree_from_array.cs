using System;
using System.Collections.Generic;

namespace suffix_tree_from_array
{
	class Program {
		static void Main(string[] args) {
			new SuffixTreeFromArray().run();
		}
	}
	
	public class SuffixTreeNode {
		public int id;
		public int depth;
		public int start;
		public int end;
		public SuffixTreeNode parent;
		public IDictionary<char, SuffixTreeNode> children;
		
		public SuffixTreeNode() {
			this.id = 0;
			this.depth = 0;
			this.start = -1;
			this.end = -1;
			this.parent = null;
			this.children = new Dictionary<char, SuffixTreeNode>();
		}
		
		public SuffixTreeNode(int id, SuffixTreeNode parent, int depth, int start, int end) {
			this.id = id;
			this.depth = depth;
			this.start = start;
			this.end = end;
			this.parent = parent;
			this.children = new Dictionary<char, SuffixTreeNode>();
		}
	}
	
	public class SuffixTreeFromArray {
		private List<SuffixTreeNode> treeList = new List<SuffixTreeNode>();
		
		public void run() {
			string text = Console.ReadLine();
			int[] suffixArray = new int[text.Length];
			var suffixes = Console.ReadLine().Split(' ');
			for (int i = 0; i < suffixArray.Length; ++i) {
				suffixArray[i] = int.Parse(suffixes[i]);
			}
			int[] lcpArray = new int[text.Length - 1];
			var lcp = Console.ReadLine().Split(' ');
			for (int i = 0; i + 1 < text.Length; ++i) {
				lcpArray[i] = int.Parse(lcp[i]);
			}
			Console.WriteLine(text);
			// Build the suffix tree and get a mapping from 
			// suffix tree node ID to the list of outgoing Edges.
			IDictionary<int, List<Edge>> suffixTree = SuffixTreeFromSuffixArray(suffixArray, lcpArray, text);
			List<string> result = new List<string>();
			// Output the edges of the suffix tree in the required order.
			// Note that we use here the contract that the root of the tree
			// will have node ID = 0 and that each vector of outgoing edges
			// will be sorted by the first character of the corresponding edge label.
			//
			// The following code avoids recursion to avoid stack overflow issues.
			// It uses two stacks to convert recursive function to a while loop.
			// This code is an equivalent of 
			//
			//    OutputEdges(tree, 0);
			//
			// for the following _recursive_ function OutputEdges:
			//
			// public void OutputEdges(Map<Integer, List<Edge>> tree, int nodeId) {
			//     List<Edge> edges = tree.get(nodeId);
			//     for (Edge edge : edges) {
			//         System.out.println(edge.start + " " + edge.end);
			//         OutputEdges(tree, edge.node);
			//     }
			// }
			//
			int[] nodeStack = new int[text.Length];
			int[] edgeIndexStack = new int[text.Length];
			nodeStack[0] = 0;
			edgeIndexStack[0] = 0;
			int stackSize = 1;
			while (stackSize > 0) {
				int node = nodeStack[stackSize - 1];
				int edgeIndex = edgeIndexStack[stackSize - 1];
				stackSize -= 1;
				if (suffixTree.ContainsKey(node) == false) {
					continue;
				}
				if (edgeIndex + 1 < suffixTree[node].Count) {
					nodeStack[stackSize] = node;
					edgeIndexStack[stackSize] = edgeIndex + 1;
					stackSize += 1;
				}
				result.Add(suffixTree[node][edgeIndex].start + " " + suffixTree[node][edgeIndex].end);
				nodeStack[stackSize] = suffixTree[node][edgeIndex].node;
				edgeIndexStack[stackSize] = 0;
				stackSize += 1;
			}
			print(result);
		}
	
		SuffixTreeNode CreateNewLeaf(SuffixTreeNode node, string text, int suffix) {
			int depth = text.Length - suffix;
			int start = suffix + node.depth;
			int end = text.Length - 1;
			SuffixTreeNode leaf = new SuffixTreeNode(treeList.Count, node, depth, start, end);
			node.children[text[leaf.start]] = leaf;
			
			return leaf;
		}
		
		SuffixTreeNode BreakEdge(SuffixTreeNode node, string text, int start, int offset) {
			char startChar = text[start];
			char midChar = text[start + offset];
			int depth = node.depth + offset;
			int end = start + offset - 1;
			SuffixTreeNode midNode = new SuffixTreeNode(treeList.Count, node, depth, start, end);
			midNode.children[midChar] = node.children[startChar];
			node.children[startChar].parent = midNode;
			node.children[startChar].start = start + offset;
			//node.children[startChar].start += offset;
			node.children[startChar] = midNode;
			
			return midNode;
		}
		
		SuffixTreeNode STFromSA(int[] order, int[] lcpArray, string text) {
			SuffixTreeNode root = new SuffixTreeNode();
			treeList.Add(root);
			int lcpPrev = 0, suffix = 0, start = 0, offset = 0;
			SuffixTreeNode curNode = root;
			for (int i=0; i<text.Length; ++i) {
				suffix = order[i];
				while (curNode.depth > lcpPrev) {
					curNode = curNode.parent;
				}
				
				if (curNode.depth == lcpPrev) {
					curNode = CreateNewLeaf(curNode, text, suffix);
					treeList.Add(curNode);
				} else {
					start = order[i - 1] + curNode.depth;
					offset = lcpPrev - curNode.depth;
					SuffixTreeNode midNode = BreakEdge(curNode, text, start, offset);
					treeList.Add(midNode);
					curNode = CreateNewLeaf(midNode, text, suffix);
					treeList.Add(curNode);
				}
				
				if (i < (text.Length - 1)) {
					lcpPrev = lcpArray[i];
				}
			}
			return root;
		}
		
		IDictionary<int, List<Edge>> SuffixTreeFromSuffixArray(int[] suffixArray, int[] lcpArray, string text) {
			IDictionary<int, List<Edge>> tree = new Dictionary<int, List<Edge>>();
		
			SuffixTreeNode root = STFromSA(suffixArray, lcpArray, text);
			/*SuffixTreeNode curNode = null;
			Queue<SuffixTreeNode> queue = new Queue<SuffixTreeNode>();
			queue.Enqueue(root);
			int id = 0;
			while (queue.Count != 0) {
				curNode = queue.Dequeue();				
				List<Edge> neighbours = new List<Edge>();
				foreach (KeyValuePair<char, SuffixTreeNode> child in curNode.children) {
					queue.Enqueue(child.Value);
					neighbours.Add(new Edge(child.Value.id, child.Value.start, child.Value.end + 1));
				}
				tree.Add(id, neighbours);
				id++;
			}	*/	
			SuffixTreeNode curNode = null;
			for (int i = 0; i < treeList.Count; i++) {
				curNode = treeList[i];				
				if (curNode.children.Count != 0) {
					List<Edge> neighbours = new List<Edge>();
					foreach (char c in curNode.children.Keys) {
						SuffixTreeNode child = curNode.children[c];
						neighbours.Add(new Edge(child.id, child.start, child.end + 1));
					}
					tree.Add(curNode.id, neighbours);
				}
			}
		
			return tree;
		}
		
		/*
		Dictionary<int, List<Edge>> SuffixTreeFromSuffixArray(int[] suffixArray, int[] lcpArray, string text) {
			Dictionary<int, List<Edge>> tree = new Dictionary<int, List<Edge>>();
			// Implement this function yourself
			List<TreeNode> nodeTree = new List<TreeNode>();
			TreeNode root = new TreeNode(nodeTree.Count, -1, new SortedDictionary<char, int>(), 0, -1, -1);
			nodeTree.Add(root);
			int lcpPrev = 0;
			TreeNode currentNode = root;
			
			for (int i = 0; i < text.Length; i++) {
				int suffix = suffixArray[i];
				while (currentNode.depth > lcpPrev) {
					currentNode = nodeTree[currentNode.parent];
				}
				if (currentNode.depth == lcpPrev) {
					createNewLeaf(ref nodeTree, ref currentNode, text, suffix);
				} else {
					int edgeStart = suffixArray[i - 1] + currentNode.depth;
					int offset = lcpPrev - currentNode.depth;
					TreeNode midNode = breakEdge(ref nodeTree, currentNode, text, edgeStart, offset);
					createNewLeaf(ref nodeTree, ref midNode, text, suffix);
					currentNode = midNode;
				}

				if (i < text.Length - 1) {
					lcpPrev = lcpArray[i];
				}
			}
			
			for (int i = 0; i < nodeTree.Count; i++) {
				TreeNode current = nodeTree[i];
				if (current.children.Count != 0) {
					List<Edge> neighbours = new List<Edge>();
					foreach (char c in current.children.Keys) {
						TreeNode child = nodeTree[current.children[c]];
						neighbours.Add(new Edge(child.id, child.start, child.end + 1));
					}
					tree.Add(current.id, neighbours);
				}
			}
		
			return tree;
		}
		
		void createNewLeaf(ref List<TreeNode> tree, ref TreeNode parent, string text, int suffix) {
			TreeNode leaf = new TreeNode(tree.Count, parent.id, new SortedDictionary<char, int>(), text.Length - suffix, suffix + parent.depth, text.Length - 1);
			tree.Add(leaf);
			if (parent.children.ContainsKey(text[leaf.start]))
				parent.children[text[leaf.start]] = leaf.id;
			else
				parent.children.Add(text[leaf.start], leaf.id);
		}
		
		TreeNode breakEdge(ref List<TreeNode> tree, TreeNode node, string text, int start, int offset) {
			char startChar = text[start];
			char midChar = text[start + offset];
			TreeNode midNode = new TreeNode(tree.Count, node.id, new SortedDictionary<char, int>(), node.depth + offset, start, start + offset - 1);
			tree.Add(midNode);
			tree[node.children[startChar]].start += offset;
			
			if (midNode.children.ContainsKey(midChar))
				midNode.children[midChar] = node.children[startChar];
			else
				midNode.children.Add(midChar, node.children[startChar]);
			
			tree[node.children[startChar]].parent = midNode.id;
			
			node.children[startChar] = midNode.id;
			
			return midNode;
		}
		*/
		void print(List<string> x) {
			foreach (string a in x) {
				Console.WriteLine(a);
			}
		}
	}
	
	// Data structure to store edges of a suffix tree.
    public class Edge {
        // The ending node of this edge.
        public int node;
        // Starting position of the substring of the text 
        // corresponding to the label of this edge.
        public int start;
        // Position right after the end of the substring of the text 
        // corresponding to the label of this edge.
        public int end;

        public Edge(int node, int start, int end) {
            this.node = node;
            this.start = start;
            this.end = end;
        }
    }
	
	/*public class TreeNode {
        public int id;
        public int parent;
        public SortedDictionary<char, int> children;
        public int depth;
        public int start;
        public int end;

        public TreeNode(int id, int parent, SortedDictionary<char, int> children, int depth, int start, int end) {
            this.id = id;
            this.parent = parent;
            this.children = children;
            this.depth = depth;
            this.start = start;
            this.end = end;
        }
    }*/
}