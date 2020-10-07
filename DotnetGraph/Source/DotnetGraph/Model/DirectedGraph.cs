using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DotnetGraph.Model
{
    public class DirectedGraph<T>
    {
        private readonly Dictionary<T, ReadOnlyCollection<T>> SuccessorsPerNode = new Dictionary<T, ReadOnlyCollection<T>>();
        private readonly Dictionary<T, ReadOnlyCollection<T>> PredecessorsPerNode = new Dictionary<T, ReadOnlyCollection<T>>();
        private readonly Dictionary<T, ReadOnlyCollection<Arc<T>>> OutgoingArcsPerNode = new Dictionary<T, ReadOnlyCollection<Arc<T>>>();
        private readonly Dictionary<T, ReadOnlyCollection<Arc<T>>> IncomingArcsPerNode = new Dictionary<T, ReadOnlyCollection<Arc<T>>>();

        public ReadOnlyCollection<T> Node { get; }
        public ReadOnlyCollection<Arc<T>> Arcs { get; }

        public DirectedGraph(IEnumerable<Arc<T>> arcs)
        {
            arcs ??= Array.Empty<Arc<T>>();
            Arcs = arcs.ToList().AsReadOnly();
        }

        public ReadOnlyCollection<Arc<T>> GetOutgoingArcs(T origin)
        {
            if(OutgoingArcsPerNode == null)
            {

            }
            return OutgoingArcsPerNode[origin];
        }

        public ReadOnlyCollection<Arc<T>> GetIncomingArcs(T destination)
        {
            if(IncomingArcsPerNode == null)
            {

            }
            return IncomingArcsPerNode[destination];
        }

        public ReadOnlyCollection<T> GetSuccessors(T origin)
        {
            if (!SuccessorsPerNode.ContainsKey(origin))
            {
                SuccessorsPerNode[origin] = GetOutgoingArcs(origin)
                    .Select(x => x.Destination)
                    .ToList()
                    .AsReadOnly();
            }
            return SuccessorsPerNode[origin];
        }

        public ReadOnlyCollection<T> GetPredecessors(T destiniation)
        {
            if (!PredecessorsPerNode.ContainsKey(destiniation))
            {
                PredecessorsPerNode[destiniation] = GetIncomingArcs(destiniation)
                    .Select(x => x.Origin)
                    .ToList()
                    .AsReadOnly();
            }
            return PredecessorsPerNode[destiniation];
        }
    }
}