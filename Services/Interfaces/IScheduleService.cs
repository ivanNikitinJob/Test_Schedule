using Entities;
using Models;

namespace Services.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetByAddressAsync(string Adress);
        Task<IEnumerable<Schedule>> GetByGroupAsync(Guid GroupId);
        Task CreateOrUpdate(IEnumerable<Schedule> scheduleList);
        Task TrySaveFileData(SaveScheduleFromFileRequestModel schedule);
    }
}
