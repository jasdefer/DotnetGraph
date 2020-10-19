using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration.LineGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration
{
    [TestClass]
    public class LineGraphTests : WeightedUndirectedGraphGenerationTests
    {
        protected override IWeightedUndirectedGraphGenerator GetGenerator()
        {
            return new LineGraphGenerator();
        }
    }
}