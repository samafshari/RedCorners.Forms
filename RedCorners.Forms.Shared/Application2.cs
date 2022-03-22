﻿using System;
using System.Collections.Generic;
using System.Text;
using RedCorners.Forms;
using RedCorners.Forms.Systems;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace RedCorners.Forms
{
    [Obsolete("AppBase is renamed to Application2. Please use Application2 instead.")]
    public abstract class AppBase : Application2 { }

    public abstract class Application2 : Application
    {
        protected readonly List<Func<Task>> SplashTasks = new List<Func<Task>>();

        public static Application2 Instance { get; private set; }

        public virtual bool IsResumed { get; set; }

        public Application2() : base()
        {
            Instance = this;
            HookSignals();
            InitializeSystems();
#if __IOS__
            SplashTasks.Add(GetPageMarginAsync);
#endif
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

        public virtual async void ShowSplashPage()
        {
            if (SplashTasks.Count > 0)
            {
                MainPage = GetSplashPage();
                await Task.WhenAll(SplashTasks.Select(x => x()));
                SplashTasks.Clear();
            }

            ShowFirstPage();
        }

        async Task GetPageMarginAsync()
        {
            while (!NotchSystem.Instance.HasWindowInformation)
            {
                NotchSystem.Instance.GetPageMargin();
                await Task.Delay(50);
            }
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

        public virtual void RunOnUI(Action a)
        {
            Device.BeginInvokeOnMainThread(a);
        }

        public virtual void PopModal()
        {
            MainPage?.Navigation.PopModalAsync();
        }

        public virtual async Task ShowModalPageAsync(Page page) =>
            await ShowPageAsync(page, true);

        public virtual void ShowModalPage(Page page) =>
            ShowPage(page, true);

        public virtual void DisplayAlert(string title, string message, string button)
        {
            RunOnUI(() =>
            {
                MainPage?.DisplayAlert(title, message, button);
            });
        }

        public virtual async Task DisplayAlertAsync(string title, string message, string button)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                await MainPage?.DisplayAlert(title, message, button);
            });
        }

        public virtual async Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
        {
            bool result = false;
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                result = await MainPage?.DisplayAlert(title, message, accept, cancel);
            });
            return result;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            IsResumed = true;
            Signals.AppStart.Signal();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            IsResumed = false;
            Signals.AppSleep.Signal();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            IsResumed = true;
            Signals.AppResume.Signal();
        }
    }
}
