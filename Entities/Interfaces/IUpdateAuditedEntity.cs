namespace Entities.Interfaces
{
    public interface IUpdateAuditedEntity: ICreateAuditedEntity
    {
        DateTime? UpdateDateTime { get; set; }
    }
}
