using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace neihanshe.Core.Model
{
    public class Subject
    {
        public ObservableCollection<Post> Posts { get; set; }
        public Menu Menu { get; set; }
        public int Page { get; set; }
        public Border CurrentGrid { get; set; }
    }
}
