using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using neihanshe.Core.Model;

namespace neihanshe.Core
{
    /// <summary>
    /// 存储程序常量
    /// </summary>
    public static partial class NeihanApi
    {
        private const string Url = "http://neihanshe.cn/apps/get_json.php?class={0}&page={1}&group_id=undefined";

        /// <summary>
        /// 获取链接
        /// </summary>
        /// <param name="category">分类（包括菜单）</param>
        /// <param name="page">页数</param>
        /// <returns>当前的url</returns>
        public static string GetCurrentUrl(string category, int page)
        {
            Debug.Assert(page>0, "参数 page 必须大于 0！");
            string url = string.Format(Url, category, page);
            Debug.WriteLine(url);
            return url;
        }

        /// <summary>
        /// 获取应用程序菜单
        /// </summary>
        /// <returns>返回所有菜单</returns>
        public static List<Menu> GetMenus()
        {
            return new List<Menu>()
            {
                new Menu(){Name = "index", Title = "首页"},
                new Menu(){Name = "hot", Title = "热门"},
                new Menu(){Name = "cmt_hot", Title = "热评"},
                new Menu(){Name = "new", Title = "最新"}
            };
        }

        /// <summary>
        /// 获取默认菜单
        /// </summary>
        /// <returns></returns>
        public static Menu GetDefaultMenu()
        {
            return GetMenus().First();
        }
    }
}
