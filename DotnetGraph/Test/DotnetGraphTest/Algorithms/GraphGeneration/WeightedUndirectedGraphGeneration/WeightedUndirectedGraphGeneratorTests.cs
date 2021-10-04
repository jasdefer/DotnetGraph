using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration
{
    [TestClass]
    public class WeightedUndirectedGraphGeneratorTests : WeightedUndirectedGraphGeneratorFixture
    {
        protected override IWeightedUndirectedGraphGenerator GetGenerator()
        {
            return new WeightedUndirectedGraphGenerator();
        }
    }
}