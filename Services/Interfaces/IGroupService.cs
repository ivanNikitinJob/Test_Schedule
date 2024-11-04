using Entities;

namespace Services.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetListAcync();
    }
}
