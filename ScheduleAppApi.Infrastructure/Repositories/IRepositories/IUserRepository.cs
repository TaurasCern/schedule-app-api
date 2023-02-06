using ScheduleAppApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAppApi.Infrastructure.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<KeyValuePair<bool, User?>> TryLoginAsync(string email, string password);
        Task<int> RegisterAsync(User user);
        Task<bool> IsRegisteredAsync(string email);
    }
}
