using DotnetGraph.Algorithms.ShortestPath;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraphTest.Algorithms.ShortestPath
{
    [TestClass]
    public abstract class ShortestPathTests
    {
        protected abstract IShortestPathAlgorithm GetShortestPathAlgorithm();

        [TestMethod]
        public void Test()
        {
            //Data preparation
            var nodes = new List<WeightedDirectedGraphNode>()
            {
                new WeightedDirectedGraphNode(1),
                new WeightedDirectedGraphNode(2),
                new WeightedDirectedGraphNode(3),
                new WeightedDirectedGraphNode(4),
                new WeightedDirectedGraphNode(5),
                new WeightedDirectedGraphNode(6)
            };

            nodes[0].AddArc(new WeightedDirectedGraphArc(1, nodes[1], 5));
            nodes[1].AddArc(new WeightedDirectedGraphArc(2, nodes[0], 5));

            nodes[0].AddArc(new WeightedDirectedGraphArc(3, nodes[3], 1));
            nodes[3].AddArc(new WeightedDirectedGraphArc(4, nodes[0], 1));

            nodes[1].AddArc(new WeightedDirectedGraphArc(5, nodes[2], 3));
            nodes[2].AddArc(new WeightedDirectedGraphArc(6, nodes[1], 3));

            nodes[1].AddArc(new WeightedDirectedGraphArc(7, nodes[4], 2));
            nodes[4].AddArc(new WeightedDirectedGraphArc(8, nodes[1], 2));

            nodes[2].AddArc(new WeightedDirectedGraphArc(9, nodes[5], 1));
            nodes[5].AddArc(new WeightedDirectedGraphArc(10, nodes[2], 1));

            nodes[3].AddArc(new WeightedDirectedGraphArc(11, nodes[4], 1));
            nodes[4].AddArc(new WeightedDirectedGraphArc(12, nodes[3], 1));

            nodes[4].AddArc(new WeightedDirectedGraphArc(13, nodes[5], 1));
            nodes[5].AddArc(new WeightedDirectedGraphArc(14, nodes[4], 1));

            //Run test method
            var algorithm = GetShortestPathAlgorithm();
            var result = algorithm.GetShortestPath<WeightedDirectedGraphArc, WeightedDirectedGraphNode>(nodes, 1, 6);

            //Validate results
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.TotalWeight);
            Assert.AreEqual(3, result.Arcs.Count);

            Assert.AreEqual(3, result.Arcs[0].Id);
            Assert.AreEqual(11, result.Arcs[1].Id);
            Assert.AreEqual(13, result.Arcs[2].Id);

            Assert.AreEqual(nodes[0].OutgoingArcs.ElementAt(1), result.Arcs[0]);
            Assert.AreEqual(nodes[3].OutgoingArcs.ElementAt(1), result.Arcs[1]);
            Assert.AreEqual(nodes[4].OutgoingArcs.ElementAt(2), result.Arcs[2]);
        }
    }
}
