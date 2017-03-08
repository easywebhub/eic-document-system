using Newtonsoft.Json;
using System;

namespace eic.common.Helper
{
    public class JsonHelper
    {
        public static T DeserializeObject<T>(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception ex)
            {
                return default(T);
            }

        }


        public static object DeserializeObject(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject(jsonString);
            }
            catch (Exception ex)
            {
                return default(object);
            }

        }

        public static string SerializeObject(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}

