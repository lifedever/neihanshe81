using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace neihanshe.Core
{
    /// <summary>
    /// Http请求工具类
    /// </summary>
    public class HttpHelper
    {
        private HttpClient HttpClient { get; set; }

        #region 构造方法
        public HttpHelper()
        {
            
        }

        public HttpHelper(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
        #endregion

        public async Task<string> GetHttpString(Uri uri)
        {
            Debug.Assert(HttpClient != null, "HttpClient未实例化！");
            HttpResponseMessage responseMessage = await HttpClient.GetAsync(uri,HttpCompletionOption.ResponseContentRead);
            return await responseMessage.Content.ReadAsStringAsync();
        }
    }
}
