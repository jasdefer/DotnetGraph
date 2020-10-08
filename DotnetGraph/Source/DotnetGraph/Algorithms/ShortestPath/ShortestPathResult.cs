using DotnetGraph.Model;
using System;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPath
{
    public class ShortestPathResult
    {
        public ShortestPathResult(IReadOnlyCollection<IWeightedArc> arcs,
            double totalWeight)
        {
            Arcs = arcs ?? throw new ArgumentNullException(nameof(arcs));
            TotalWeight = totalWeight;
        }

        IReadOnlyCollection<IWeightedArc> Arcs { get; }
        double TotalWeight { get; }
    }
}