using DotnetGraph.Model;
using System;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.GraphGeneration
{
    public interface IGraphGeneration<T> where T : IArc
    {
        Random Random { get; set; }
        List<INode<T>> Create(int numberOfNodes, int numberOfEdges);
    }
}