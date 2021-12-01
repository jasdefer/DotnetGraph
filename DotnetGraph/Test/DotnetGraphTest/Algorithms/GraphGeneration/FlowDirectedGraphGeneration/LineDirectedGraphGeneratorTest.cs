using DotnetGraph.Algorithms.GraphGeneration.FlowDirectedGraphGeneration;

namespace DotnetGraphTest.Algorithms.GraphGeneration.FlowDirectedGraphGeneration
{
    [TestClass]
    public class LineDirectedGraphGeneratorTest : FlowDirectedGraphGeneratorFixture
    {
        protected override IFlowDirectedGraphGenerator GetGenerator()
        {
            return new LineFlowDirectedGraphGenerator();
        }
    }
}