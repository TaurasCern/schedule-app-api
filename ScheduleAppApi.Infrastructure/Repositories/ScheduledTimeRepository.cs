using ScheduleAppApi.Domain.Models;
using ScheduleAppApi.Infrastructure.Database;
using ScheduleAppApi.Infrastructure.Repositories.IRepositories;

namespace ScheduleAppApi.Infrastructure.Repositories
{
    public class ScheduledTimeRepository : Repository<ScheduledTime>, IScheduledTimeRepository
    {
        private readonly ScheduleContext _context;
        public ScheduledTimeRepository(ScheduleContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
