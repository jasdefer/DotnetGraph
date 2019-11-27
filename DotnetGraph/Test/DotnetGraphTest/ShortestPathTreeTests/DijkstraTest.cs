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

        [TestMethod]
        public void GetSuccessors()
        {
            var nodes = new Dictionary<int, Node>
            {
                {0, new Node("A") },
                {1, new Node("B") },
                {2, new Node("C") }
            };
            //A 3x -> B
            //A 1x -> C
            //C 1x -> B
            var arcs = new Arc<Node>[]
            {
                new Arc<Node>(nodes[0], nodes[1], 2),
                new Arc<Node>(nodes[0], nodes[1], 1),
                new Arc<Node>(nodes[0], nodes[1], 3),
                new Arc<Node>(nodes[0], nodes[2], 10),
                new Arc<Node>(nodes[1], nodes[2], 2)
            };
            var successors = Dijkstra.GetSuccessors(arcs, nodes.ToDictionary(x => x.Value, x=> x.Key), 0);
            Assert.IsNotNull(successors);
            Assert.AreEqual(2, successors.Length);
            Assert.AreEqual(1, successors[0].Node);
            Assert.AreEqual(1, successors[0].Weight);
            Assert.AreEqual(2, successors[1].Node);
            Assert.AreEqual(10, successors[1].Weight);
        }
    }
}