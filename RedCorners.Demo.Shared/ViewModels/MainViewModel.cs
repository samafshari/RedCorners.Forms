using RedCorners.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Reflection;
using static RedCorners.Forms.SideBar;

namespace RedCorners.Demo.ViewModels
{
    public class MainViewModel : BindableModel
    {
        public MainViewModel()
        {
            IsModal = false;
        }

        bool _lightContent = true;
        public bool LightContent
        {
            get => _lightContent;
            set
            {
                _lightContent = value;
                UpdateProperties();
            }
        }

        public UIStatusBarStyles UIStatusBarStyle => LightContent ? UIStatusBarStyles.LightContent : UIStatusBarStyles.Default;

        #region SideBar Tests
        SidebarSides _side = SidebarSides.Right;
        public SidebarSides Side
        {
            get => _side;
            set => SetProperty(ref _side, value);
        }

        bool _isFullSize = false;
        public bool IsFullSize
        {
            get => _isFullSize;
            set => SetProperty(ref _isFullSize, value);
        }

        GridLength _contentSize = new GridLength(2, GridUnitType.Star);
        public GridLength ContentSize
        {
            get => _contentSize;
            set => SetProperty(ref _contentSize, value);
        }

        public Command<string> PlaceSidebarCommand => new Command<string>(side =>
        {
            if (Enum.TryParse<SidebarSides>(side, out var s))
                Side = s;
        });

        public Command AutoSizeCommand => new Command(() => ContentSize = GridLength.Auto);
        public Command Star2Command => new Command(() => ContentSize = new GridLength(2, GridUnitType.Star));
        public Command Absolute200Command => new Command(() => ContentSize = new GridLength(200, GridUnitType.Absolute));
        #endregion

        #region TabBar Tests
        bool _isTabbarVisible = true;
        public bool IsTabbarVisible
        {
            get => _isTabbarVisible;
            set => SetProperty(ref _isTabbarVisible, value);
        }

        bool _isTab2Visible = true;
        public bool IsTab2Visible
        {
            get => _isTab2Visible;
            set => SetProperty(ref _isTab2Visible, value);
        }

        int _selectedIndex = 0;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }

        ImageButtonStyles _tabStyle = ImageButtonStyles.ImageText;
        public ImageButtonStyles TabStyle
        {
            get => _tabStyle;
            set => SetProperty(ref _tabStyle, value);
        }

        public Command<int> TabStyleChangeCommand => new Command<int>(i =>
        {
            if (i == 0) TabStyle = ImageButtonStyles.Image;
            else if (i == 1) TabStyle = ImageButtonStyles.Text;
            else TabStyle = ImageButtonStyles.ImageText;
        });

        public Command ShowTabCommand => new Command(() =>
        {

        }, () =>
        {
            App.Instance.DisplayAlert("Oops", "You are not logged in!", "OK");
            return false;
        });

        public Command Switch2Command => new Command(() => SelectedIndex = 1);
        #endregion

        public Command<object> MessageCommand => new Command<object>(s => App.Instance.DisplayAlert("Message", s?.ToString(), "OK"));
    }
}
