using DotnetGraph.Algorithms.GraphGeneration.FlowDirectedGraphGeneration;

namespace DotnetGraphTest.Algorithms.GraphGeneration.FlowDirectedGraphGeneration
{
    [TestClass]
    public class FlowDirectedGraphGeneratorTests : FlowDirectedGraphGeneratorFixture
    {
        protected override IFlowDirectedGraphGenerator GetGenerator()
        {
            return new FlowDirectedGraphGenerator();
        }
    }
}