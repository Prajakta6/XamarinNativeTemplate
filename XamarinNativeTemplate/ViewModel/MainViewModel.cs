using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Acr.UserDialogs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using Plugin.Connectivity;
using PropertyChanged;

namespace XamarinNativeTemplate
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel : ViewModelBase
    {
        INavigationService navigationService { get => ServiceLocator.Current.GetInstance<INavigationService>(); }

        public ObservableCollection<Employee> AllEmployees { get; set; } = new ObservableCollection<Employee>();

        public int CurrentSorting { get; set; } = 0;

        public void BuildEmployeeList()
        {
            Stream dataStream;

            string employeeList = string.Empty;

            var assembly = typeof(MainViewModel).GetTypeInfo().Assembly;

            dataStream = assembly.GetManifestResourceStream(StringsConstants.EMPLOYEE_JSON_FILE);

            using (var reader = new StreamReader(dataStream))
            {
                employeeList = reader.ReadToEnd();
            }

            XamarinNativeTemplate.Locator.MainVm.AllEmployees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(employeeList);
        }

        public async Task<bool> DownloadAllEmployeesData()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                UserDialogs.Instance.ShowLoading("");

                if (await DownloadManager.DownloadManagerInstance.DownloadAllEmployeesData())
                {
                    UserDialogs.Instance.HideLoading();

                    return true;
                }

                UserDialogs.Instance.HideLoading();

                //Show error alert to the user 
                var tryAgain = await UserDialogs.Instance.ConfirmAsync(StringsConstants.DOWNLOAD_EMPLOYEE_FAIL,
                                                                       StringsConstants.DOWNLOAD_ERROR,
                                                                       StringsConstants.TRY_AGAIN,
                                                                       StringsConstants.CANCEL, null);

                if (tryAgain)
                {
                    await DownloadAllEmployeesData();
                }
            }
            else
            {
                UserDialogs.Instance.HideLoading();

                //Show no connectivity alert to the user 
                await UserDialogs.Instance.AlertAsync(StringsConstants.NO_INTERNET_CONNECTION,
                                                      StringsConstants.CONNECTIVITY_ERROR,
                                                      StringsConstants.OK, null);
            }

            return false;
        }

        #region Methods for Sort Options

        // Get all the sort options
        string[] GetSortOptions()
        {
            string[] lstrSortOptions = new string[6];

            lstrSortOptions[0] = StringsConstants.SORT_OPTION_NAME_A_Z;
            lstrSortOptions[1] = StringsConstants.SORT_OPTION_NAME_Z_A;
            lstrSortOptions[2] = StringsConstants.SORT_OPTION_EMAIL_A_Z;
            lstrSortOptions[3] = StringsConstants.SORT_OPTION_EMAIL_Z_A;
            lstrSortOptions[4] = StringsConstants.SORT_OPTION_DOB_OLDER_YOUNGER;
            lstrSortOptions[5] = StringsConstants.SORT_OPTION_DOB_YOUNGER_OLDER;

            return lstrSortOptions;
        }

        // Get the seleted sort option
        string[] GetSortOrderLabel()
        {
            string[] lstrSortOption = GetSortOptions();
            lstrSortOption[CurrentSorting] += "   ✓";

            return lstrSortOption;
        }

        // Applying the selected sort option on Brief/Debrief list
        void ApplySortingOnEmployeeList(string sortType)
        {
            if (sortType.Contains("   ✓"))
            {
                sortType = sortType.Remove(sortType.Length - "   ✓".Length);
            }

            List<Employee> empLocalList = new List<Employee>();

            switch (sortType)
            {
                case StringsConstants.SORT_OPTION_NAME_A_Z:
                    empLocalList = AllEmployees.OrderBy(n => n.Name).ToList();
                    break;
                case StringsConstants.SORT_OPTION_NAME_Z_A:
                    empLocalList = AllEmployees.OrderByDescending(n => n.Name).ToList();
                    break;
                case StringsConstants.SORT_OPTION_EMAIL_A_Z:
                    empLocalList = AllEmployees.OrderBy(e => e.EmailId).ToList();
                    break;
                case StringsConstants.SORT_OPTION_EMAIL_Z_A:
                    empLocalList = AllEmployees.OrderByDescending(e => e.EmailId).ToList();
                    break;
                case StringsConstants.SORT_OPTION_DOB_OLDER_YOUNGER:
                   empLocalList = AllEmployees.OrderByDescending(d => DateTime.Parse(d.EmployeeDOB)).ToList();
                    break;
                case StringsConstants.SORT_OPTION_DOB_YOUNGER_OLDER:
                    empLocalList = AllEmployees.OrderBy(d => DateTime.Parse(d.EmployeeDOB)).ToList();
                    break;
            }

            StringsConstants.USER_SELECTED_SORT_TYPE = sortType;

            AllEmployees.Clear();

            foreach (var emp in empLocalList)
            {
                AllEmployees.Add(emp);
            }

            var options = new List<string>(GetSortOrderLabel());

            var stringToMatch = options.FirstOrDefault(s => s.Contains(sortType));

            CurrentSorting = options.IndexOf(stringToMatch);
        }

        #endregion

        RelayCommand _sortActionCommand;
        public RelayCommand SortActionCommand
        {
            get
            {
                return _sortActionCommand ?? (_sortActionCommand = new RelayCommand(() =>
                                                {
                                                    var sortDialogView = new ActionSheetConfig
                                                    {
                                                        Title = StringsConstants.SORT_ACTION_SHEET_TITLE
                                                    };

                                                    sortDialogView.SetCancel(StringsConstants.CANCEL, null);

                                                    foreach (var sortType in GetSortOrderLabel())
                                                    {
                                                        sortDialogView.Add(sortType, () => ApplySortingOnEmployeeList(sortType));
                                                    }

                                                    UserDialogs.Instance.ActionSheet(sortDialogView);

                                                }));
            }
        }

    }
}