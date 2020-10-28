using DotnetGraph.Algorithms.ShortestPath;
using DotnetGraph.Algorithms.ShortestPath.Dijkstra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.ShortestPath
{
    [TestClass]
    public class DijkstraAlgorithmTests : ShortestPathTests
    {
        protected override IShortestPathAlgorithm GetShortestPathAlgorithm()
        {
            return new DijkstraAlgorithm();
        }
    }
}