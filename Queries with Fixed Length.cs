using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'solve' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY arr
     *  2. INTEGER_ARRAY queries
     */

    public static List<int> solve(List<int> arr, List<int> queries)
    {
        List<int> result = new List<int>();
        int n = arr.Count;
        
        foreach (int d in queries) {
            // For each query, find the minimum of maximums in all subarrays of length d
            int minOfMax = int.MaxValue;
            
            // Use a deque to find max in sliding window
            LinkedList<int> deque = new LinkedList<int>();
            
            for (int i = 0; i < n; i++) {
                // Remove elements outside the current window
                if (deque.Count > 0 && deque.First.Value <= i - d) {
                    deque.RemoveFirst();
                }
                
                // Remove smaller elements from the back
                while (deque.Count > 0 && arr[deque.Last.Value] <= arr[i]) {
                    deque.RemoveLast();
                }
                
                deque.AddLast(i);
                
                // When we have a full window, track the minimum of maximums
                if (i >= d - 1) {
                    minOfMax = Math.Min(minOfMax, arr[deque.First.Value]);
                }
            }
            
            result.Add(minOfMax);
        }
        
        return result;
    }

    // Alternative implementation using priority queue
    public static List<int> solveAlternative(List<int> arr, List<int> queries) {
        List<int> result = new List<int>();
        int n = arr.Count;
        
        foreach (int d in queries) {
            // Use sorted set to maintain window elements
            var window = new SortedSet<(int value, int index)>();
            int minOfMax = int.MaxValue;
            
            for (int i = 0; i < n; i++) {
                window.Add((arr[i], i));
                
                // Remove elements outside current window
                if (i >= d) {
                    window.Remove((arr[i - d], i - d));
                }
                
                // When we have a full window, get the maximum
                if (i >= d - 1) {
                    minOfMax = Math.Min(minOfMax, window.Max.value);
                }
            }
            
            result.Add(minOfMax);
        }
        
        return result;
    }


    }

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int q = Convert.ToInt32(firstMultipleInput[1]);

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        List<int> queries = new List<int>();

        for (int i = 0; i < q; i++)
        {
            int queriesItem = Convert.ToInt32(Console.ReadLine().Trim());
            queries.Add(queriesItem);
        }

        List<int> result = Result.solve(arr, queries);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
