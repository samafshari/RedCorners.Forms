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

        bool _isTabbarVisible = true;
        public bool IsTabbarVisible
        {
            get => _isTabbarVisible;
            set => SetProperty(ref _isTabbarVisible, value);
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

        int _transitionId = 2;
        public TabbedPageTransitions Transition => (TabbedPageTransitions)TransitionId;
        public int TransitionId
        {
            get => _transitionId;
            set
            {
                SetProperty(ref _transitionId, value);
                RaisePropertyChanged(nameof(Transition));
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
    }
}
