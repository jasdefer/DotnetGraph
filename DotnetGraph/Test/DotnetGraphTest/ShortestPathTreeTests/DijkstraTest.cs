using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Algorithms.ShortestPathTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.ShortestPathTreeTests
{
    [TestClass]
    public class DijkstraTest : ShortestPathTreeFixture
    {
        public override IShortestPathTreeAlgorithm GetShortestPathTreeAlgorithm()
        {
            return new Dijkstra();
        }
    }
}