using Entities;

namespace Database.Interfaces
{
    public interface IScheduleRepositrory
    {
        Task<IEnumerable<Schedule>> GetByAddressAsync(string Adress);
        Task<IEnumerable<Schedule>> GetByGroupAsync(Guid GroupId);
        Task UpdateScheduleByDay(IEnumerable<Schedule> schedule);
    }
}
