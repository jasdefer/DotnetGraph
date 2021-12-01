using DotnetGraph.Algorithms.DepthFirstSearch;
using DotnetGraph.Algorithms.GraphGeneration.DirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.DiscoverableDirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.UndirectedGraphGeneration;
using DotnetGraph.Model.Implementations.Graph.DiscoverableDirectedGraph;

namespace DotnetGraphTest.Algorithms.DfSearch
{
    [TestClass]
    public abstract class DepthFirstSearchTests
    {
        protected abstract IDepthFirstSearchAlgorithm GetAlgorithm();
        private static DiscoverableDirectedGraphNode[] GetCormenExampleGraph()
        {
            var nodes = new DiscoverableDirectedGraphNode[]
            {
                new DiscoverableDirectedGraphNode(1), //u
                new DiscoverableDirectedGraphNode(2), //v
                new DiscoverableDirectedGraphNode(3), //w
                new DiscoverableDirectedGraphNode(4), //x
                new DiscoverableDirectedGraphNode(5), //y
                new DiscoverableDirectedGraphNode(6), //z
            };
            nodes[0].AddArc(new DiscoverableDirectedGraphArc(1, nodes[1]));
            nodes[0].AddArc(new DiscoverableDirectedGraphArc(2, nodes[3]));
            nodes[3].AddArc(new DiscoverableDirectedGraphArc(3, nodes[1]));
            nodes[1].AddArc(new DiscoverableDirectedGraphArc(4, nodes[4]));
            nodes[4].AddArc(new DiscoverableDirectedGraphArc(5, nodes[3]));
            nodes[2].AddArc(new DiscoverableDirectedGraphArc(6, nodes[4]));
            nodes[2].AddArc(new DiscoverableDirectedGraphArc(7, nodes[5]));
            nodes[5].AddArc(new DiscoverableDirectedGraphArc(8, nodes[5]));
            return nodes;
        }

        [TestMethod]
        public void CanReproduceExample()
        {
            var nodes = GetCormenExampleGraph();
            var searchAlgorithm = GetAlgorithm();
            searchAlgorithm.Run<DiscoverableDirectedGraphNode, DiscoverableDirectedGraphArc>(nodes);
            //node - discovery time - finished time            
            var expected = new[]
            {
                new { l = 1, d = 1, f = 8 },
                new { l = 2, d = 2, f = 7 },
                new { l = 3, d = 9, f = 12},
                new { l = 4, d = 4, f = 5},
                new { l = 5, d = 3, f = 6},
                new { l = 6, d = 10, f = 11},
            };
            var results = nodes
                .Select(n => new { l = n.Id, d = n.DiscoveredTime, f = n.ExploredTime });

            Assert.IsTrue(expected.SequenceEqual(results));
        }

        [TestMethod]
        public void Monkey()
        {
            var algorithm = GetAlgorithm();
            var generator = new DiscoverableDirectedGraphGenerator()
            {
                DirectedGraphGenerator = new UndirectedToDirectedGraphGenerator()
                {
                    UndirectedGraphGenerator = new ErdosRenyiGenerator()
                    {
                        ConnectComponents = false
                    }
                }
            };

            for (int i = 0; i < 100; i++)
            {
                var numberOfNodes = 100 + i * 10;
                var density = 2.5 / (double)numberOfNodes;
                var nodes = generator.Generate(numberOfNodes, density);
                algorithm.Run<DiscoverableDirectedGraphNode, DiscoverableDirectedGraphArc>(nodes);
            }
        }
    }
}