using System.Collections.Generic;
using System.Diagnostics;
using Couchbase.Lite;
using Couchbase.Lite.Store;
using Newtonsoft.Json;

namespace XamarinNativeTemplate
{
    public class CouchbaseDbServices
    {
        readonly Database couchDatabase;

        static CouchbaseDbServices _couchClient;
        public static CouchbaseDbServices CouchbaseInstance
        {
            get
            {
                if (_couchClient == null)
                {
                    _couchClient = new CouchbaseDbServices();
                }

                return _couchClient;
            }
        }

        public CouchbaseDbServices()
        {
            var dbOptions = new DatabaseOptions
            {
                StorageType = StorageEngineTypes.SQLite,

                EncryptionKey = new SymmetricKey(Security.KeySeed(), Security.SaltText(), 10000),

                Create = true
            };

            var db = Manager.SharedInstance.OpenDatabase("couchbase-native", dbOptions);

            couchDatabase = db;
        }

        // Method to save multiple (list/collection) records into couchbase db
        public void SaveUsers(string users)
        {
            var vals = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(users);

            foreach (var employee in vals)
            {
                employee.Add("type", "employee");

                // if document doesnt exists, and empty one will be created
                var doc = couchDatabase.GetDocument(employee["id"].ToString());

                try
                {
                    if (doc.Properties == null)
                    {
                        doc.PutProperties(employee);
                    }
                    else
                    {
                        var props = new Dictionary<string, object>(doc.Properties);

                        var keys = props.Diff(employee);

                        if (keys.Count > 0)
                        {
                            foreach (var key in keys)
                            {
                                props[key] = employee[key];
                            }

                            doc.PutProperties(props);
                        }
                    }
                }
                catch (CouchbaseLiteException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        // Method to save 1 record into couchbase
        public string SaveUser(Employee employee)
        {
            var lEmployee = employee.GetDictionary();

            lEmployee.Add("type", "employee");

            // if document doesnt exists, and empty one will be created
            var doc = couchDatabase.GetDocument(lEmployee["id"].ToString());

            try
            {
                if (doc.Properties == null)
                {
                    doc.PutProperties(lEmployee);
                }
                else
                {
                    var props = new Dictionary<string, object>(doc.Properties);

                    var keys = props.Diff(lEmployee);

                    if (keys.Count > 0)
                    {
                        foreach (var key in keys)
                        {
                            props[key] = lEmployee[key];
                        }

                        doc.PutProperties(props);
                    }
                }

                return "";
            }
            catch (CouchbaseLiteException ex)
            {
                return ex.Message;
            }
        }

        // Method to get/read records from couchbase DB
        public List<Employee> ReadRecord()
        {
            try
            {
                var query = couchDatabase.CreateAllDocumentsQuery();

                query.PostFilter = (arg) => arg.Document.Properties["type"].ToString() == "employee";

                var rows = query.Run();

                var result = new List<Employee>();

                foreach (var row in rows)
                {
                    var dict = row.Document.Properties;

                    var lEmployee = (Employee)dict.GetObject(typeof(Employee));

                    result.Add(lEmployee);
                }

                return result;
            }
            catch (CouchbaseLiteException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }

        // Method to delete record from couchbase DB, based any kind of "key". (Id in this case)
        public bool DeleteRecord(int Id)
        {
            try
            {
                var doc = couchDatabase.GetExistingDocument(Id.ToString());

                doc.Delete();

                return doc.Deleted;
            }
            catch (CouchbaseLiteException ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
