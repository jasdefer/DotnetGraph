using System;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.Components.StronglyConnectedComponents
{
    public class StronglyConnectedComponentsResult<TNode>
    {
        public StronglyConnectedComponentsResult(IReadOnlyList<IReadOnlyCollection<TNode>> components)
        {
            Components = components ?? throw new ArgumentNullException(nameof(components));
        }

        public IReadOnlyList<IReadOnlyCollection<TNode>> Components { get; }
        public int NumberOfComponents => Components.Count;
    }
}