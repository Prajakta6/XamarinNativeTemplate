using System;
using Newtonsoft.Json;
using PropertyChanged;
namespace XamarinNativeTemplate
{
    [AddINotifyPropertyChangedInterface]
    public class Employee
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("EmailId")]
        public string EmailId { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("DOB")]
        public string DOB { get; set; }

        [JsonIgnore]
        public string EmployeeDOB
        {
           get
            {
                if (!string.IsNullOrEmpty(DOB))
                {
                    try
                    {
                        var employeeDOB = Convert.ToDateTime(DOB).ToString(StringsConstants.DAY_MONTH_YEAR_FORMAT);

                        if (!string.IsNullOrEmpty(employeeDOB))
                            return employeeDOB;
                    }                   
                    catch (FormatException)
                    {
                        return string.Empty;
                    }
                }

                return string.Empty;
            }
        }
    }
}