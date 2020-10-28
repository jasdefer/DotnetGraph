using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration.ErdosRenyi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration
{
    [TestClass]
    public class ErdosRenyiTests : WeightedUndirectedGraphGenerationTests
    {
        protected override IWeightedUndirectedGraphGenerator GetGenerator()
        {
            return new ErdosRenyiGenerator();
        }
    }
}