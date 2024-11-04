using Database.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class ScheduleRepositrory : IScheduleRepositrory
    {
        private readonly EFContext _context;

        public ScheduleRepositrory(EFContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Schedule>> GetByAddressAsync(string inputAdress)
        {

            return await _context.Groups
                .Join(_context.Addresses, group => group.Id, adress => adress.GroupId, (group, address) => address)
                .Where(x => x.Street == inputAdress)
                .Join(_context.Schedules, adress => adress.GroupId, schedule => schedule.GroupId, (adress, schedule) => schedule)
                .ToListAsync();
        }

        public async Task<IEnumerable<Schedule>> GetByGroupAsync(Guid GroupId)
        {
            return await _context.Schedules.Where(x => x.GroupId == GroupId).ToListAsync();
        }

        public async Task UpdateScheduleByDay(IEnumerable<Schedule> scheduleList)
        {
            var groupId = scheduleList.FirstOrDefault()?.GroupId;
            var day = scheduleList.FirstOrDefault()?.Day;

            var schedulesToRemove = await _context.Schedules.Where(x => x.Day == day && x.GroupId == groupId).ToListAsync();

            foreach (var scheduleToRemove in schedulesToRemove)
            {
                _context.Schedules.Remove(scheduleToRemove);
            };

            _context.Schedules.AddRange(scheduleList);
            await _context.SaveChangesAsync();
        }
    }
}
