using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using neihanshe.Core.Model;

namespace neihanshe.Core
{
    public class ParseDataUtils
    {
        /// <summary>
        /// 获取数据工具类
        /// </summary>
        /// <param name="json">需要解析的json数据</param>
        public static List<Post> ParsePost(string json)
        {
            List<Post> posts = new List<Post>();
            if (json.Contains("[\"error\"]"))       // 服务器返回错误信息
                return posts;
            json = json.Remove(1, json.IndexOf(",", StringComparison.Ordinal));
            JsonArray jsonArray = JsonArray.Parse(json);

            posts.AddRange(jsonArray.Select(jsonItem => jsonItem.GetObject()).Select(itemObject => new Post()
            {
                Id = itemObject["id"].GetString(),
                Uid = itemObject["uid"].GetString(),
                UserInfo = itemObject["user_info"].GetString(),
                Title = itemObject["title"].GetString(),
                PicH = itemObject["pic_h"].GetString(),
                PicUrl = itemObject["pic_url"].GetString(),
                Up = itemObject["up"].GetString(),
                Dn = itemObject["dn"].GetString(),
                Cmt = itemObject["cmt"].GetString(),
                QNum = itemObject["q_num"].GetString(),
                TNum = itemObject["t_num"].GetString(),
                SNum = itemObject["s_num"].GetString(),
                RNum = itemObject["r_num"].GetString()
            }));

            return posts;
        }

        public static void CopyListToObservableCollection(List<Post> posts, ObservableCollection<Post> observable)
        {
            foreach (var post in posts)
            {
                observable.Add(post);
            }            
        }
    }
}
