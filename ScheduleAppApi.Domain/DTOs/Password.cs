namespace ScheduleAppApi.Domain.DTOs
{
    public class Password
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}