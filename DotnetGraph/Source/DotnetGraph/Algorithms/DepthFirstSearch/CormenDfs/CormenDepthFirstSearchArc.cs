using DotnetGraph.Model.Properties;
using System;

namespace DotnetGraph.Algorithms.DepthFirstSearch.CormenDfs
{
    public class CormenDepthFirstSearchArc :
        IHasDestination<CormenDepthFirstSearchNode>,
        IHasId
    {
        public CormenDepthFirstSearchArc(int id, CormenDepthFirstSearchNode origin, CormenDepthFirstSearchNode destination)
        {
            Id = id;
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        }
        public CormenDepthFirstSearchNode Origin { get; }
        public CormenDepthFirstSearchNode Destination { get; }
        public int Id { get; }
    }
}