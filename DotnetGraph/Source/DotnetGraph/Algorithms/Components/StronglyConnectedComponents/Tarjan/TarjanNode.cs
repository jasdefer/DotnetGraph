using DotnetGraph.Model.Implementations;
using DotnetGraph.Model.Properties;

namespace DotnetGraph.Algorithms.Components.StronglyConnectedComponents.Tarjan
{
    public class TarjanNode : IdNode<Arc<TarjanNode>>,
        IHasOutgoingArcs<Arc<TarjanNode>>,
        IHasId
    {
        public TarjanNode(int id) : base(id)
        {
        }

        public int? Index { get; set; }
        public int LowLink { get; set; }
        public bool IsOnStack { get; set; }
        public int Component { get; set; }
    }
}