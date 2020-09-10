using System;
using System.Collections.Generic;
using System.Text;

namespace OrthograhicalNeighborProblem
{
    public class Solver
    {
        private List<string> WordDictonary;
        /// <summary>
        /// Constuctor: Requires a dictionary in type List string.
        /// </summary>
        public Solver(List<string> dictonary)
        {
            WordDictonary = dictonary;
        }
        /// <summary>
        /// This method takes a start and end word.
        /// It returns a stack of Ortographical Neighbors connecting the words (if possible).
        /// </summary>
        public Stack<string> Solve(string startWord, string endWord)
        {
            List<string> TabooList = new List<string>();
            Queue<string> searchQueue = new Queue<string>();
            searchQueue.Enqueue(startWord);
            Dictionary<string, string> parent = new Dictionary<string, string>();

            while (searchQueue.Count > 0)
            {
                string node = searchQueue.Dequeue();
                if (node == endWord)
                {
                    return Backtrace(parent, startWord, endWord);
                }
                List<string> children = GetOrthoNeighbors(node, ref TabooList);
                foreach (string s in children)
                {
                    searchQueue.Enqueue(s);
                    parent.Add(s, node);
                }
            }
            return new Stack<string>();
        }

        /// <summary>
        /// This method takes a start and end word and a dictionary.
        /// It returns a list of Ortographical Neighbors connecting the words (if possible).
        /// </summary>
        public Stack<string> Backtrace(Dictionary<string, string> parent, string root, string end)
        {
            Stack<string> path = new Stack<string>();
            path.Push(end);
            while (path.Peek() != root)
            {
                path.Push(parent[path.Peek()]);
            }
            return path;
        }

        /// <summary>
        /// This method takes a root word and a taboo list.
        /// It returns Orthographical neighbors of the root word that aren't found on the taboo list
        /// Taboo list is passed by refence so it can be updated aswell
        /// </summary>
        public List<string> GetOrthoNeighbors(string strRoot, ref List<string> tabooList)
        {
            List<string> neighbors = new List<string>();
            foreach (string s in WordDictonary)
            {
                if (AreOrthographicalNeighbors(s, strRoot)) {
                    if (!tabooList.Contains(s)) {
                        neighbors.Add(s);
                        tabooList.Add(s);
                    }
                }
            }
            return neighbors;

        }

        /// <summary>
        /// This method takes two words and returns a boolean of true if they are Orthographical Neighbors.
        /// </summary>
        public bool AreOrthographicalNeighbors(string strWord1, string strWord2)
        {
            int intMatchingChars = 0;
            for (int i = 0; i < strWord1.Length; i++)
            {
                if (strWord1[i] == strWord2[i]) {
                    intMatchingChars++;
                }
            }

            if (intMatchingChars == (strWord1.Length - 1)) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
