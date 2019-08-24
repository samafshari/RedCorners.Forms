using RedCorners.Components;
using RedCorners.Forms;
using Demos.Systems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demos
{
    public partial class App : Application2
    {
        public override Page GetFirstPage()
            => new Views.MainPage();

        public override void InitializeSystems()
        {
            InitializeComponent();
            base.InitializeSystems();
            SplashTasks.Add(SettingsSystem.Instance.InitializeAsync);
        }
    }
}
