using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SpecflowApiDemoQaTestByMe.Models;

namespace SpecflowApiDemoQaTestByMe.Support
{
    public class readConfig
    {
        public static Env getConfig()
        {
            var basePath = new UriBuilder(Directory.GetCurrentDirectory());
            if (basePath is null) throw new Exception("Path cannot be null");
            //var realPath = Path.GetDirectoryName(Uri.UnescapeDataString(basePath.Path));
            var fullPath = Path.Combine(basePath.Uri.LocalPath,
                "Support", "config.json");
            var data = JsonConvert.DeserializeObject<Env>(File.ReadAllText(fullPath));
            return data;
        } 

        public static Env GetConfigData => getConfig();
        public static string GetBaseUrl => GetConfigData.baseUrl.baseurl;
        public static string GetAllBooksEnpoint => GetConfigData.endpoints.getallbooks;
        public static string GetSingleBookEnpoint => GetConfigData.endpoints.getsinglebook;
    }
}
