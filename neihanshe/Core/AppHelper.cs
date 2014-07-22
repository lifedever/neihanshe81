using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using neihanshe.Common;

namespace neihanshe.Core
{
    public static class AppHelper
    {
        /// <summary>
        /// 初始化状态栏
        /// </summary>
        public static void ShowStatusBar()
        {
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ShowAsync();
            statusBar.BackgroundColor = Colors.DodgerBlue;
            statusBar.BackgroundOpacity = 1;
            statusBar.ProgressIndicator.Text = Application.Current.Resources["AppName"] as string;
            statusBar.ProgressIndicator.ProgressValue = 0;
            statusBar.ProgressIndicator.ShowAsync();
        }

        /// <summary>
        /// 显示进度信息
        /// </summary>
        /// <param name="message"></param>
        public static void ShowProgressMessage(string message)
        {
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ShowAsync();
            statusBar.BackgroundColor = Colors.DodgerBlue;
            statusBar.BackgroundOpacity = 1;
            statusBar.ProgressIndicator.Text = message;
            statusBar.ProgressIndicator.ProgressValue = null;
            statusBar.ProgressIndicator.ShowAsync();
        }

        public static void HideStatusBar()
        {
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.HideAsync();
        }

        public async static Task<bool> UserLogin(string username, string password)
        {
            bool status = await Login(username, password);
            if (status)
            {
                SettingUtils.Save("loginname", username);
                SettingUtils.Save("password", password);
                // 获取登录用户信息
                await ParseUserInfoFromHtml();

            }
            return status;
        }
        private static async Task ParseUserInfoFromHtml()
        {
            HttpResponseMessage message = await App.HttpClient.GetAsync(new Uri("http://neihanshe.cn/set", UriKind.Absolute));
            var contentType = message.Content.Headers.ContentType;
            if (contentType != null && string.IsNullOrEmpty(contentType.CharSet))
            {
                contentType.CharSet = "utf-8";
            }
            string returnHtml = await message.Content.ReadAsStringAsync();
            using (StringReader sr = new StringReader(returnHtml))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("账号名称：")) // 用户名
                    {
                        string str = line.Substring(line.LastIndexOf("value=", StringComparison.Ordinal));
                        string username = str.Split('\"')[1];
                        SettingUtils.Save("username", username);
                    }
                    if (line.Contains("<div class=\"avatar\">"))    //用户头像
                    {
                        string avatar = line.Split('\"')[3];
                        SettingUtils.Save("avatar", avatar);
                    }
                }
            }

        }
        /// <summary>
        /// 模拟登陆
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static async Task<bool> Login(string username, string password)
        {
            // This is the postdata
            var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("user", username),
                new KeyValuePair<string, string>("pass", password)
            };

            IHttpContent content = new HttpFormUrlEncodedContent(postData);

            HttpResponseMessage message = await App.HttpClient.PostAsync(new Uri("http://neihanshe.cn/login", UriKind.Absolute), content);
            var contentType = message.Content.Headers.ContentType;
            if (contentType != null && string.IsNullOrEmpty(contentType.CharSet))
            {
                contentType.CharSet = "utf-8";
            }
            if (message.Content.ToString().Contains("<li id=\"error_info\">账号或密码不正确！</li>"))
            {
                return false;
            }
            return true;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        
    }
}
