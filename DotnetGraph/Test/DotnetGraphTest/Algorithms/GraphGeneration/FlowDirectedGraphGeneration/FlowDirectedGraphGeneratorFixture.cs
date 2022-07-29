using DotnetGraph.Algorithms.GraphGeneration.FlowDirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Model.Implementations.Graph.FlowDirectedGraph;

namespace DotnetGraphTest.Algorithms.GraphGeneration.FlowDirectedGraphGeneration;

[TestClass]
public abstract class FlowDirectedGraphGeneratorFixture
{
    protected abstract IFlowDirectedGraphGenerator GetGenerator();

    protected virtual void AssertNodes(FlowDirectedGraphNode[] nodes) { }

    [TestMethod]
    public void Monkey()
    {
        // Arrange
        var generator = GetGenerator();
        var capacityGenerator = new ConstantNumber();

        for (int i = 0; i < 20; i++)
        {
            // Act
            var numberOfNodes = (i + 1) * 10;
            var density = (2.5 + (i % 3 == 0 ? 2 : 0)) / (double)(numberOfNodes);
            var nodes = generator.Generate(numberOfNodes, density, capacityGenerator);
            // Assert
            Assert.IsNotNull(nodes);
            Assert.AreEqual(numberOfNodes, nodes.Length);
            var numberOfArcs = nodes.Sum(x => x.OutgoingArcs.Count);
            var numberOfExpectedArcs = numberOfNodes * numberOfNodes * density;
            var delta = Math.Max(3, numberOfExpectedArcs * 0.2);
            Assert.AreEqual(numberOfExpectedArcs, numberOfArcs, delta);
            AssertNodes(nodes);
        }
    }
}
