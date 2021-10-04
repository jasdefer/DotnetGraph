using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration
{
    [TestClass]
    public abstract class WeightedUndirectedGraphGeneratorFixture
    {
        protected abstract IWeightedUndirectedGraphGenerator GetGenerator();
    }
}