namespace ARMS.Core.Models
{
    public class AppConfig
    {
        public class AppDbConnections
        {
            public string DefaultConnection { get; set; }

        }

        public enum AppDbConnectionName
        {
            DefaultConnection
        }
    }
}
