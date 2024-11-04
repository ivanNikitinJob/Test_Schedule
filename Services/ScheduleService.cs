using Database.Interfaces;
using Entities;
using Models;
using Services.Interfaces;
using System.Text.RegularExpressions;

namespace Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IScheduleRepositrory _scheduleRepositrory;

        public ScheduleService(IGroupRepository groupRepository, IScheduleRepositrory scheduleRepositrory)
        {
            _groupRepository = groupRepository;
            _scheduleRepositrory = scheduleRepositrory;
        }

        public async Task<IEnumerable<Schedule>> GetByAddressAsync(string Adress)
        {
            return await _scheduleRepositrory.GetByAddressAsync(Adress);
        }

        public async Task<IEnumerable<Schedule>> GetByGroupAsync(Guid GroupId)
        {
            return await _scheduleRepositrory.GetByGroupAsync(GroupId);
        }

        public async Task CreateOrUpdate(IEnumerable<Schedule> scheduleList)
        {
            var dayGroupedSchedule = scheduleList.GroupBy(comparer => comparer.Day);
            foreach (var daySchedule in dayGroupedSchedule)
            {
                await _scheduleRepositrory.UpdateScheduleByDay(daySchedule);
            }
        }

        public async Task TrySaveFileData(SaveScheduleFromFileRequestModel fileData)
        {
            try
            {
                var scheduleList = new List<Schedule>();
                foreach (var modelItem in fileData.Items)
                {
                    var group = await _groupRepository.GetGroupIdByNumberAsync(modelItem.GroupNumber);
                    if (group != null)
                    {
                        foreach (var interval in modelItem.Intervals)
                        {
                            var newScheduleItem = new Schedule();
                            newScheduleItem.CreateDateTime = DateTime.UtcNow;
                            newScheduleItem.Day = (DayOfWeek)modelItem.DayNumber;
                            newScheduleItem.GroupId = group.Id;

                            newScheduleItem.StartTime = TimeOnly.Parse(interval.StartTime);
                            newScheduleItem.EndTime = TimeOnly.Parse(interval.EndTime);

                            scheduleList.Add(newScheduleItem);
                        }
                    }

                    if (group == null)
                    {
                        throw new InvalidOperationException($"Group number {modelItem.GroupNumber} is missing");
                    }
                }

                var dayGroupedSchedule = scheduleList.GroupBy(comparer => comparer.Day);
                foreach (var daySchedule in dayGroupedSchedule)
                {
                    await _scheduleRepositrory.UpdateScheduleByDay(daySchedule);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
