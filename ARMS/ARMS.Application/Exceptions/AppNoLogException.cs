namespace ARMS.Application.Exceptions
{
    public class AppNoLogException : AppException
    {
        public object AdditionalData { get; set; }

        public AppNoLogException(Exception ex, string message, params object[] placeholders)
            : base(GetMessage(message, placeholders), ex)
        {
        }

        public AppNoLogException(string message, params object[] placeholders)
            : base(GetMessage(message, placeholders))
        {
        }

        private static string GetMessage(string message, object[] placeholders)
        {
            if (placeholders == null || placeholders.Length == 0)
            {
                return message;
            }

            return string.Format(message, placeholders);
        }
    }
}
