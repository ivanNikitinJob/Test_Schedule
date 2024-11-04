using Entities.Interfaces;

namespace Entities.BaseEntities
{
    public class UpdateAuditedEntity : CreateAuditedEntity, IUpdateAuditedEntity
    {
        public DateTime? UpdateDateTime { get; set; }
    }
}
