using System.Threading.Tasks;

namespace XamarinNativeTemplate
{
    public class DownloadManager
    {
        readonly NetworkServices applicationNetworkServices;

        DownloadManager() 
        {
            applicationNetworkServices = new NetworkServices();
        }

        static DownloadManager _downloadManager;

        public static DownloadManager DownloadManagerInstance
        {
            get
            {
                if (_downloadManager == null)
                {
                    _downloadManager = new DownloadManager();
                }

                return _downloadManager;
            }
        }

        public async Task<bool> DownloadAllEmployeesData()
        {
            var response = await applicationNetworkServices.GetEmployees();

            return response;
        }
    }
}