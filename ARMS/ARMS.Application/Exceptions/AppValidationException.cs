namespace ARMS.Application.Exceptions
{
    public class AppValidationException : AppException
    {
        public AppValidationException(Exception ex, string message, params object[] placeholders)
            : base(ex, message, placeholders)
        {
        }

        public AppValidationException(string message, params object[] placeholders)
            : base(message, placeholders)
        {
        }
    }
}
