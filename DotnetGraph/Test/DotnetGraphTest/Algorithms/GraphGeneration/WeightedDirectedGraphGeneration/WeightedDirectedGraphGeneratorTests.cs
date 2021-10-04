using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration
{
    [TestClass]
    public class WeightedDirectedGraphGeneratorTests : WeightedDirectedGraphGenerationFixture
    {
        protected override IWeightedDirectedGraphGenerator GetGenerator()
        {
            return new WeightedDirectedGraphGenerator();
        }
    }
}