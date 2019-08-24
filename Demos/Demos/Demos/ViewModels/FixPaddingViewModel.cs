using System;
using System.Text;
using System.Linq;
using RedCorners.Forms;
using RedCorners.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Demos.ViewModels
{
public class FixPaddingViewModel : BindableModel
{
    public UIStatusBarStyles UIStatusBarStyle =>
        FixTopPadding ? 
            UIStatusBarStyles.LightContent :
            UIStatusBarStyles.Default;

    bool _fixTopPadding = false;
    public bool FixTopPadding
    {
        get => _fixTopPadding;
        set
        {
            SetProperty(ref _fixTopPadding, value);
            RaisePropertyChanged(nameof(UIStatusBarStyle));
        }
    }

    bool _fixBottomPadding = false;
    public bool FixBottomPadding
    {
        get => _fixBottomPadding;
        set => SetProperty(ref _fixBottomPadding, value);
    }
}
}
