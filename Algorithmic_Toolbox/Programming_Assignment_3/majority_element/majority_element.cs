using System;
using System.Collections.Generic;

namespace majority_element 
{
	class Program
	{
		static int getFrequency(int[] a, int key)
		{
			int count = 0;
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] == key)
					count++;
			}
			return count;
		}
		
		static int getMajorityElement(int[] a, int left, int right) {
			if (left == right) {
				return a[left];
			}						
			//write your code here
			int mid = left + (right - left) / 2;
			int elem1 = getMajorityElement(a, left, mid);
			int elem2 = getMajorityElement(a, mid + 1, right);			
			if (elem1 == elem2) {
				return elem1;
			}
			int cnt1 = 0, cnt2 = 0;
			for (int i=left; i<=right; i++)
			{
				if (a[i] == elem1)
					cnt1++;
				if (a[i] == elem2)
					cnt2++;
			}
			int size = right - left + 1;
			if (cnt1 > size / 2)
				return elem1;
			if (cnt2 > size / 2)
				return elem2;

			
			/*int cnt1 = getFrequency(a, elem1);
			if (cnt1 > mid + 1)
				return elem1;
			
			int cnt2 = getFrequency(a, elem2);
			if (cnt2 > mid + 1)
				return elem2;*/
			
			return -1;
		}
		
		static void Main(string[] args)
        {
			int n = int.Parse(Console.ReadLine());
			int[] a  = new int[n];
			
			var tokens = Console.ReadLine().Split(' ');
			for (int i = 0; i < n; i++)
			{
				a[i] = int.Parse(tokens[i]);
			}
			
			int result = (getMajorityElement(a, 0, n-1) != -1) ? 1 : 0;			
			Console.WriteLine(result);
		}		
	}
}