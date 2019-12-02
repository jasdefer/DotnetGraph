using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.Contracts.GraphGeneration
{
    public interface IUndirectedGraphGeneration
    {
        public Edge<T>[] GenerateGraph<T>(IEnumerable<T> nodes);
    }
}