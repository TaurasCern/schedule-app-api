
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAppApi.Domain.Models
{
    public class ScheduledTime
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Note { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }
    }
}
