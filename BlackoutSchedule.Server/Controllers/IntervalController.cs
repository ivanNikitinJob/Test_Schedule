using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace BlackoutSchedule.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class IntervalController : Controller
    {
        private readonly IIntervalService _intervalService;

        public IntervalController(IIntervalService intervalService)
        {
            _intervalService = intervalService;
        }

        [HttpGet]
        public async Task<IEnumerable<DayIntervalModel>> GetIntervals()
        {
            var result = new List<DayIntervalModel>();
            var intervals = await _intervalService.GetAllAsync();

            foreach (var day in Enum.GetNames(typeof(DayOfWeek)))
            {
                var newIntervalsList = new List<IntervalModel>();
                foreach (var interval in intervals)
                {
                    newIntervalsList.Add(new IntervalModel() 
                    {
                        StartTime = interval.StartTime,
                        EndTime = interval.EndTime
                    });
                }

                var newDay = new DayIntervalModel()
                {
                    Day = day,
                    DayId = ((int)Enum.Parse<DayOfWeek>(day)),
                    Intervals = newIntervalsList
                };

                result.Add(newDay);
            }

            return result;
        }
    }
}
