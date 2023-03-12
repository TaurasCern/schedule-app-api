using ScheduleAppApi.Domain.Models;
using ScheduleAppApi.DTOs.ScheduledTime;
using ScheduleAppApi.DTOs.User;

namespace ScheduleAppApi.Adapters.IAdapters
{
    public interface IAdapter
    {
        LoginResponse Bind(User user, string token);
        ScheduledTimeResponse[] Bind(List<ScheduledTime> scheduledTimes);
        ScheduledTimeResponse Bind(ScheduledTime scheduledTime);
        ScheduledTime Bind(ScheduledTimeRequest request, int userId);
    }
}
