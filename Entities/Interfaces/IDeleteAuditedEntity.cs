namespace Entities.Interfaces
{
    public interface IDeleteAuditedEntity: IUpdateAuditedEntity
    {
        DateTime? DeleteDateTime { get; set; }
        bool? IsDeleted { get; set; }
    }
}
