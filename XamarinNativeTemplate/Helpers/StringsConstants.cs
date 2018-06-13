namespace XamarinNativeTemplate
{
    public static class StringsConstants
    {
        public const string EMPLOYEE_LIST_PAGETITLE = "Employees List";

        public static readonly string API_EMPLOYEE = "/api/allemployees";

        public static readonly string EMPLOYEE_JSON_FILE = "XamarinNativeTemplate.Data.employee.json";

        public const string OK = "Ok";
        public const string TRY_AGAIN = "Try again";
        public const string CANCEL = "Cancel";
        public const string SUCCESS = "Success";
        public const string FAILURE = "Failure";
        public const string DOWNLOAD_ERROR = "Download Error";
        public const string CONNECTIVITY_ERROR = "Connectivity Error";
        public const string NO_INTERNET_CONNECTION = "No internet connection, please connect your phone to internet";
        public const string DOWNLOAD_EMPLOYEE_FAIL = "Failed to download employees data. Please try again after some time";

        public static readonly string DAY_MONTH_YEAR_FORMAT = "dd MMM yyyy";

        #region Sort Options
        public const string SORT_ACTION_SHEET_TITLE = "Sort By";
        public const string SORT_OPTION_NAME_A_Z = "Name: A - Z";
        public const string SORT_OPTION_NAME_Z_A = "Name: Z - A";
        public const string SORT_OPTION_EMAIL_A_Z = "Email to: A - Z";
        public const string SORT_OPTION_EMAIL_Z_A = "Email to: Z - A";
        public const string SORT_OPTION_DOB_YOUNGER_OLDER = "Date of Birth: Younger to Older";
        public const string SORT_OPTION_DOB_OLDER_YOUNGER = "Date of Birth: Older to Younger";
        #endregion

        public static string USER_SELECTED_SORT_TYPE;
    }
}