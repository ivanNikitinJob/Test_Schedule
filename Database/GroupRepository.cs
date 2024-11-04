using Database.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class GroupRepository : IGroupRepository
    {
        private readonly EFContext _context;
        public GroupRepository(EFContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _context.Groups.OrderBy(x => x.CreateDateTime).ToListAsync();
        }

        public async Task<Group> GetAsync(Guid groupId)
        {
            return await _context.Groups.FirstOrDefaultAsync(x => x.Id == groupId);
        }

        public async Task<Group> GetGroupIdByNumberAsync(int groupNumber)
        {
            return await _context.Groups.FirstOrDefaultAsync(x=>x.Number == groupNumber);
        }
    }
}
