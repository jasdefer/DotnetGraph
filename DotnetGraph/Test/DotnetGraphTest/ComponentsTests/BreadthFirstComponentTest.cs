using DotnetGraph.Algorithms.ComponentAlgorithm;
using DotnetGraph.Algorithms.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetGraphTest.ComponentsTests
{
    [TestClass]
    public class BreadthFirstComponentTest : ComponentsFixture
    {
        protected override IComponentAlgorithm GetComponentAlgorithm()
        {
            return new BreadthFirstComponent();
        }
    }
}