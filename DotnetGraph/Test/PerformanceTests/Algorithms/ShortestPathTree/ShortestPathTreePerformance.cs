using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration;
using DotnetGraph.Algorithms.ShortestPathTree.Fifo;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;

namespace PerformanceTests.Algorithms.ShortestPathTree
{
    public class ShortestPathTreePerformance
    {
        private WeightedDirectedGraphNode[] baseNodes;
        private FifoNode[] fifoNodes;
        private int originNodeId;

        [GlobalSetup]
        public void Setup()
        {
            var generator = new WeightedDirectedGraphGenerator();
            var weightGenerator = new UniformNumberGenerator();
            baseNodes = generator.Generate(50000, 0.0000403, weightGenerator);
            originNodeId = 1;
            fifoNodes = FifoAlgorithm.Convert<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(baseNodes);
        }

        [Benchmark]
        public int FifoRaw()
        {
            var shortestPathResult = FifoAlgorithm.GetShortestPathTree(fifoNodes, originNodeId);
            return shortestPathResult.ShortestPaths.Count;
        }

        [Benchmark]
        public int FifoWithConversion()
        {
            var algorithm = new FifoAlgorithm();
            var shortestPathResult = algorithm.GetShortestPathTree<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(baseNodes, originNodeId);
            return shortestPathResult.ShortestPaths.Count;
        }
    }
}