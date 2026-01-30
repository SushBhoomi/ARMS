namespace ARMS.Application.Exceptions
{
    public class AppEntityNotFoundException : AppException
    {
        public AppEntityNotFoundException(Exception ex, string message, params object[] placeholders)
            : base(ex, message, placeholders)
        {
        }

        public AppEntityNotFoundException(string message, params object[] placeholders)
            : base(message, placeholders)
        {
        }
    }
}
