using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using RedCorners.Models;
using Xamarin.Forms;
using RedCorners.Forms;

#if WINDOWS
using System.Windows;
#endif

namespace RedCorners
{
    [AttributeUsage(AttributeTargets.All)]
    public class ManualUpdate : Attribute { }

    public partial class BindableModel : INotifyPropertyChanged
    {
        protected readonly static Random Random = new Random();
        public virtual bool IsModal { get; set; } = true;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string m = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(m));
        public void RaisePropertyChanged([CallerMemberName] string m = null) =>
            OnPropertyChanged(m);

        public event EventHandler<string> OnLog;

        public virtual bool IsBusy
        {
            get => Status == TaskStatuses.Busy;
            set
            {
                if (value) Status = TaskStatuses.Busy;
                else Status = TaskStatuses.Success;
            }
        }

        [ManualUpdate] public bool IsFailed => Status == TaskStatuses.Fail;
        [ManualUpdate] public bool IsFinished => Status == TaskStatuses.Success;
        [ManualUpdate] public bool IsFirstTime => _isFirstTime;
        [ManualUpdate] public bool IsIdle => !IsBusy;
        [ManualUpdate] public bool IsNotFailed => !IsFailed;
        [ManualUpdate] public bool IsNotFinished => !IsFinished;

        TaskStatuses _status = TaskStatuses.Busy;
        bool _isFirstTime = true;
        public TaskStatuses Status
        {
            get => _status;
            set
            {
                var hasChanged = _status != value;
                if (hasChanged)
                    _status = value;
                if (_status == TaskStatuses.Success)
                    _isFirstTime = false;
                if (hasChanged)
                    UpdateProperties(true);
            }
        }


        public void ResetFirstTime()
        {
            _isFirstTime = true;
            UpdateProperties();
        }

        public virtual void Refresh()
        {

        }

        public void Log(string message = null, [CallerMemberName] string method = null)
        {
            OnLog?.Invoke(this, $"[{method}] {message ?? "(null)"}");
        }

        public BindableModel() { }

        protected virtual void SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            storage = value;
            RaisePropertyChanged(propertyName);
        }

        public void UpdateProperties(bool forceAll = false)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                foreach (var item in GetType().GetProperties())
                {
                    if (item.GetCustomAttributes(typeof(ManualUpdate), true).Any() && !forceAll)
                        continue;

                    RaisePropertyChanged(item.Name);
                }
            });
        }

        public void UpdateProperties(IEnumerable<string> names)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                foreach (var item in names)
                    RaisePropertyChanged(item);
            });
        }

        public const string DefaultLoadingText = "Loading...";

        string _loadingText = DefaultLoadingText;

        public virtual string LoadingText
        {
            get => _loadingText;
            set
            {
                SetProperty(ref _loadingText, value);
            }
        }

        public const string DefaultErrorText = "Something went wrong";
        string _errorText = DefaultErrorText;
        [ManualUpdate]
        public virtual string ErrorText
        {
            get => _errorText;
            set
            {
                _errorText = value;
                RaisePropertyChanged();
            }
        }

        public virtual Command RefreshCommand => new Command(Refresh);

        public virtual Command GoBackCommand => new Command(() =>
        {
            Signals.RunOnUI.Send<Action>(() => OnBack());
        });

        protected bool backed = false;
        public virtual bool OnBack()
        {
            if (backed) return true;
            backed = true;
            return OnBackSuccessful();
        }

        public virtual bool OnBackSuccessful()
        {
            if (IsModal)
            {
                Signals.PopModal.Send();
            }
            else Signals.ShowFirstPage.Send();
            return true;
        }

        public virtual void OnAppeared(ContentPage page)
        {

        }

        /// <summary>
        /// Call from the View to activate the view model
        /// </summary>
        public virtual void Start()
        {
            backed = false;
        }

        /// <summary>
        /// Call from the view to deactivate the view model
        /// </summary>
        public virtual void Stop()
        {

        }
    }
}
