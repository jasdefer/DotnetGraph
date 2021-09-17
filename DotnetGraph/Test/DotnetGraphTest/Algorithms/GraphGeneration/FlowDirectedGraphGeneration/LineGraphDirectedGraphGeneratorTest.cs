using DotnetGraph.Algorithms.GraphGeneration.FlowDirectedGraphGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.GraphGeneration.FlowDirectedGraphGeneration
{
    [TestClass]
    public class LineGraphDirectedGraphGeneratorTest : FlowDirectedGraphGenerationTests
    {
        protected override IFlowDirectedGraphGenerator GetGenerator()
        {
            return new LineGraphDirectedGraphGenerator();
        }
    }
}