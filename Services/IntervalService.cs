using Database.Interfaces;
using Entities;
using Services.Interfaces;

namespace Services
{
    public class IntervalService: IIntervalService
    {
        private readonly IIntervalRepository _intervalRepository;
        public IntervalService(IIntervalRepository intervalRepository)
        {
            _intervalRepository = intervalRepository;
        }

        public async Task<List<Interval>> GetAllAsync()
        {
            return await _intervalRepository.GetAllAsync();
        }
    }
}
