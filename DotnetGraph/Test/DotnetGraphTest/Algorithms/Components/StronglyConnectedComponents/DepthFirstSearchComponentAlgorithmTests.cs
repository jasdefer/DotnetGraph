using DotnetGraph.Algorithms.Components.StronglyConnectedComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.Algorithms.Components.StronglyConnectedComponents
{
    [TestClass]
    public class DepthFirstSearchComponentAlgorithmTests : StronglyConnectedComponentsTests
    {
        protected override IGetStronglyConnectedComponents GetAlgorithm()
        {
            return new DepthFirstSearchComponentAlgorithm();
        }
    }
}