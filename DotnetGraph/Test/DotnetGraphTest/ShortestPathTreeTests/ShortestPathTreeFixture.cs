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
        }
    }
}