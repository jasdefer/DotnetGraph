using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.MinimumSpanningTreeTests
{
    [TestClass]
    public abstract class MinimumSpanningTreeFixture
    {
        protected abstract IMinimumSpanningTreeAlgorithm GetAlgorithm();

        [TestMethod]
        public void SmallGraph()
        {
            var graph = UndirectedGraphGenerator.GetSmallGraph();
            var algorithm = GetAlgorithm();
            var tree = algorithm.GetMinimumSpanningTree(graph);
            Assert.IsNotNull(tree);
            Assert.AreEqual(5, tree.Length);
            Assert.AreEqual(6, tree.TotalWeight());
        }

        [TestMethod]
        public void GraphWithNegativeEdge()
        {
            var graph = UndirectedGraphGenerator.GetSmallGraphWithNegativeEdge();
            var algorithm = GetAlgorithm();
            var tree = algorithm.GetMinimumSpanningTree(graph);
            Assert.IsNotNull(tree);
            Assert.AreEqual(5, tree.Length);
            Assert.AreEqual(4, tree.TotalWeight());
        }
    }
}