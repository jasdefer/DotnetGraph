using System.Collections.Generic;

namespace DotnetGraph.Model
{
    public interface INode
    {
        public string Label { get; }
    }

    public interface INode<T> : INode where T : IArc
    {
        IList<T> OutgoingArcs { get; }
    }
}