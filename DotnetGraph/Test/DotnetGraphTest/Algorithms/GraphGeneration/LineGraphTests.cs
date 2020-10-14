using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.LineGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.GraphGeneration
{
    [TestClass]
    public class LineGraphTests : WeightedDirectedGraphGenerationTests
    {
        protected override IWeightedDirectGraphGenerator GetWeightedDirectGraphGenerator()
        {
            return new LineGraphGenerator();
        }
    }
}