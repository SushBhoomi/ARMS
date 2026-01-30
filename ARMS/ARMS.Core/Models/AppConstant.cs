namespace ARMS.Core.Models
{
    public static class AppConstant
    {
        public const string SYSTEM_USERNAME = "System";
        public const string DEFAULT_PERMISSION_CACHE = "user-permission";
        public const int ACCOUNT_NUMBER_LENGTH = 10;

        public const string ROLE_GUEST = "Guest";
        public const string ROLE_PENDING = "Pending";


        public const char TILDE = '~';

    }

    public static class Seperator
    {
        public const char HYPHEN = '-';
        public const char COMMA = ',';
        public const char SEMICOLON = ';';
        public const char UNDERSCORE = '_';
    }

    public static class StatusType
    {
        public const string ACTIVE = "Active";
        public const string INACTIVE = "Inactive";
    }

    public static class DateFormat
    {
        public const string MM_DD_YYYY = "MM/dd/yyyy";
        public const string MM_DD_YYYY_LABEL = "MM/DD/YYYY";
        public const string DD_MM_YYYY = "dd/MM/yyyy";
        public const string YYYY_MM_DD = "yyyy/MM/dd";
    }

    public static class FormModeType
    {
        public const string VIEW = "view";
        public const string EDIT = "edit";
        public const string ADD = "add";
        public const string COPY = "copy";
    }

}
