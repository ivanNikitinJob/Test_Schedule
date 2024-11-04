using Entities.Interfaces;

namespace Entities.BaseEntities
{
    public class CreateAuditedEntity: Entity, ICreateAuditedEntity
    {
        public DateTime? CreateDateTime { get; set; }

        public CreateAuditedEntity()
        {
            CreateDateTime = DateTime.UtcNow;
        }
    }
}
