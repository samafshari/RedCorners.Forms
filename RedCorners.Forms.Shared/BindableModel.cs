using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using RedCorners.Models;
using Xamarin.Forms;
using RedCorners.Forms;
using System.Windows.Input;
using System.Reflection;
using System.Threading.Tasks;

#if WINDOWS
using System.Windows;
#endif

namespace RedCorners.Forms
{

    [AttributeUsage(AttributeTargets.All)]
    public class NoUpdate : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All)]
    public class Updates : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All)]
    public class ManualUpdate : Attribute
    {
    }

    public partial class BindableModel : INotifyPropertyChanged
    {
        protected readonly static Random Random = new Random();

        public virtual bool IsModal { get; set; } = true;

        public bool RaisePropertyChangeOnUI { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        [Obsolete("Use RaisePropertyChanged instead.")]
        public void OnPropertyChanged([CallerMemberName] string m = null) =>
            RaisePropertyChanged(m);
        
        public void RaisePropertyChanged([CallerMemberName] string m = null) =>
            RaisePropertyChanged(RaisePropertyChangeOnUI, m);
        
        public void RaisePropertyChanged(bool onUI, [CallerMemberName] string m = null)
        {
            void Raise() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(m));
            if (onUI) Device.BeginInvokeOnMainThread(Raise);
            else Raise();
        }

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

        bool _isSideBarOpen = false;
        public virtual bool IsSideBarOpen
        {
            get => _isSideBarOpen;
            set
            {
                _isSideBarOpen = value;
                UpdateProperties();
            }
        }

        public virtual Command ShowSideBarCommand => new Command(() => IsSideBarOpen = true);
        public virtual Command HideSideBarCommand => new Command(() => IsSideBarOpen = false);

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
            Task.Run(RefreshAsync);
        }

        public virtual Task RefreshAsync()
        {
            return Task.CompletedTask;
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

        PropertyInfo[] properties = null;
        public void UpdateProperties(bool forceAll = false)
        {
            if (properties == null)
                properties = GetType().GetProperties();
            
            Device.BeginInvokeOnMainThread(() =>
            {
                foreach (var item in properties)
                {
                    if (item.GetCustomAttributes(typeof(NoUpdate), true).Any())
                        continue;
                    if (item.GetCustomAttributes(typeof(ManualUpdate), true).Any() && !forceAll)
                        continue;
                    if (item.PropertyType.IsAssignableFrom(typeof(ICommand)) && !item.GetCustomAttributes(typeof(Updates), true).Any())
                        continue;

                    RaisePropertyChanged(onUI: false, item.Name);
                }
            });
        }

        public void UpdateProperties(IEnumerable<string> names)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                foreach (var item in names)
                    RaisePropertyChanged(onUI: false, item);
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
            Signals.RunOnUI.Signal<Action>(() => OnBack());
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
                Signals.PopModal.Signal();
            }
            else Signals.ShowFirstPage.Signal();
            return true;
        }

        public virtual void OnAppeared(ContentPage page)
        {

        }

        /// <summary>
        /// Call from the View to activate the view model
        /// </summary>
        public virtual void OnStart()
        {
            backed = false;
        }

        /// <summary>
        /// Call from the view to deactivate the view model
        /// </summary>
        public virtual void OnStop()
        {

        }

        public virtual void OnBind(BindableObject bindable)
        {

        }

        public virtual void OnUnbind(BindableObject bindable)
        {

        }

        public bool IsIOS => Device.RuntimePlatform == Device.iOS;
        public bool IsAndroid => Device.RuntimePlatform == Device.Android;

        public bool IsTablet => Device.Idiom == TargetIdiom.Tablet;
        public bool IsPhone => Device.Idiom == TargetIdiom.Phone;
    }
}
