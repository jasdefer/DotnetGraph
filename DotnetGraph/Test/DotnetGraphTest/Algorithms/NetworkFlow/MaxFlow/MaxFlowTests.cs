using DotnetGraph.Algorithms.NetworkFlow.MaxFlow;
using DotnetGraph.Model.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraphTest.Algorithms.NetworkFlow.MaxFlow
{
    [TestClass]
    public abstract class MaxFlowTests
    {
        protected abstract IMaxFlowAlgorithm GetAlgorithm();

        protected void AssertFlow<TNode, TArc>(IList<TNode> nodes, int originNodeId, int destinationNodeId)
            where TNode : IHasOutgoingArcs<TArc>, IHasId
            where TArc : IHasId, IHasCapacity, IHasFlow, IHasDestination<TNode>
        {
            var dict = nodes.ToDictionary(x => x.Id, x => 0d);
            foreach (var node in nodes)
            {
                foreach (var arc in node.OutgoingArcs)
                {
                    Assert.IsTrue(arc.Capacity >= arc.Flow);
                    Assert.IsTrue(arc.Flow >= 0);
                    dict[node.Id] -= arc.Flow;
                    dict[arc.Destination.Id] += arc.Flow;
                }
            }
            Assert.AreEqual(dict[originNodeId], -dict[destinationNodeId]);
            dict.Remove(originNodeId);
            dict.Remove(destinationNodeId);
            Assert.IsFalse(dict.Any(x => x.Value != 0));
        }
    }
}