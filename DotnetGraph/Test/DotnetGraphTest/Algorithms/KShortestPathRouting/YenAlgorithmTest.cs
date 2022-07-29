using DotnetGraph.Algorithms.KShortestPathRouting;
using DotnetGraph.Algorithms.KShortestPathRouting.Yen;

namespace DotnetGraphTest.Algorithms.KShortestPathRouting;

[TestClass]
public class YenAlgorithmTest : KShortestPathRoutingTest
{
    protected override IKShortestPathRoutingAlgorithm GetAlgorithm()
    {
        return new YenAlgorithm();
    }
}
