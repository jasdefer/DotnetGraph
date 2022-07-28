using DotnetGraph.Algorithms.Components.ConnectedComponents;
using DotnetGraph.Algorithms.Components.ConnectedComponents.SimpleConnectedComponent;

namespace DotnetGraphTest.Algorithms.Components.ConnectedComponents;

[TestClass]
public class SimpleConnectedComponentAlgorithmTests : ConnectedComponentsTests
{
    protected override IConnectedComponentsAlgorithm GetAlgorithm()
    {
        return new SimpleConnectedComponentAlgorithm();
    }
}
