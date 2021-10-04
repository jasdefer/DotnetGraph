using DotnetGraph.Algorithms.Components.ConnectedComponents.SimpleConnectedComponent;
using DotnetGraph.Algorithms.GraphGeneration.UndirectedGraphGeneration;
using DotnetGraph.Model.Implementations.Graph.UndirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.GraphGeneration.UndirectedGraphGeneration
{
    [TestClass]
    public class LineGraphTests : UndirectedGraphGeneratorFixture
    {
        protected override IUndirectedGraphGenerator GetGenerator()
        {
            return new LineGraphGenerator();
        }

        protected override void AssertNodes(UndirectedGraphNode[] nodes)
        {
            var componentAlgorithm = new SimpleConnectedComponentAlgorithm();
            var components = componentAlgorithm.GetComponents<UndirectedGraphNode, UndirectedGraphEdge>(nodes);
            Assert.AreEqual(1, components.NumberOfComponents);
        }
    }
}