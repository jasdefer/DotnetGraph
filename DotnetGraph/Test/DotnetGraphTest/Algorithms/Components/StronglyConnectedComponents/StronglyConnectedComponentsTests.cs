using DotnetGraph.Algorithms.Components.StronglyConnectedComponents;
using DotnetGraph.Model.Implementations.Graph.DirectedGraph;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using DotnetGraphTest.Helper;

namespace DotnetGraphTest.Algorithms.Components.StronglyConnectedComponents
{
    [TestClass]
    public abstract class StronglyConnectedComponentsTests
    {
        protected abstract IGetStronglyConnectedComponents GetAlgorithm();

        [TestMethod]
        public void TwoUnconnectedNodes()
        {
            //Data preparation
            var nodes = new DirectedGraphNode[]
            {
                new DirectedGraphNode(1),
                new DirectedGraphNode(2),
            };

            var algorithm = GetAlgorithm();
            var componentResult = algorithm.GetCompontents<DirectedGraphNode, DirectedGraphArc>(nodes);

            Assert.AreEqual(2, componentResult.NumberOfComponents);
            Assert.AreEqual(2, componentResult.Components.Count);
            Assert.AreEqual(1, componentResult.Components[0].Count);
            Assert.AreEqual(1, componentResult.Components[1].Count);
        }

        [TestMethod]
        public void TinyUnconnectedGraph()
        {
            var nodes = new DirectedGraphNode[]
            {
                new DirectedGraphNode(1),
                new DirectedGraphNode(2),
                new DirectedGraphNode(3)
            };
            nodes[0].Add(new DirectedGraphArc(1, nodes[1]));
            nodes[1].Add(new DirectedGraphArc(1, nodes[0]));
            nodes[1].Add(new DirectedGraphArc(1, nodes[2]));

            var algorithm = GetAlgorithm();
            var componentResult = algorithm.GetCompontents<DirectedGraphNode, DirectedGraphArc>(nodes);

            Assert.AreEqual(2, componentResult.NumberOfComponents);
            Assert.AreEqual(nodes.Length, componentResult.Components.Sum(x => x.Count));
            Assert.IsFalse(componentResult.Components.Any(x => x.Count < 1));
        }

        [TestMethod]
        public void SmallUnconnectedGraph()
        {
            var nodes = GraphLibrary.SmallDirectedGraph();

            var algorithm = GetAlgorithm();
            var componentResult = algorithm.GetCompontents<DirectedGraphNode, DirectedGraphArc>(nodes);

            Assert.AreEqual(3, componentResult.NumberOfComponents);
            for (int i = 0; i < componentResult.NumberOfComponents; i++)
            {
                Assert.IsTrue(componentResult.Components.ElementAt(i).Count > 1, "Empty component");
            }
            Assert.AreEqual(nodes.Length, componentResult.Components.Sum(x => x.Count));
        }

        [TestMethod]
        public void SmallGraph()
        {
            var nodes = GraphLibrary.SmallWeightedDirectedGraph();
            var algorithm = GetAlgorithm();
            var componentResult = algorithm.GetCompontents<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes);
            Assert.IsNotNull(componentResult);
            Assert.AreEqual(1, componentResult.NumberOfComponents);
            Assert.AreEqual(1, componentResult.Components.Count);
            Assert.AreEqual(nodes.Length, componentResult.Components[0].Count);
        }
    }
}