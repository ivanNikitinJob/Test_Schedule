using Entities;

namespace Database.Interfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetAllAsync();
        Task<Group> GetAsync(Guid groupId);
        Task<Group> GetGroupIdByNumberAsync(int groupNumber);
    }
}
