using DotnetGraph.Model.Graphs.WeightedDirectedGraph;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPath
{
    public interface IShortestPathAlgorithm
    {
        ShortestPathResult GetShortestPath(IList<IWeightedDirectedGraphNode> nodes,
            int originIndex,
            int destinationIndex);
    }
}