namespace XamarinNativeTemplate
{
    public class XamarinNativeTemplate
    {
        XamarinNativeTemplate() { }

        static XamarinNativeTemplate _instance;

        public static XamarinNativeTemplate Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new XamarinNativeTemplate();
                }
                return _instance;
            }
        }

        static ViewModelLocator locator;

        public static ViewModelLocator Locator
        {
            get
            {
                if (locator == null)
                {
                    locator = new ViewModelLocator();
                }

                return locator;
            }
        }

        internal class AdditionalDetails
        {
        }
    }
}