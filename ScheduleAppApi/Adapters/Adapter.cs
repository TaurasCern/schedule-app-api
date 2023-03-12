using ScheduleAppApi.Adapters.IAdapters;
using ScheduleAppApi.Domain.Models;
using ScheduleAppApi.DTOs.ScheduledTime;
using ScheduleAppApi.DTOs.User;

namespace ScheduleAppApi.Adapters
{
    public class Adapter : IAdapter
    {
        public LoginResponse Bind(User user, string token) 
            => new() { Role = user.Role.ToString(), Token = token };
        public ScheduledTimeResponse[] Bind(List<ScheduledTime> scheduledTimes) 
            => scheduledTimes == null ? Array.Empty<ScheduledTimeResponse>() :
            scheduledTimes.Select(Bind).ToArray();
        public ScheduledTimeResponse Bind(ScheduledTime st)
            => new() 
            {
                Id = st.Id,
                From = st.From,
                To = st.To,
                Note = st.Note,
            };

        public ScheduledTime Bind(ScheduledTimeRequest request, int userId)
            => new()
            {
                From = request.From,
                To = request.To,
                Note = request.Note,
                UserId = userId
            };
    }
}
