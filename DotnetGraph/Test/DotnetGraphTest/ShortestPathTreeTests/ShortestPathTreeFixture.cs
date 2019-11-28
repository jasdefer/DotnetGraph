using DotnetGraph.Algorithms.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.ShortestPathTreeTests
{
    [TestClass]
    public abstract class ShortestPathTreeFixture
    {
        public abstract IShortestPathTreeAlgorithm GetShortestPathTreeAlgorithm();

        [TestMethod]
        public void SmallGraphShortestPathAtoF()
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

            Assert.AreEqual(3, shortestPath["B"].Length);
            Assert.AreEqual("A", shortestPath["B"][0].Origin);
            Assert.AreEqual("D", shortestPath["B"][0].Destination);
            Assert.AreEqual("D", shortestPath["B"][1].Origin);
            Assert.AreEqual("E", shortestPath["B"][1].Destination);
            Assert.AreEqual("E", shortestPath["B"][2].Origin);
            Assert.AreEqual("B", shortestPath["B"][2].Destination);
        }
    }
}