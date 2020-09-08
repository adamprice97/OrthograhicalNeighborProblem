using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace OrthograhicalNeighborProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            //Handle user inputs
            string[] strDictionary = GetDictionary();
            string strStartWord = GetStartWord();
            string strEndWord = GetEndWord();
            string strResultDir = GetResultDir();

            //Clean dictionary 
            List<string> cleanDictonary = CleanDictionary(strDictionary);


            List<string> test = new List<string>();
            test = BreadthFirstSearch(strStartWord, strEndWord, cleanDictonary);
        }


        private static List<string> BreadthFirstSearch(string root, string end, List<string> dictonary)
        {
            List<string> TabooList = new List<string>();
            Queue<string> searchQueue = new Queue<string>();
            searchQueue.Enqueue(root);

            while (searchQueue.Count > 0)
            {
                string node = searchQueue.Dequeue();
                if (node == end)
                {
                    Console.WriteLine("Found");
                    break;
                }
                List<string> children = GetOrthoNeighbors(node, dictonary, ref TabooList);
                foreach (string s in children)
                {
                    searchQueue.Enqueue(s);
                }
            }

            Console.WriteLine("fail");

            return new List<string>();
        }


        /// <summary>
        /// This class takes a root word, a dictionary and a taboo list.
        /// It returns Orthographical neighbors of the root word that aren't found on the taboo list
        /// Taboo list is passed by refence so it can be updated aswell
        /// </summary>
        private static List<string> GetOrthoNeighbors(string strRoot, List<string> dictionary, ref List<string> TabooList)
        {
            List<string> neighbors = new List<string>();

            foreach (string s in dictionary)
            {
                if (AreOrthographicalNeighbors(s, strRoot))
                {
                    if (!TabooList.Contains(s))
                    {
                        neighbors.Add(s);
                        TabooList.Add(s);
                    }
                }
            }
            return neighbors;
        }

        /// <summary>
        /// This class takes two words and returns a boolean of true if they are Orthographical Neighbors
        /// </summary>
        private static bool AreOrthographicalNeighbors(string strWord1, string strWord2)
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
            } else { 
                return false;
            }
        }


        /// <summary>
        /// This class get a valid end word from the user and returns it.
        /// </summary>
        private static List<string> CleanDictionary(string[] dictionary)
        {
            List<string> cleanDictonary = new List<string>();
            foreach (string word in dictionary)
            {
                if (Regex.IsMatch(word, @"\b\w[a-zA-Z]{3}\b") && word.Length == 4) {
                    cleanDictonary.Add(word.ToLower());
                }
            }

            return cleanDictonary;
        }


        /// <summary>
        /// This class get a valid end word from the user and returns it.
        /// </summary>
        static string GetEndWord()
        {
            bool blnCorrectWord = false;
            string strInput = "";
            while (blnCorrectWord == false)
            {
                Console.WriteLine("Please a end word.");
                strInput = @"" + Console.ReadLine();
                if (Regex.IsMatch(strInput, @"\b\w[a-zA-Z]{3}\b") && strInput.Length == 4) { //check input format
                    blnCorrectWord = true;
                }
                else {
                    Console.WriteLine("Error: Input in wrong format (Check length (4) and no special chars)");
                }
            }
            return strInput.ToLower();
        }
        /// <summary>
        /// This class get a valid start word from the user and returns it
        /// </summary>
        static string GetStartWord()
        {
            bool blnCorrectWord = false;
            string strInput = "";
            while (blnCorrectWord == false)
            {
                Console.WriteLine("Please a start word.");
                strInput = @"" + Console.ReadLine();
                if (Regex.IsMatch(strInput, @"\b\w[a-zA-Z]{3}\b") && strInput.Length == 4) { //check input format
                    blnCorrectWord = true;
                }
                else {
                    Console.WriteLine("Error: Input in wrong format (Check length (4) and no special chars)");
                }
            }
            return strInput.ToLower();
        }

        /// <summary>
        /// This class get a valid directory of a txt file from the user and returns the contents
        /// of the file as an array of strings.
        /// </summary>
        static string[] GetDictionary()
        {
            bool blnFoundFile = false;
            string strPath = "";
            while (blnFoundFile == false) {
                Console.WriteLine("Please input file path of dictionary file.");
                strPath = @"" + Console.ReadLine();
                if (File.Exists(strPath)) {
                    blnFoundFile = true;
                } else {
                    Console.WriteLine("Error: file could not be found.");
                }
            }
            string[] lines = File.ReadAllLines(strPath);
            return lines;
        }

        /// <summary>
        /// This class gets the result path from the user and returns it.
        /// </summary>
        static string GetResultDir()
        {
            Console.WriteLine("Please input result path.");
            return Console.ReadLine();
        }
    }
}
