using DotnetGraph.Algorithms.Components.StronglyConnectedComponents;
using DotnetGraph.Algorithms.Components.StronglyConnectedComponents.Tarjan;

namespace DotnetGraphTest.Algorithms.Components.StronglyConnectedComponents
{
    [TestClass]
    public class TarjanAlgorithmTests : StronglyConnectedComponentsTests
    {
        protected override IGetStronglyConnectedComponents GetAlgorithm()
        {
            return new TarjanAlgorithm();
        }
    }
}