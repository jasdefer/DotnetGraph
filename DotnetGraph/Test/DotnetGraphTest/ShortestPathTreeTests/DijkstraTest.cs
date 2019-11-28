using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Algorithms.ShortestPathTree;
using DotnetGraph.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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