using DotnetGraph.Model.Properties;
using System;
using System.Collections.ObjectModel;

namespace DotnetGraph.Algorithms.ShortestPath
{
    public class ShortestPathResult<TArc>
        where TArc : IHasId
    {
        public ShortestPathResult(ReadOnlyCollection<TArc> arcs, double totalWeight)
        {
            Arcs = arcs ?? throw new ArgumentNullException(nameof(arcs));
            TotalWeight = totalWeight;
        }

        public ReadOnlyCollection<TArc> Arcs { get; }
        public double TotalWeight { get; }
    }
}