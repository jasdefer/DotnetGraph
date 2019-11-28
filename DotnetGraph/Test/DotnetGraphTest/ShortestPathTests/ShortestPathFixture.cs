using DotnetGraph.Algorithms.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.ShortestPathTests
{
    [TestClass]
    public abstract class ShortestPathFixture
    {
        public abstract IShortestPathAlgorithm GetShortestPathAlgorithm();

        [TestMethod]
        public void SmallGraphShortestPathAtoF()
        {
            var graph = GraphGenerator.GetSmallGraph();
            var algorithm = GetShortestPathAlgorithm();
            var shortestPath = algorithm.GetShortestPath(graph, graph[0].Origin, graph[1].Destination);
            Assert.IsNotNull(shortestPath);
            Assert.AreEqual(3, shortestPath.Length);
            Assert.AreEqual(1, shortestPath[0].Weight);
            Assert.AreEqual(2, shortestPath[1].Weight);
        }
    }
}