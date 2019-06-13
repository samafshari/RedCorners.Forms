using RedCorners.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Demo.ViewModels
{
    public class TabBarDemosViewModel : BindableModel
    {
        public override bool IsModal => true;

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

        int _transitionId = 2;
        public TabbedContentTransitions Transition => (TabbedContentTransitions)TransitionId;
        public int TransitionId
        {
            get => _transitionId;
            set
            {
                SetProperty(ref _transitionId, value);
                RaisePropertyChanged(nameof(Transition));
            }
        }

        bool _isLoggedIn = false;
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                _isLoggedIn = value;
                UpdateProperties();
            }
        }

        int _durationId = 1;
        public int DurationId
        {
            get => _durationId;
            set
            {
                SetProperty(ref _durationId, value);
                RaisePropertyChanged(nameof(TransitionDuration));
            }
        }

        public double TransitionDuration
        {
            get
            {
                if (DurationId == 0) return 100.0;
                if (DurationId == 1) return 250.0;
                if (DurationId == 2) return 750.0;
                return 1000.0;
            }
        }

        bool _bottomTabBar = true;
        public TabBarPositions TabBarPosition => BottomTabBar ? TabBarPositions.Bottom : TabBarPositions.Top;
        public TitleBarPositions TitlePosition => BottomTabBar ? TitleBarPositions.Top : TitleBarPositions.Bottom;

        public bool BottomTabBar
        {
            get => _bottomTabBar;
            set
            {
                _bottomTabBar = value;
                UpdateProperties();
            }
        }

        public Command<int> TabStyleChangeCommand => new Command<int>(i =>
        {
            if (i == 0) TabStyle = ImageButtonStyles.Image;
            else if (i == 1) TabStyle = ImageButtonStyles.Text;
            else TabStyle = ImageButtonStyles.ImageText;
        });

        public Command<object> MessageCommand => new Command<object>(s =>
            App.Instance.DisplayAlert("Message", s?.ToString(), "OK"));

        public Command ShowLoginCommand => new Command(() => { }, () =>
        {
            if (!IsLoggedIn)
                App.Instance.DisplayAlert("Error", "Please log in to see the profile", "OK");

            return IsLoggedIn;
        });

        public Command ShowSettingsCommand => new Command(() => SelectedIndex = 1);

        bool _isTabBarVisible = true;
        public bool IsTabBarVisible
        {
            get => _isTabBarVisible;
            set => SetProperty(ref _isTabBarVisible, value);
        }

        bool _isProfileVisible = true;
        public bool IsProfileVisible
        {
            get => _isProfileVisible;
            set => SetProperty(ref _isProfileVisible, value);
        }
    }
}
