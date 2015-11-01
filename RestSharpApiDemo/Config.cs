using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpApiDemo
{
    public class Config : IConfig
    {
        private const string baseUrl = "http://kkirtland.pythonanywhere.com";

        public string GetBaseUrl()
        {
            return baseUrl;
        }
    }
}
