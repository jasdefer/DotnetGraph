using DotnetGraph.Algorithms.Components.StronglyConnectedComponents;
using DotnetGraph.Model.Implementations.Graph.DirectedGraph;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using DotnetGraphTest.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void SmallUnconnectedGraph()
        {
            var nodes = GraphLibrary.SmallDirectedGraph();

            var algorithm = GetAlgorithm();
            var componentResult = algorithm.GetCompontents<DirectedGraphNode, DirectedGraphArc>(nodes);

            Assert.AreEqual(3, componentResult.NumberOfComponents);
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