using ScheduleAppApi.Adapters.IAdapters;
using ScheduleAppApi.Domain.Models;
using ScheduleAppApi.DTOs.User;

namespace ScheduleAppApi.Adapters
{
    public class Adapter : IAdapter
    {
        public LoginResponse Bind(User user) => new() { Role = user.Role.ToString() };

    }
}
