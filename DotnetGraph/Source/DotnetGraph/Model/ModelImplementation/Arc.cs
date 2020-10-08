using System;

namespace DotnetGraph.Model.ModelImplementation
{
    public class Arc : IArc
    {
        public Arc(INode destination)
        {
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        }

        public INode Destination { get; }
    }
}