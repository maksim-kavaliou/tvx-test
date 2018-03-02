using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Posts.Tests.Common
{
    public static class ContentHelper
    {
        public static StringContent AsJson(object o)
        {
            return new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
        }
    }
}
