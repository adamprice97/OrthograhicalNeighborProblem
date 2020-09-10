using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrthograhicalNeighborProblem;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class SolverTests
    {
        public Solver solver;
        public SolverTests()
        {
            string[] strDictionary = File.ReadAllLines(@"C:\Users\Adam\Desktop\words-english.txt");
            List<string> cleanDictonary = OrthograhicalNeighborSolver.CleanDictionary(strDictionary);
            solver = new Solver(cleanDictonary);
        }

        [TestMethod]
        public void TestAreOrthographicalNeighbors()
        {
            bool actual = solver.AreOrthographicalNeighbors("test", "text");
            Assert.AreEqual(true, actual, "Incorrect");

            actual = solver.AreOrthographicalNeighbors("trrt", "text");
            Assert.AreEqual(false, actual, "Incorrect");

            actual = solver.AreOrthographicalNeighbors("test", "test");
            Assert.AreEqual(false, actual, "Incorrect");
        }

        [TestMethod]
        public void TestGetOrthoNeighbors()
        {
            List<string> expected = new List<string>() { "tess", "best", "fest", "jest", "lest", "nest", "pest", "rest", "teat", "teet", "tent", "text", "vest", "west", "zest" };
            List<string> taboo = new List<string>();
            List<string> actual = solver.GetOrthoNeighbors("test", ref taboo);

            Assert.AreEqual(expected.Last(), actual.Last(), "Incorrect");
            Assert.AreEqual(expected.Count(), actual.Count(), "Incorrect");

            Assert.AreEqual(expected.Count(), taboo.Count(), "Incorrect");
        }

        [TestMethod]
        public void TestGetOrthoNeighborsTaboo()
        {
            List<string> expected = new List<string>() { "jest", "lest", "nest", "pest", "rest", "teat", "teet", "tent", "text", "vest", "west", "zest" };
            List<string> taboo = new List<string>() { "tess", "best", "fest"};
            List<string> actual = solver.GetOrthoNeighbors("test", ref taboo);

            Assert.AreEqual(expected.Last(), actual.Last(), "Incorrect");
            Assert.AreEqual(expected.Count(), actual.Count(), "Incorrect");
        }

    }  
}
