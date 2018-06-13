using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json;

namespace XamarinNativeTemplate
{
    public class NetworkServices
    {
        public static string BASE_URL = "http://jsonplaceholder.typicode.com";

        static readonly HttpClient client = new HttpClient(new NativeMessageHandler
        { DisableCaching = true })
        { Timeout = TimeSpan.FromSeconds(60) };

        readonly JsonSerializer _serializer = new JsonSerializer();

        public async Task<bool> GetEmployees()
        {
            string url = Uri.EscapeUriString(BASE_URL + StringsConstants.API_EMPLOYEE);

            Debug.WriteLine(url); // this line added just to check web service url

            var stopWatch = new Stopwatch();

            try
            {
                stopWatch.Start();

                var response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var dataStream = await response.Content.ReadAsStreamAsync();

                stopWatch.Stop();

                using (var dataReader = new StreamReader(dataStream))
                {
                    using (var jsonData = new JsonTextReader(dataReader))
                    {
                        XamarinNativeTemplate.Locator.MainVm.AllEmployees = _serializer.Deserialize<ObservableCollection<Employee>>(jsonData);

                        return true;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(ex.Message);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
