using System;
using System.Collections.Generic;
using System.Linq;

namespace job_queue 
{
	public class Worker 
	{
		public int id;
		public long startTime;
		
		public Worker(int id, long time) 
		{
			this.id = id;
			this.startTime = time;
		}
	}
	
	public class WorkerComparator : IComparer<Worker>
	{
		public int Compare(Worker w1, Worker w2)
		{
			if (w1.startTime != w2.startTime)
			   return (int)(w1.startTime - w2.startTime);
		   return w1.id - w2.id;
		}
	}
	
	public class JobQueue 
	{
		private int numWorkers;
		private int[] jobs;

		private int[] assignedWorker;
		private long[] startTime;
		
		private void readData()
		{
			var tokens = Console.ReadLine().Split(' ');
			numWorkers = int.Parse(tokens[0]);
			int m = int.Parse(tokens[1]);
			
			tokens = Console.ReadLine().Split(' ');
			jobs = new int[m];
			for (int i = 0; i < m; ++i) {
				jobs[i] = int.Parse(tokens[i]);
			}
		}
		
		private void writeResponse() 
		{
			for (int i = 0; i < jobs.Length; ++i) {
				Console.WriteLine(assignedWorker[i] + " " + startTime[i]);
			}
		}
		
		private void assignJobs() 
		{
			SortedSet<Worker> worker_queue = new SortedSet<Worker>(new WorkerComparator());
			for (int i=0; i<numWorkers; i++) {
				worker_queue.Add(new Worker(i, 0));
			}
			
			assignedWorker = new int[jobs.Length];
			startTime = new long[jobs.Length];
			
			for (int i = 0; i < jobs.Length; i++) {
				Worker worker = worker_queue.First();
				worker_queue.Remove(worker);
				
				assignedWorker[i] = worker.id;
				startTime[i] = worker.startTime;
				worker.startTime += jobs[i];
				
				worker_queue.Add(worker);
			}
		}
		
		public void solve()
		{
			readData();
			assignJobs();
			writeResponse();
		}
	}
	
	class Program
	{
		static void Main(string[] args)
        {
			new JobQueue().solve();
        }		
	}
}