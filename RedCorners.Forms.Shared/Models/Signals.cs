using System;
using System.Collections.Generic;
using System.Text;

namespace RedCorners.Forms
{
    public enum Signals
    {
        PopModal,
        RunOnUI, // Action
        ShowFirstPage,
        ShowPage, // Page
        ShowModalPage, // Page
        AppStart,
        AppSleep,
        AppResume
    }
}
