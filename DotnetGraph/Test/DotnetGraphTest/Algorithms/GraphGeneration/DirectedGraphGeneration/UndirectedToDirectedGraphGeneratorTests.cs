using DotnetGraph.Algorithms.GraphGeneration.DirectedGraphGeneration;
using DotnetGraph.Model.Implementations.Graph.DirectedGraph;

namespace DotnetGraphTest.Algorithms.GraphGeneration.DirectedGraphGeneration
{
    [TestClass]
    public class UndirectedToDirectedGraphGeneratorTests : DirectedGraphGenerationFixture
    {
        protected override IDirectedGraphGenerator GetGenerator()
        {
            return new UndirectedToDirectedGraphGenerator();
        }

        protected override void AssertNodes(DirectedGraphNode[] nodes)
        {
            foreach (var node in nodes)
            {
                foreach (var arc in node.OutgoingArcs)
                {
                    Assert.IsTrue(arc.Destination.OutgoingArcs.Any(x => x.Destination.Id == node.Id));
                }
            }
        }
    }
}