using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration;
using DotnetGraph.Algorithms.ShortestPath.Dijkstra;
using DotnetGraph.Algorithms.ShortestPathTree;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using DotnetGraphTest.Helper;

namespace DotnetGraphTest.Algorithms.ShortestPathTree
{
    [TestClass]
    public abstract class ShortestPathTreeTests
    {
        protected abstract IShortestPathTreeAlgorithm GetAlgorithm();

        [TestMethod]
        public void SmallGraph()
        {
            //Data preparation
            var nodes = GraphLibrary.SmallComplicatedWeightedDirectedGraph();
            var algorithm = GetAlgorithm();

            //Run
            var result = algorithm.GetShortestPathTree<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes, 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Origin, nodes[0]);
            Assert.AreEqual(8, result.ShortestPaths.Count);
            Assert.AreEqual(38, result.ShortestPaths[2].Sum(x => x.Weight));
            Assert.AreEqual(15, result.ShortestPaths[3].Sum(x => x.Weight));
            Assert.AreEqual(27, result.ShortestPaths[4].Sum(x => x.Weight));
            Assert.AreEqual(54, result.ShortestPaths[5].Sum(x => x.Weight));
            Assert.AreEqual(43, result.ShortestPaths[6].Sum(x => x.Weight));
            Assert.AreEqual(65, result.ShortestPaths[7].Sum(x => x.Weight));
            Assert.AreEqual(82, result.ShortestPaths[8].Sum(x => x.Weight));
            Assert.AreEqual(95, result.ShortestPaths[9].Sum(x => x.Weight));
        }

        [TestMethod]
        public void Monkey()
        {
            var algorithm = GetAlgorithm();
            var generator = new LineWeightedDirectedGraphGenerator();
            var weightGenerator = new UniformNumberGenerator();
            var dijkstra = new DijkstraAlgorithm();
            for (int i = 0; i < 2; i++)
            {
                var numberOfNodes = 10 + 500 * i;
                var density = 4d / (numberOfNodes + 1);
                var nodes = generator.Generate(numberOfNodes, density, weightGenerator);
                var result = algorithm.GetShortestPathTree<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes, 1);
                Assert.IsNotNull(result);
                Assert.AreEqual(nodes[0], result.Origin);
                Assert.AreEqual(numberOfNodes - 1, result.ShortestPaths.Count);
                foreach (var path in result.ShortestPaths)
                {
                    var dijkstraSolution = dijkstra.GetShortestPath<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes, 1, path.Key);
                    Assert.AreEqual(dijkstraSolution.TotalWeight, path.Value.Sum(x => x.Weight));
                }
            }
        }
    }
}