using Entities.Interfaces;

namespace Entities.BaseEntities
{
    public class Entity: IEntity
    {
        public Guid Id { get; set; }
    }
}
