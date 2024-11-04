using Entities.BaseEntities;

namespace Entities
{
    public class Address: DeleteAuditedEntity
    {
        public string? Street { get; set; }
        public Guid GroupId { get; set; }
        public virtual Group? Group { get; set; }
    }
}
