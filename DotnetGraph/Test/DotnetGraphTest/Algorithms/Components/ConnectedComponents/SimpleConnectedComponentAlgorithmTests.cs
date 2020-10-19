using DotnetGraph.Algorithms.Components.ConnectedComponents;
using DotnetGraph.Algorithms.Components.ConnectedComponents.SimpleConnectedComponent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.Components.ConnectedComponents
{
    [TestClass]
    public class SimpleConnectedComponentAlgorithmTests : ConnectedComponentsTests
    {
        protected override IConnectedComponentsAlgorithm GetAlgorithm()
        {
            return new SimpleConnectedComponentAlgorithm();
        }
    }
}