using System.Collections.Generic;

namespace DotnetGraph.Model.Properties
{
    public interface IHasIncomingArcs<out TArc>
    {
        IReadOnlyCollection<TArc> IncomingArcs { get; }
    }
}