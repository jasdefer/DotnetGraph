using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Implementations
{
    public class Entity : IHasId
    {
        public Entity(int id)
        {
            Id = id;
        }
        public int Id { get; }

        public override string ToString()
        {
            return $"Id {Id}";
        }
    }
}