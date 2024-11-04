using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace BlackoutSchedule.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost("FindByAddres")]
        public async Task<IEnumerable<ScheduleModel>> FindByAddress(FindByAddressRequestModel model)
        {
            try
            {
                var result = (await _scheduleService.GetByAddressAsync(model.Address)).GroupBy(x => x.Day);
                var response = new List<ScheduleModel>();

                foreach (var item in result)
                {
                    var intervals = new List<ScheduleIntervalModel>();

                    foreach (var scheduleInterval in item)
                    {
                        var newInterval = new ScheduleIntervalModel()
                        {
                            StartTime = scheduleInterval.StartTime.ToString(),
                            EndTime = scheduleInterval.EndTime.ToString(),
                            IsActive = true
                        };

                        intervals.Add(newInterval);
                    }


                    var dayString = item.Key.ToString();
                    var scheduleModel = new ScheduleModel()
                    {
                        Day = item.Key.ToString(),
                        DayId = ((int)Enum.Parse<DayOfWeek>(dayString)),
                        GroupId = item.FirstOrDefault()?.GroupId,
                        Intervals = intervals
                    };

                    response.Add(scheduleModel);
                }

                return response;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("FindByGroup")]
        public async Task<IEnumerable<ScheduleModel>> FindByGroup(FindByGroupRequestModel model)
        {
            try
            {
                var result = (await _scheduleService.GetByGroupAsync(Guid.Parse(model.GroupId))).GroupBy(x => x.Day);
                var response = new List<ScheduleModel>();

                foreach (var item in result)
                {
                    var intervals = new List<ScheduleIntervalModel>();

                    foreach (var scheduleInterval in item)
                    {
                        var newInterval = new ScheduleIntervalModel()
                        {
                            StartTime = scheduleInterval.StartTime.ToString("HH:mm:ss"),
                            EndTime = scheduleInterval.EndTime.ToString("HH:mm:ss"),
                            IsActive = true
                        };

                        intervals.Add(newInterval);
                    }

                    var dayString = item.Key.ToString();
                    var scheduleModel = new ScheduleModel()
                    {
                        Day = dayString,
                        DayId = ((int)Enum.Parse<DayOfWeek>(dayString)),
                        Intervals = intervals
                    };

                    response.Add(scheduleModel);
                }

                return response;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("SaveSchedule")]
        public async Task SaveSchedule(SaveScheduleRequestModel model)
        {
            try
            {
                var scheduleList = new List<Schedule>();
                foreach (var scheduleModel in model.ScheduleModelsList)
                {
                    var day = scheduleModel.Day;
                    foreach (var interval in scheduleModel.Intervals)
                    {
                        if (!interval.IsActive)
                        {
                            continue;
                        }

                        var newSchedule = new Schedule()
                        {
                            Day = Enum.Parse<DayOfWeek>(day),
                            StartTime = TimeOnly.Parse(interval.StartTime),
                            EndTime = TimeOnly.Parse(interval.EndTime),
                            GroupId = model.GroupId
                        };

                        scheduleList.Add(newSchedule);
                    }
                }

                await _scheduleService.CreateOrUpdate(scheduleList);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("SaveScheduleFromFile")]
        public async Task<SaveScheduleFromFileResponseModel> SaveScheduleFromFile(SaveScheduleFromFileRequestModel model)
        {
            var response = new SaveScheduleFromFileResponseModel();
            try
            {
                await _scheduleService.TrySaveFileData(model);
            }
            catch (InvalidOperationException ex)
            {
                response.Error = ex.Message;
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
