using Database.Interfaces;
using Database;
using Services.Interfaces;
using Services;

namespace BlackoutSchedule.Server
{
    public static class InjectionProgramExtension
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IIntervalService, IntervalService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IGroupService, GroupService>();
        }

        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IIntervalRepository, IntervalRepository>();
            services.AddScoped<IScheduleRepositrory, ScheduleRepositrory>();
            services.AddScoped<IGroupRepository, GroupRepository>();
        }
    }
}
