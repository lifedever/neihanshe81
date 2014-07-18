using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace neihanshe
{
    public sealed partial class FlyoutTip : UserControl
    {
        public FlyoutTip()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 调用显示提示信息
        /// </summary>
        /// <param name="element"></param>
        /// <param name="millisecond"></param>
        public async void Show(FrameworkElement element, string message , int millisecond)
        {
            MessageTextBlock.Text = message;
            MyFlyout.ShowAt(element);
            await Task.Delay(millisecond);
            MyFlyout.Hide();
        }

        /// <summary>
        /// 调用显示提示信息，默认2000秒
        /// </summary>
        /// <param name="element"></param>
        /// <param name="message"></param>
        public void Show(FrameworkElement element, string message)
        {
            Show(element, message, 2000);
        }
    }
}
