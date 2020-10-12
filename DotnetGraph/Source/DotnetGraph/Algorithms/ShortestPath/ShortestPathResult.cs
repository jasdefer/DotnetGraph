using DotnetGraph.Model.Graphs.WeightedDirectedGraph;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPath
{
    public class ShortestPathResult
    {
        public IList<IWeightedDirectedGraphArc> Arcs { get; }
        public double TotalWeight { get; }
    }
}