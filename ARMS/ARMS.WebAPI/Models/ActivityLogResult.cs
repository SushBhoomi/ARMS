namespace ARMS.WebAPI.Models
{
    public class ActivityLogResult
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Activity { get; set; }
        public DateTime Timestamp { get; set; }
        public string IpAddress { get; set; }
        public string Message { get; set; }
    }
}
