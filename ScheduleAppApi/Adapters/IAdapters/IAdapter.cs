using ScheduleAppApi.Domain.Models;
using ScheduleAppApi.DTOs.User;

namespace ScheduleAppApi.Adapters.IAdapters
{
    public interface IAdapter
    {
        LoginResponse Bind(User user);
    }
}
