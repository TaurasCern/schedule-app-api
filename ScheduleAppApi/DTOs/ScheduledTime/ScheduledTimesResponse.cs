namespace ScheduleAppApi.DTOs.ScheduledTime
{
    public class ScheduledTimeResponse
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Note { get; set; }
    }
}
