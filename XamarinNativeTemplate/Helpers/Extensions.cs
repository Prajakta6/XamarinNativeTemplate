using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace XamarinNativeTemplate
{
    public static class Extensions
    {
        public static byte[] GetBytes(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(this byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static List<string> Diff(this IDictionary<string, object> dict1, IDictionary<string, object> dict2)
        {
            var result = new List<string>();

            foreach (var key in dict2.Keys)
            {
                if (dict1[key] == null || dict2[key] == null)
                {
                    result.Add(key);
                    continue;
                }

                if (dict2[key] is JObject)
                {
                    if (!JToken.DeepEquals((JObject)dict1[key], (JObject)dict2[key]))
                    {
                        result.Add(key);
                    }
                }
                else if (!dict1[key].Equals(dict2[key]))
                {
                    result.Add(key);
                }
            }

            return result;
        }

        public static IDictionary<string, object> GetDictionary(this object obj)
        {
            var strObj = JsonConvert.SerializeObject(obj);

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(strObj);
        }

        public static Object GetObject(this IDictionary<string, object> dict, Type type)
        {
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                var prop = type.GetRuntimeProperty(kv.Key);
                if (prop == null) continue;

                object value = kv.Value;

                if (value is Int64) // It seems couchbase saves Integers as Int64
                {
                    value = Convert.ToInt32(value);
                }

                if (value is double) // It seems couchbase saves Single as Double
                {
                    value = Convert.ToSingle(value);
                }

                if (value is JObject)
                {
                    value = ((JObject)value).ToObject(prop.PropertyType);
                }

                prop.SetValue(obj, value, null);
            }
            return obj;
        }
    }
}
