using DotnetGraph.Helper;
using DotnetGraph.Model.Implementations.Graph.UndirectedGraph;

namespace DotnetGraphTest.Helper
{
    [TestClass]
    public class GraphConverterTests
    {
        [TestMethod]
        public void GetNodes()
        {
            //Create data
            var nodes = new UndirectedGraphNode[]
            {
                new UndirectedGraphNode(1),
                new UndirectedGraphNode(2),
                new UndirectedGraphNode(3)
            };
            var edges = new UndirectedGraphEdge[]
            {
                new UndirectedGraphEdge(1, nodes[0], nodes[1]),
                new UndirectedGraphEdge(1, nodes[1], nodes[2]),
            };
            nodes[0].Add(edges[0]);
            nodes[1].Add(edges[0]);
            nodes[1].Add(edges[1]);
            nodes[2].Add(edges[1]);

            //Run method
            var convertedNodes = GraphConverter.GetNodes<UndirectedGraphNode, UndirectedGraphEdge>(edges);

            //Assert
            Assert.IsNotNull(convertedNodes);
            Assert.AreEqual(3, convertedNodes.Length);
            Assert.AreEqual(nodes[0], convertedNodes[0]);
            Assert.AreEqual(nodes[1], convertedNodes[1]);
            Assert.AreEqual(nodes[2], convertedNodes[2]);
        }

        [TestMethod]
        public void GraphFromString()
        {
            var nodes = GraphConverter.GetNodes("1,2,1\r\n2,3,2\r\n1,3,5", ",");
            Assert.IsNotNull(nodes);
            Assert.AreEqual(3, nodes.Length);
            Assert.AreEqual(1, nodes[0].Id);
            Assert.AreEqual(2, nodes[1].Id);
            Assert.AreEqual(3, nodes[2].Id);
            Assert.AreEqual(2, nodes[0].OutgoingArcs.Count);
            Assert.AreEqual(1, nodes[1].OutgoingArcs.Count);
            Assert.AreEqual(0, nodes[2].OutgoingArcs.Count);
            Assert.AreEqual(2, nodes[0].OutgoingArcs.ElementAt(0).Destination.Id);
            Assert.AreEqual(1, nodes[0].OutgoingArcs.ElementAt(0).Weight);
            Assert.AreEqual(3, nodes[0].OutgoingArcs.ElementAt(1).Destination.Id);
            Assert.AreEqual(5, nodes[0].OutgoingArcs.ElementAt(1).Weight);
            Assert.AreEqual(3, nodes[1].OutgoingArcs.Single().Destination.Id);
            Assert.AreEqual(2, nodes[1].OutgoingArcs.Single().Weight);
        }
    }
}