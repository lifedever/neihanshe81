using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Html;

namespace neihanshe.Core.Model
{
    /// <summary>
    /// 文章类
    /// </summary>
    public class Post
    {
        public string Id { get; set; }
        public string Uid { get; set; }
        private string _userInfo;
        public string Title { get; set; }
        public string PicH { get; set; }
        public string PicUrl { get; set; }
        public string Up { get; set; }
        public string Dn { get; set; }
        public string Cmt { get; set; }
        public string QNum { get; set; }
        public string TNum { get; set; }
        public string SNum { get; set; }
        public string RNum { get; set; }
        public double Width { get; set; }
        public string UserInfo
        {
            get { return _userInfo; }
            set
            {
                if (value.Contains("</a>"))
                {
                    value = HtmlUtilities.ConvertToText(value);
                    
                }
                _userInfo = value;
            }
        }
    }
}
