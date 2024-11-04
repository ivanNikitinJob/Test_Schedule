using Entities;
namespace Services.Interfaces
{
    public interface IIntervalService
    {
        Task<List<Interval>> GetAllAsync();
    }
}
