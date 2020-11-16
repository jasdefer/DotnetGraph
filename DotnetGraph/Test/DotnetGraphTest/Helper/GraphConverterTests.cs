using DotnetGraph.Helper;
using DotnetGraph.Model.Implementations.Graph.UndirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}