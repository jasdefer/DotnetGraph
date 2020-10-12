using System.Collections.Generic;

namespace DotnetGraph.Model.Properties
{
    public interface IHasOutgoingArcs<TArc>
    {
        IList<TArc> OutgoingArcs { get; }
    }
}