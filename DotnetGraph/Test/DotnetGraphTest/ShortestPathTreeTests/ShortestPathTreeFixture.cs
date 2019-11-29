using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Model;
using DotnetGraph.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DotnetGraphTest.ShortestPathTreeTests
{
    [TestClass]
    public abstract class ShortestPathTreeFixture
    {
        public abstract IShortestPathTreeAlgorithm GetShortestPathTreeAlgorithm();

        [TestMethod]
        public void SmallGraphShortestPathTree()
        {
            var graph = GraphGenerator.GetSmallGraph();
            var algorithm = GetShortestPathTreeAlgorithm();
            var shortestPath = algorithm.GetShortestPathTree(graph, graph[0].Origin);
            Assert.IsNotNull(shortestPath);
            Assert.AreEqual(4, shortestPath["C"].Length);
            Assert.AreEqual("A", shortestPath["C"][0].Origin);
            Assert.AreEqual("D", shortestPath["C"][0].Destination);
            Assert.AreEqual("D", shortestPath["C"][1].Origin);
            Assert.AreEqual("E", shortestPath["C"][1].Destination);
            Assert.AreEqual("E", shortestPath["C"][2].Origin);
            Assert.AreEqual("F", shortestPath["C"][2].Destination);
            Assert.AreEqual("F", shortestPath["C"][3].Origin);
            Assert.AreEqual("C", shortestPath["C"][3].Destination);
            Assert.AreEqual(4, shortestPath["C"].TotalWeight());

            Assert.AreEqual(3, shortestPath["B"].Length);
            Assert.AreEqual("A", shortestPath["B"][0].Origin);
            Assert.AreEqual("D", shortestPath["B"][0].Destination);
            Assert.AreEqual("D", shortestPath["B"][1].Origin);
            Assert.AreEqual("E", shortestPath["B"][1].Destination);
            Assert.AreEqual("E", shortestPath["B"][2].Origin);
            Assert.AreEqual("B", shortestPath["B"][2].Destination);
            Assert.AreEqual(4, shortestPath["B"].TotalWeight());
        }

        [TestMethod]
        public void SmallGraphShortestPathTreeWithDoubleArcs()
        {
            var graph = GraphGenerator.GetSmallGraph().ToList();
            graph.Add(new Arc<string>("A", "B", 4));
            graph.Add(new Arc<string>("A", "B", 3));
            graph.Add(new Arc<string>("A", "B", 6));
            graph.Add(new Arc<string>("A", "B", 7));
            var algorithm = GetShortestPathTreeAlgorithm();
            var shortestPath = algorithm.GetShortestPathTree(graph, graph[0].Origin);
            Assert.IsNotNull(shortestPath);
            Assert.AreEqual(4, shortestPath["C"].Length);
            Assert.AreEqual("A", shortestPath["C"][0].Origin);
            Assert.AreEqual("D", shortestPath["C"][0].Destination);
            Assert.AreEqual("D", shortestPath["C"][1].Origin);
            Assert.AreEqual("E", shortestPath["C"][1].Destination);
            Assert.AreEqual("E", shortestPath["C"][2].Origin);
            Assert.AreEqual("F", shortestPath["C"][2].Destination);
            Assert.AreEqual("F", shortestPath["C"][3].Origin);
            Assert.AreEqual("C", shortestPath["C"][3].Destination);
            Assert.AreEqual(4, shortestPath["C"].TotalWeight());

            Assert.AreEqual(1, shortestPath["B"].Length);
            Assert.AreEqual("A", shortestPath["B"][0].Origin);
            Assert.AreEqual("B", shortestPath["B"][0].Destination);
            Assert.AreEqual(3, shortestPath["B"].TotalWeight());
        }
    }
}