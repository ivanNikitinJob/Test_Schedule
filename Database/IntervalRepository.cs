using Database.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class IntervalRepository : IIntervalRepository
    {
        private readonly EFContext _context;

        public IntervalRepository(EFContext context)
        {
            _context = context;
        }

        public async Task<List<Interval>> GetAllAsync()
        {
            try
            {
                return await _context.Intervals.OrderBy(x => x.StartTime).ToListAsync();
            }
            catch { throw; }
        }
    }
}
