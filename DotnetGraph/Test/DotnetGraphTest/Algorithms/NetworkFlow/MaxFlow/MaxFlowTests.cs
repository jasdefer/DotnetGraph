using DotnetGraph.Algorithms.GraphGeneration.FlowDirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.NetworkFlow.MaxFlow;
using DotnetGraph.Helper;
using DotnetGraph.Model.Implementations.Graph.FlowDirectedGraph;
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

        [TestMethod]
        public void SmallGraph()
        {
            var originNodeId = 0;
            var destinationNodeId = 5;
            var nodes = new FlowDirectedGraphNode[]
            {
                new FlowDirectedGraphNode(0),
                new FlowDirectedGraphNode(1),
                new FlowDirectedGraphNode(2),
                new FlowDirectedGraphNode(3),
                new FlowDirectedGraphNode(4),
                new FlowDirectedGraphNode(5)
            };

            nodes[0].Add(new FlowDirectedGraphArc(1, 16, nodes[1]));
            nodes[0].Add(new FlowDirectedGraphArc(2, 13, nodes[2]));
            nodes[1].Add(new FlowDirectedGraphArc(3, 12, nodes[3]));
            nodes[2].Add(new FlowDirectedGraphArc(4, 4, nodes[1]));
            nodes[2].Add(new FlowDirectedGraphArc(5, 14, nodes[4]));
            nodes[3].Add(new FlowDirectedGraphArc(6, 9, nodes[2]));
            nodes[3].Add(new FlowDirectedGraphArc(7, 20, nodes[5]));
            nodes[4].Add(new FlowDirectedGraphArc(8, 7, nodes[3]));
            nodes[4].Add(new FlowDirectedGraphArc(9, 4, nodes[5]));
            var maxFlowAlgorithm = GetAlgorithm();
            maxFlowAlgorithm.SetFlow<FlowDirectedGraphNode, FlowDirectedGraphArc>(nodes, originNodeId, destinationNodeId);
            AssertFlow<FlowDirectedGraphNode, FlowDirectedGraphArc>(nodes, originNodeId, destinationNodeId);
            Assert.AreEqual(23, nodes.Single(x => x.Id == originNodeId).OutgoingArcs.Sum(x => x.Flow));
            Assert.AreEqual(23, nodes.SelectMany(x => x.OutgoingArcs).Where(x => x.Destination.Id == destinationNodeId).Sum(x => x.Flow));
        }

        [TestMethod]
        public void Monkey()
        {
            var generator = new CornerFlowFlowDirectedGraphGenerator();
            var capacityGenerator = new UniformNumberGenerator(1,10);
            var algorithm = GetAlgorithm();
            for (int i = 0; i < 100; i++)
            {
                var numberOfNodes = (i + 1) * 10;
                var density = 2.5 / numberOfNodes;
                var nodes = generator.Generate(numberOfNodes, density, capacityGenerator);
                PrintGraph.PrintFlowDirectedGraph<FlowDirectedGraphNode, FlowDirectedGraphArc>("flow.csv", nodes);
                algorithm.SetFlow<FlowDirectedGraphNode, FlowDirectedGraphArc>(nodes, 1, numberOfNodes);
                Assert.IsNotNull(nodes);
                Assert.AreEqual(numberOfNodes, nodes.Length);
                AssertFlow<FlowDirectedGraphNode, FlowDirectedGraphArc>(nodes, 1, numberOfNodes);
            }
        }
    }
}