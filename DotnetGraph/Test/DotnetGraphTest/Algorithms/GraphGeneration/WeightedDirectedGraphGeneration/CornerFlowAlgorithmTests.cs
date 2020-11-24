using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.CornerFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration
{
    [TestClass]
    public class CornerFlowAlgorithmTests : WeightedDirectedGraphGenerationTests
    {
        protected override IWeightedDirectedGraphGenerator GetGenerator()
        {
            return new CornerFlowAlgorithm();
        }
    }
}