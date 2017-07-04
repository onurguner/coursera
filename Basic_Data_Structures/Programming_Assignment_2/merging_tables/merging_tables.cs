using System;
using System.Collections.Generic;
using System.Linq;

namespace merging_tables 
{
	public class Table 
	{
        public Table parent;
        public int rank;
        public int numberOfRows;

        public Table(int numberOfRows) 
		{
            this.numberOfRows = numberOfRows;
            this.rank = 0;
            this.parent = this;
        }
		
		public Table getParent() 
		{
            // find super parent and compress path
			if (this != parent)
			{
				parent = parent.getParent();
			}
            return parent;
        }
	}
	
	public class MergingTables
	{
		int maximumNumberOfRows = -1;
		
		private void merge(Table destination, Table source) 
		{
			Table realDestination = destination.getParent();
			Table realSource = source.getParent();
			if (realDestination == realSource) {
				return;
			}
			// merge two components here
			// use rank heuristic
			// update maximumNumberOfRows
			if (realDestination.rank <= realSource.rank)
			{
				realDestination.parent = realSource;
				realSource.numberOfRows += realDestination.numberOfRows;
				if (realDestination.rank == realSource.rank)
					realSource.rank += 1;
			}
			else
			{
				realSource.parent = realDestination;
				realDestination.numberOfRows += realSource.numberOfRows;
			}
			
			int numOfRows = Math.Max(realSource.numberOfRows, realDestination.numberOfRows);
			maximumNumberOfRows = Math.Max(maximumNumberOfRows, numOfRows);			
		}
		
		public void run() 
		{
			var tokens = Console.ReadLine().Split(' ');
			int n = int.Parse(tokens[0]);
			int m = int.Parse(tokens[1]);
			
			tokens = Console.ReadLine().Split(' ');
			
			Table[] tables = new Table[n];
			for (int i = 0; i < n; i++) {
				int numberOfRows = int.Parse(tokens[i]);
				tables[i] = new Table(numberOfRows);
				maximumNumberOfRows = Math.Max(maximumNumberOfRows, numberOfRows);
			}
			for (int i = 0; i < m; i++) {
				tokens = Console.ReadLine().Split(' ');
				int destination = int.Parse(tokens[0]) - 1;
				int source = int.Parse(tokens[1]) - 1;
				merge(tables[destination], tables[source]);
				Console.WriteLine(maximumNumberOfRows);
			}
		}
		
	}

	class Program
	{
		static void Main(string[] args)
        {
			new MergingTables().run();
        }		
	}
}