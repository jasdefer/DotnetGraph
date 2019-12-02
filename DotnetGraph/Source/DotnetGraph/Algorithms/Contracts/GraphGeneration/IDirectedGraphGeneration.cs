using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.Contracts.GraphGeneration
{
    public interface IDirectedGraphGeneration
    {
        public Arc<T>[] GenerateGraph<T>(IEnumerable<T> nodes);
    }
}