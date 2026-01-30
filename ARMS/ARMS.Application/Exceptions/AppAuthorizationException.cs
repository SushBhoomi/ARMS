namespace ARMS.Application.Exceptions
{
    public class AppAuthorizationException : AppException
    {
        public AppAuthorizationException(Exception ex, string message, params object[] placeholders)
            : base(ex, message, placeholders)
        {
        }

        public AppAuthorizationException(string message, params object[] placeholders)
            : base(message, placeholders)
        {
        }
    }
}
