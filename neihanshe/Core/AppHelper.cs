using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

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

    }
}
