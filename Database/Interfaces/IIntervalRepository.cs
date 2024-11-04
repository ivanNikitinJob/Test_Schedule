using Entities;

namespace Database.Interfaces
{
    public interface IIntervalRepository
    {
        Task<List<Interval>> GetAllAsync();
    }
}
