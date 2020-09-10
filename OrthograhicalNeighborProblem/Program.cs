using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace OrthograhicalNeighborProblem
{
    public class OrthograhicalNeighborSolver
    {
        static void Main(string[] args)
        {
            //Handle user inputs
            string[] strDictionary = GetDictionary();
            //Clean dictionary 
            List<string> cleanDictonary = CleanDictionary(strDictionary);

            Solver solver = new Solver(cleanDictonary);

            while (true)
            {
                string strStartWord = GetStartWord();
                string strEndWord = GetEndWord(cleanDictonary);
                string strResultDir = GetResultDir();

                Stack<string> result = solver.Solve(strStartWord, strEndWord);

                if (result.Count == 0) {
                    Console.WriteLine("No result found");
                } else {
                    foreach (string str in result)
                    {
                        Console.WriteLine(str);
                    }
                    WriteResult(result, strResultDir);
                }
            }
        }

        /// <summary>
        /// This method takes writes the result of the search to the path provided.
        /// </summary>
        public static void WriteResult(Stack<string> result, string path)
        {
            try {
                File.WriteAllLines(@path, result);
                Console.WriteLine("Result saved.");
            } catch {
                Console.WriteLine("Error: Invalid path " + @path);
            }     
        }

        /// <summary>
        /// This method get a valid end word from the user and returns it.
        /// </summary>
        public static List<string> CleanDictionary(string[] dictionary)
        {
            List<string> cleanDictonary = new List<string>();
            foreach (string word in dictionary)
            {   //Only letters, of 4 length. Slight redundancy here. 
                if (Regex.IsMatch(word, @"\b\w[a-zA-Z]{3}\b") && word.Length == 4) {
                    cleanDictonary.Add(word.ToLower());
                }
            }

            return cleanDictonary;
        }

        /// <summary>
        /// This method get a valid end word from the user and returns it.
        /// </summary>
        public static string GetEndWord(List<string> dictonary)
        {
            bool blnCorrectWord = false;
            string strInput = "";
            while (blnCorrectWord == false)
            {
                Console.WriteLine("Please a end word.");
                strInput = @"" + Console.ReadLine();
                if (Regex.IsMatch(strInput, @"\b\w[a-zA-Z]{3}\b") && strInput.Length == 4) { //check input format
                    if (dictonary.Contains(strInput)) {
                        blnCorrectWord = true;
                    } else {
                        Console.WriteLine("Error: Input is not in the dictionary.");
                    }    
                }
                else {
                    Console.WriteLine("Error: Input in wrong format (Check length (4) and no special chars)");
                }
            }
            return strInput.ToLower();
        }

        /// <summary>
        /// This method get a valid start word from the user and returns it
        /// </summary>
        public static string GetStartWord() {
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
        /// This method get a valid directory of a txt file from the user and returns the contents
        /// of the file as an array of strings.
        /// </summary>
        public static string[] GetDictionary()
        {
            bool blnFoundFile = false;
            string strPath = "";
            while (blnFoundFile == false)
            {
                Console.WriteLine("Please input file path of dictionary file.");
                strPath = @"" + Console.ReadLine();
                if (File.Exists(strPath)) {
                    blnFoundFile = true;
                }
                else {
                    Console.WriteLine("Error: file could not be found.");
                }
            }
            string[] lines = File.ReadAllLines(strPath);
            return lines;
        }

        /// <summary>
        /// This method gets the result path from the user and returns it.
        /// </summary>
        public static string GetResultDir() {
            Console.WriteLine("Please input result path.");
            return Console.ReadLine();
        }
    }
}
