using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAppApi.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ERole Role { get; set; }

        public ICollection<ScheduledTime> ScheduledTimes { get; set; } = new HashSet<ScheduledTime>();
    }
}
