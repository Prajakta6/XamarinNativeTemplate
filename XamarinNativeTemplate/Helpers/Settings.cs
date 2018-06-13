// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace XamarinNativeTemplate
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		const string SettingsKey = "settings_key";
		static readonly string SettingsDefault = string.Empty;

        const string AppVersionKey = "appVersion_key";
        static readonly string AppVersionDefault = string.Empty;

        const string AppEnvironmentsKey = "appEnvironments_key";
        static readonly EnvironmentType AppEnvironmentsDefault = EnvironmentType.DEVELOPMENT;
             
        #endregion

        public static string GeneralSettings
		{
            get => AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);

            set => AppSettings.AddOrUpdateValue(SettingsKey, value);
        }

        public static string AppVersion
        {
            get => AppSettings.GetValueOrDefault(AppVersionKey, AppVersionDefault);
           
            set => AppSettings.AddOrUpdateValue(AppVersionKey, value);
        }

        //public static EnvironmentType AppEnvironments
        //{
        //    get => AppSettings.GetValueOrDefault(AppEnvironmentsKey, AppEnvironmentsDefault);

        //    set => AppSettings.AddOrUpdateValue(AppEnvironmentsKey, value);
        //}
     
      
    }
}