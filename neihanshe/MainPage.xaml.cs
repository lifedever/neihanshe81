using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage.Search;
using Windows.UI;
using neihanshe.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “基本页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍
using neihanshe.Core;
using neihanshe.Core.Model;

namespace neihanshe
{
    /// <summary>
    /// 可独立使用或用于导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        #region 初始化信息

        public ObservableCollection<Post> IndexPosts { get; set; }
        public ObservableCollection<Post> HotPosts { get; set; }
        public ObservableCollection<Post> CmtHotPosts { get; set; }
        public ObservableCollection<Post> NewPosts { get; set; }

        private Subject _indexSubject = new Subject(){Page = 1};
        private Subject _hotSubject = new Subject() { Page = 1 };
        private Subject _cmtHotSubject = new Subject(){Page = 1};
        private Subject _newSubject = new Subject() { Page = 1 };

        private int _pivotIndex;

        #endregion


        public MainPage()
        {

            InitParams();

            this.InitializeComponent();

            
            App.AppWidth = Window.Current.Bounds.Width - 50;

            IndexPostListView.DataContext = this;
            HotPostListView.DataContext = this;
            CmtHotContentRoot.DataContext = this;
            NewPostListView.DataContext = this;

            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            _pivotIndex = -1;
        }

        private void InitParams()
        {
            // 初始化集合
            IndexPosts = new ObservableCollection<Post>();
            HotPosts = new ObservableCollection<Post>();
            NewPosts = new ObservableCollection<Post>();
            CmtHotPosts = new ObservableCollection<Post>();
           
        }

        #region 一些与逻辑无关的代码
        /// <summary>
        /// 获取与此 <see cref="Page"/> 关联的 <see cref="NavigationHelper"/>。
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// 获取此 <see cref="Page"/> 的视图模型。
        /// 可将其更改为强类型视图模型。
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// 使用在导航过程中传递的内容填充页。  在从以前的会话
        /// 重新创建页时，也会提供任何已保存状态。
        /// </summary>
        /// <param name="sender">
        /// 事件的来源; 通常为 <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">事件数据，其中既提供在最初请求此页时传递给
        /// <see cref="Frame.Navigate(Type, Object)"/> 的导航参数，又提供
        /// 此页在以前会话期间保留的状态的
        /// 字典。 首次访问页面时，该状态将为 null。</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// 保留与此页关联的状态，以防挂起应用程序或
        /// 从导航缓存中放弃此页。值必须符合
        /// <see cref="SuspensionManager.SessionState"/> 的序列化要求。
        /// </summary>
        /// <param name="sender">事件的来源；通常为 <see cref="NavigationHelper"/></param>
        ///<param name="e">提供要使用可序列化状态填充的空字典
        ///的事件数据。</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper 注册

        /// <summary>
        /// 此部分中提供的方法只是用于使
        /// NavigationHelper 可响应页面的导航方法。
        /// <para>
        /// 应将页面特有的逻辑放入用于
        /// <see cref="NavigationHelper.LoadState"/>
        /// 和 <see cref="NavigationHelper.SaveState"/> 的事件处理程序中。
        /// 除了在会话期间保留的页面状态之外
        /// LoadState 方法中还提供导航参数。
        /// </para>
        /// </summary>
        /// <param name="e">提供导航方法数据和
        /// 无法取消导航请求的事件处理程序。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            AppHelper.ShowStatusBar();

            string username = SettingUtils.Get("username") as string;
            if (username != null)
                UserTextBlock.Text = username;

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        #endregion

        private async void LoadPostData(Subject subject)
        {
            try
            {
                AppHelper.ShowProgressMessage("正在加载数据......");
                subject.CurrentGrid.Visibility = Visibility.Collapsed;
                LoginAppBarButton.IsEnabled = false;
                HttpHelper helper = new HttpHelper(App.HttpClient);
                string content = await helper.GetHttpString(new Uri(NeihanApi.GetCurrentUrl(subject.Menu.Name, subject.Page)));

                List<Post> posts = ParseDataUtils.ParsePost(content);
                if (posts.Count > 0)
                {
                    ParseDataUtils.CopyListToObservableCollection(posts, subject.Posts);
                }
                else
                {
                    ShowTipMessage("未获取到数据，请稍后重试！");
                }
                AppHelper.ShowStatusBar();
                LoginAppBarButton.IsEnabled = true;
                subject.CurrentGrid.Visibility = Visibility.Visible;
                ShowTipMessage(string.Format("已成功加载{0}条数据，图片加载可能有些慢，请小伙伴耐心等待！", subject.Posts.Count));
            }
            catch (Exception e)
            {
                ShowTipMessage("数据加载失败，请稍后重试！");
            }

        }

        #region 工具方法
        private Subject GetCurrentSubject()
        {
            switch (DataPivot.SelectedIndex)
            {
                case 0:
                    {
                        _indexSubject.Menu = NeihanApi.GetMenus()[0];
                        _indexSubject.Posts = IndexPosts;
                        _indexSubject.CurrentGrid = IndexFooterGrid;
                        _indexSubject.ListView = IndexPostListView;
                        return _indexSubject;
                    }
                case 1:
                    {
                        _hotSubject.Menu = NeihanApi.GetMenus()[1];
                        _hotSubject.Posts = HotPosts;
                        _hotSubject.CurrentGrid = HotFooterGrid;
                        _hotSubject.ListView = HotPostListView;
                        return _hotSubject;
                    }
                case 2:
                    {
                        _cmtHotSubject.Menu = NeihanApi.GetMenus()[2];
                        _cmtHotSubject.Posts = CmtHotPosts;
                        _cmtHotSubject.CurrentGrid = CmtHotFooterGrid;
                        _cmtHotSubject.ListView = CmtHotPostListView;
                        return _cmtHotSubject;
                    }
                case 3:
                    {
                        _newSubject.Menu = NeihanApi.GetMenus()[3];
                        _newSubject.Posts = NewPosts;
                        _newSubject.CurrentGrid = NewFooterGrid;
                        _newSubject.ListView = NewPostListView;
                        return _newSubject;
                    }
                default:
                    {
                        _indexSubject.Menu = NeihanApi.GetMenus()[0];
                        _indexSubject.Posts = IndexPosts;
                        _indexSubject.CurrentGrid = IndexFooterGrid;
                        return _indexSubject;
                    }
            }
        }

        /// <summary>
        /// 显示弹出提示
        /// </summary>
        /// <param name="message"></param>
        private void ShowTipMessage(string message)
        {
            MyFlyoutTip.Show(this, message, 3000);
        }

        #endregion

        private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// 点击下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        private void CurrentHyperlinkButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var subject = GetCurrentSubject();
            subject.Page ++;
            LoadPostData(subject);
        }

        private void Pivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataPivot.SelectedIndex != _pivotIndex) { 
                var subject = GetCurrentSubject();
                if (subject.Posts.Count <= 0)   // 没有数据才加载
                    LoadPostData(subject);
                _pivotIndex = DataPivot.SelectedIndex;
            }
        }

        private void RefreshAppBarButton_OnClick(object sender, RoutedEventArgs e)
        {
            var subject = GetCurrentSubject();
            subject.Page = 1;
            subject.Posts.Clear();
            LoadPostData(subject);
        }

        private void IndexPostListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = GetCurrentSubject().ListView.SelectedItem;
            if (item == null)
                return;
            GetCurrentSubject().ListView.SelectedItem = null;
            Frame.Navigate(typeof(PostPage), item);
        }

        private void LoginAppBarButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (LoginPage));
        }
    }
}
