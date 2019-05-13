using System;
using System.Collections.Generic;
using System.Text;
using RedCorners.Forms;
using RedCorners.Forms.Systems;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public abstract class AppBase : Application
    {
        public static AppBase Instance { get; private set; }

        public virtual bool IsResumed { get; set; }

        public AppBase() : base()
        {
            Instance = this;
            HookSignals();
            InitializeSystems();
            ShowSplashPage();
        }

        public virtual void InitializeSystems()
        {

        }

        void HookSignals()
        {
            Signals.PopModal.Subscribe(this, () =>
            {
                PopModal();
            });

            Signals.RunOnUI.Subscribe<Action>(this, (action) =>
            {
                RunOnUI(action);
            });

            Signals.ShowFirstPage.Subscribe(this, () =>
            {
                ShowFirstPage();
            });

            Signals.ShowPage.Subscribe<Page>(this, page =>
            {
                ShowPage(page);
            });

            Signals.ShowModalPage.Subscribe<Page>(this, page =>
            {
                ShowModalPage(page);
            });
        }

        public virtual
#if __IOS__
            async
#endif
            void ShowSplashPage()
        {
#if __IOS__
            MainPage = GetSplashPage();
            while (!NotchSystem.Instance.HasWindowInformation)
            {
                NotchSystem.Instance.GetPageMargin();
                await Task.Delay(50);
            }
#endif

            ShowFirstPage();
        }

        public virtual Page GetSplashPage() => new ContentPage
        {
            Content = new ActivityIndicator
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                IsRunning = true
            }
        };

        public virtual Page GetFirstPage() => new ContentPage
        {
            Content = new Label
            {
                Text = "RedCorners.Forms FirstPage",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            }
        };

        public virtual async Task ShowFirstPageAsync()
        {
            await ShowPageAsync(GetFirstPage());
        }

        public void ShowFirstPage() =>
            RunOnUI(async () => await ShowFirstPageAsync());

        public virtual async Task ShowPageAsync(Page page, bool isModal = false)
        {
            if (isModal && MainPage != null)
                await MainPage.Navigation.PushModalAsync(page);
            else
                MainPage = page;
        }

        public void ShowPage(Page page, bool isModal = false) =>
            RunOnUI(async () => await ShowPageAsync(page, isModal));

        public void RunOnUI(Action a)
        {
            Device.BeginInvokeOnMainThread(a);
        }

        public void PopModal()
        {
            MainPage?.Navigation.PopModalAsync();
        }

        public async Task ShowModalPageAsync(Page page) =>
            await ShowPageAsync(page, true);

        public void ShowModalPage(Page page) =>
            ShowPage(page, true);

        public void DisplayAlert(string title, string message, string button)
        {
            RunOnUI(() =>
            {
                MainPage?.DisplayAlert(title, message, button);
            });
        }

        public async Task DisplayAlertAsync(string title, string message, string button)
        {
            await MainPage?.DisplayAlert(title, message, button);
        }

        public async Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
        {
            return await MainPage?.DisplayAlert(title, message, accept, cancel);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            IsResumed = true;
            Signals.AppStart.Send();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            IsResumed = false;
            Signals.AppSleep.Send();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            IsResumed = true;
            Signals.AppResume.Send();
        }
    }
}
