namespace ScheduleAppApi.DTOs.ScheduledTime
{
    public class ScheduledTimeRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Note { get; set; }
    }
}
