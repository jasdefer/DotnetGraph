using System;
using System.Collections.ObjectModel;

namespace DotnetGraph.Algorithms.Components.StronglyConnectedComponents
{
    public class StronglyConnectedComponentsResult<TNode>
    {
        public StronglyConnectedComponentsResult(ReadOnlyCollection<ReadOnlyCollection<TNode>> components)
        {
            Components = components ?? throw new ArgumentNullException(nameof(components));
        }

        public ReadOnlyCollection<ReadOnlyCollection<TNode>> Components { get; }
        public int NumberOfComponents => Components.Count;
    }
}