using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neihanshe.Core.Model
{
    /// <summary>
    /// 文章类
    /// </summary>
    public class Post
    {
        public string Id { get; set; }
        public string Uid { get; set; }
        public string UserInfo { get; set; }
        public string Title { get; set; }
        public string PicH { get; set; }
        public string PicUrl { get; set; }
        public int Up { get; set; }
        public int Dn { get; set; }
        public int Cmt { get; set; }
        public int QNum { get; set; }
        public int TNum { get; set; }
        public int SNum { get; set; }
        public int RNum { get; set; }

    }
}
