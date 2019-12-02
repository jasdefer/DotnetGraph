using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.Contracts
{
    public interface IComponentAlgorithm
    {
        T[][] GetComponents<T>(IEnumerable<Edge<T>> edges);
    }
}