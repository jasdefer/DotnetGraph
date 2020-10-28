using DotnetGraph.Algorithms.MinimumSpanningTree;
using DotnetGraph.Algorithms.MinimumSpanningTree.Kruskal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.MinimumSpanningTree
{
    [TestClass]
    public class KruskalAlgorithmTests : MinimumSpanningTreeTests
    {
        protected override IMinimumSpanningTreeAlgorithm GetAlgorithm()
        {
            return new KruskalAlgorithm();
        }
    }
}