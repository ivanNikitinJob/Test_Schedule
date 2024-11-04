using Database.Interfaces;
using Entities;
using Services.Interfaces;

namespace Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IEnumerable<Group>> GetListAcync()
        {
            return await _groupRepository.GetAllAsync();
        }
    }
}
