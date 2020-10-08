using System;
using System.Collections.Generic;

namespace DotnetGraph.Model.ModelImplementation
{
    public class Node<T> : INode<T> where T : IArc
    {
        public Node(string label)
        {
            OutgoingArcs = new List<T>();
            Label = label ?? throw new ArgumentNullException(nameof(label));
        }

        public Node(string label, IList<T> outgoingArcs)
        {
            OutgoingArcs = outgoingArcs ?? throw new ArgumentNullException(nameof(outgoingArcs));
            Label = label ?? throw new ArgumentNullException(nameof(label));
        }

        public IList<T> OutgoingArcs { get; }

        public string Label { get; }

        public void AddArc(T arc)
        {
            if (!OutgoingArcs.Contains(arc))
            {
                OutgoingArcs.Add(arc);
            }
        }

        public void RemoveArc(T arc)
        {
            OutgoingArcs.Remove(arc);
        }
    }
}