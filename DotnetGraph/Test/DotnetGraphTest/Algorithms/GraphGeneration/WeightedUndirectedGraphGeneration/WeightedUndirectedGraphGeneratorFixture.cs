using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration;

namespace DotnetGraphTest.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration;

[TestClass]
public abstract class WeightedUndirectedGraphGeneratorFixture
{
    protected abstract IWeightedUndirectedGraphGenerator GetGenerator();
}
