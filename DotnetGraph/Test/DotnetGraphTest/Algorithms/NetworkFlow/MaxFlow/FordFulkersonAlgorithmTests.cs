using DotnetGraph.Algorithms.NetworkFlow.MaxFlow;
using DotnetGraph.Algorithms.NetworkFlow.MaxFlow.FordFulkerson;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DotnetGraphTest.Algorithms.NetworkFlow.MaxFlow
{
    [TestClass]
    public class FordFulkersonAlgorithmTests : MaxFlowTests
    {
        protected override IMaxFlowAlgorithm GetAlgorithm()
        {
            return new FordFulkersonAlgorithm();
        }

        [TestMethod]
        public void Custom()
        {
            var originNodeId = 0;
            var destinationNodeId = 5;
            var nodes = new FordFulkersonNode[]
            {
                new FordFulkersonNode(0),
                new FordFulkersonNode(1),
                new FordFulkersonNode(2),
                new FordFulkersonNode(3),
                new FordFulkersonNode(4),
                new FordFulkersonNode(5)
            };

            nodes[0].AddOutgoingArc(new FordFulkersonArc(1, 16, nodes[0], nodes[1]));
            nodes[0].AddOutgoingArc(new FordFulkersonArc(2, 13, nodes[0], nodes[2]));
            nodes[1].AddOutgoingArc(new FordFulkersonArc(3, 12, nodes[1], nodes[3]));
            nodes[2].AddOutgoingArc(new FordFulkersonArc(4, 4, nodes[2], nodes[1]));
            nodes[2].AddOutgoingArc(new FordFulkersonArc(5, 14, nodes[2], nodes[4]));
            nodes[3].AddOutgoingArc(new FordFulkersonArc(6, 9, nodes[3], nodes[2]));
            nodes[3].AddOutgoingArc(new FordFulkersonArc(7, 20, nodes[3], nodes[5]));
            nodes[4].AddOutgoingArc(new FordFulkersonArc(8, 7, nodes[4], nodes[3]));
            nodes[4].AddOutgoingArc(new FordFulkersonArc(9, 4, nodes[4], nodes[5]));

            FordFulkersonAlgorithm.SetFlow(nodes, originNodeId, destinationNodeId);
            AssertFlow<FordFulkersonNode, FordFulkersonArc>(nodes, originNodeId, destinationNodeId);
            Assert.IsTrue(nodes.Single(x => x.Id == originNodeId).OutgoingArcs.Sum(x => x.Flow) == 23);
            Assert.IsTrue(nodes.Single(x => x.Id == destinationNodeId).IncomingArcs.Sum(x => x.Flow) == 23);
        }
    }
}
