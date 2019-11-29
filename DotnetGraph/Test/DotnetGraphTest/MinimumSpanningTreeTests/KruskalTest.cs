using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Algorithms.MinimumSpanningTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.MinimumSpanningTreeTests
{
    [TestClass]
    public class KruskalTest : MinimumSpanningTreeFixture
    {
        protected override IMinimumSpanningTreeAlgorithm GetAlgorithm()
        {
            return new Kruskal();
        }
    }
}