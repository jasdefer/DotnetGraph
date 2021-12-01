using DotnetGraph.Algorithms.Components.ConnectedComponents;
using DotnetGraph.Model.Implementations.Graph.UndirectedGraph;

namespace DotnetGraphTest.Algorithms.Components.ConnectedComponents
{
    [TestClass]
    public abstract class ConnectedComponentsTests
    {
        protected abstract IConnectedComponentsAlgorithm GetAlgorithm();

        [TestMethod]
        public void TwoNodes()
        {
            var nodes = new UndirectedGraphNode[2];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new UndirectedGraphNode(i + 1);
            }
            var edges = new UndirectedGraphEdge[]{
                new UndirectedGraphEdge(1, nodes[0], nodes[1])
            };
            for (int i = 0; i < edges.Length; i++)
            {
                edges[i].Node1.Add(edges[i]);
                edges[i].Node2.Add(edges[i]);
            }
            var algorithm = GetAlgorithm();
            var componentResult = algorithm.GetComponents<UndirectedGraphNode, UndirectedGraphEdge>(nodes);
            Assert.IsNotNull(componentResult);
            Assert.AreEqual(1, componentResult.NumberOfComponents);
            Assert.AreEqual(1, componentResult.Components.Count);
            Assert.AreEqual(2, componentResult.Components.Single().Nodes.Count);
        }

        [TestMethod]
        public void FourNodes()
        {
            var nodes = new UndirectedGraphNode[4];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new UndirectedGraphNode(i + 1);
            }
            var edges = new UndirectedGraphEdge[]{
                new UndirectedGraphEdge(1, nodes[0], nodes[1]),
                new UndirectedGraphEdge(2, nodes[2], nodes[3])
            };
            for (int i = 0; i < edges.Length; i++)
            {
                edges[i].Node1.Add(edges[i]);
                edges[i].Node2.Add(edges[i]);
            }
            var algorithm = GetAlgorithm();
            var componentResult = algorithm.GetComponents<UndirectedGraphNode, UndirectedGraphEdge>(nodes);
            Assert.IsNotNull(componentResult);
            Assert.AreEqual(2, componentResult.NumberOfComponents);
            Assert.AreEqual(2, componentResult.Components.Count);
            Assert.AreEqual(2, componentResult.Components[0].Nodes.Count);
            Assert.AreEqual(2, componentResult.Components[1].Nodes.Count);
        }

        [TestMethod]
        public void EightNodes()
        {
            var nodes = new UndirectedGraphNode[9];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new UndirectedGraphNode(i + 1);
            }
            var edges = new UndirectedGraphEdge[]{
                new UndirectedGraphEdge(1, nodes[0], nodes[3]),
                new UndirectedGraphEdge(2, nodes[2], nodes[1]),
                new UndirectedGraphEdge(2, nodes[3], nodes[2]),
                new UndirectedGraphEdge(2, nodes[3], nodes[4]),
                new UndirectedGraphEdge(2, nodes[6], nodes[5]),
                new UndirectedGraphEdge(2, nodes[7], nodes[8]),
                new UndirectedGraphEdge(2, nodes[7], nodes[5]),
                new UndirectedGraphEdge(2, nodes[5], nodes[3]),
            };
            for (int i = 0; i < edges.Length; i++)
            {
                edges[i].Node1.Add(edges[i]);
                edges[i].Node2.Add(edges[i]);
            }
            var algorithm = GetAlgorithm();
            var componentResult = algorithm.GetComponents<UndirectedGraphNode, UndirectedGraphEdge>(nodes);
            Assert.IsNotNull(componentResult);
            Assert.AreEqual(1, componentResult.NumberOfComponents);
            Assert.AreEqual(1, componentResult.Components.Count);
            Assert.AreEqual(9, componentResult.Components[0].Nodes.Count);
        }

        [TestMethod]
        public void TenNodes()
        {
            var nodes = new UndirectedGraphNode[10];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new UndirectedGraphNode(i + 1);
            }
            var edges = new UndirectedGraphEdge[]{
                //Component 1
                new UndirectedGraphEdge(1, nodes[0], nodes[1]),
                new UndirectedGraphEdge(2, nodes[0], nodes[2]),
                new UndirectedGraphEdge(2, nodes[1], nodes[3]),
                new UndirectedGraphEdge(2, nodes[1], nodes[4]),
                new UndirectedGraphEdge(2, nodes[2], nodes[4]),
                new UndirectedGraphEdge(2, nodes[5], nodes[0]),
                //Component 2
                new UndirectedGraphEdge(2, nodes[6], nodes[7]),
                new UndirectedGraphEdge(2, nodes[7], nodes[8]),
                new UndirectedGraphEdge(2, nodes[8], nodes[9]),
            };
            for (int i = 0; i < edges.Length; i++)
            {
                edges[i].Node1.Add(edges[i]);
                edges[i].Node2.Add(edges[i]);
            }
            var algorithm = GetAlgorithm();
            var componentResult = algorithm.GetComponents<UndirectedGraphNode, UndirectedGraphEdge>(nodes);
            Assert.IsNotNull(componentResult);
            Assert.AreEqual(2, componentResult.NumberOfComponents);
            Assert.AreEqual(2, componentResult.Components.Count);
        }
    }
}