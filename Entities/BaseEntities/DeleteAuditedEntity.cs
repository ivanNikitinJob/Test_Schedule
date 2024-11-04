using Entities.Interfaces;

namespace Entities.BaseEntities
{
    public class DeleteAuditedEntity : UpdateAuditedEntity, IDeleteAuditedEntity
    {
        public DateTime? DeleteDateTime { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
