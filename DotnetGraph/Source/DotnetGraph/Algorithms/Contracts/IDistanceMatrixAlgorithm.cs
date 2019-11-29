using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.Contracts
{
    public interface IDistanceMatrixAlgorithm
    {
        DistanceMatrix<T> GetDistanceMatrix<T>(IEnumerable<Arc<T>> arcs);
    }
}