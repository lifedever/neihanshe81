//      *********    请勿修改此文件     *********
//      此文件由设计工具再生成。更改
//      此文件可能会导致错误。
namespace Blend.SampleData.PostsDataSource
{
	using System; 
	using System.ComponentModel;

// 若要在生产应用程序中显著减小示例数据涉及面，则可以设置
// DISABLE_SAMPLE_DATA 条件编译常量并在运行时禁用示例数据。
#if DISABLE_SAMPLE_DATA
	internal class PostsDataSource { }
#else

	public class PostsDataSource : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public PostsDataSource()
		{
			try
			{
				Uri resourceUri = new Uri("ms-appx:/SampleData/PostsDataSource/PostsDataSource.xaml", UriKind.RelativeOrAbsolute);
				Windows.UI.Xaml.Application.LoadComponent(this, resourceUri);
			}
			catch
			{
			}
		}

		private Posts _Posts = new Posts();

		public Posts Posts
		{
			get
			{
				return this._Posts;
			}
		}
	}

	public class Posts : System.Collections.ObjectModel.ObservableCollection<PostsItem>
	{ 
	}

	public class PostsItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private string _Id = string.Empty;

		public string Id
		{
			get
			{
				return this._Id;
			}

			set
			{
				if (this._Id != value)
				{
					this._Id = value;
					this.OnPropertyChanged("Id");
				}
			}
		}

		private double _Uid = 0;

		public double Uid
		{
			get
			{
				return this._Uid;
			}

			set
			{
				if (this._Uid != value)
				{
					this._Uid = value;
					this.OnPropertyChanged("Uid");
				}
			}
		}

		private string _UserInfo = string.Empty;

		public string UserInfo
		{
			get
			{
				return this._UserInfo;
			}

			set
			{
				if (this._UserInfo != value)
				{
					this._UserInfo = value;
					this.OnPropertyChanged("UserInfo");
				}
			}
		}

		private string _Title = string.Empty;

		public string Title
		{
			get
			{
				return this._Title;
			}

			set
			{
				if (this._Title != value)
				{
					this._Title = value;
					this.OnPropertyChanged("Title");
				}
			}
		}

		private string _PicH = string.Empty;

		public string PicH
		{
			get
			{
				return this._PicH;
			}

			set
			{
				if (this._PicH != value)
				{
					this._PicH = value;
					this.OnPropertyChanged("PicH");
				}
			}
		}

		private string _PicUrl = string.Empty;

		public string PicUrl
		{
			get
			{
				return this._PicUrl;
			}

			set
			{
				if (this._PicUrl != value)
				{
					this._PicUrl = value;
					this.OnPropertyChanged("PicUrl");
				}
			}
		}

		private double _Up = 0;

		public double Up
		{
			get
			{
				return this._Up;
			}

			set
			{
				if (this._Up != value)
				{
					this._Up = value;
					this.OnPropertyChanged("Up");
				}
			}
		}

		private double _Dn = 0;

		public double Dn
		{
			get
			{
				return this._Dn;
			}

			set
			{
				if (this._Dn != value)
				{
					this._Dn = value;
					this.OnPropertyChanged("Dn");
				}
			}
		}

		private double _Cmt = 0;

		public double Cmt
		{
			get
			{
				return this._Cmt;
			}

			set
			{
				if (this._Cmt != value)
				{
					this._Cmt = value;
					this.OnPropertyChanged("Cmt");
				}
			}
		}

		private double _QNum = 0;

		public double QNum
		{
			get
			{
				return this._QNum;
			}

			set
			{
				if (this._QNum != value)
				{
					this._QNum = value;
					this.OnPropertyChanged("QNum");
				}
			}
		}

		private double _TNum = 0;

		public double TNum
		{
			get
			{
				return this._TNum;
			}

			set
			{
				if (this._TNum != value)
				{
					this._TNum = value;
					this.OnPropertyChanged("TNum");
				}
			}
		}

		private double _SNum = 0;

		public double SNum
		{
			get
			{
				return this._SNum;
			}

			set
			{
				if (this._SNum != value)
				{
					this._SNum = value;
					this.OnPropertyChanged("SNum");
				}
			}
		}

		private double _RNum = 0;

		public double RNum
		{
			get
			{
				return this._RNum;
			}

			set
			{
				if (this._RNum != value)
				{
					this._RNum = value;
					this.OnPropertyChanged("RNum");
				}
			}
		}
	}
#endif
}
