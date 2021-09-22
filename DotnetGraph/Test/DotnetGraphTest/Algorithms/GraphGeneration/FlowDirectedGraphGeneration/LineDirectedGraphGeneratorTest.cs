using DotnetGraph.Algorithms.GraphGeneration.FlowDirectedGraphGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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