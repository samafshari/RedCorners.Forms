using System;
using System.Text;
using System.Linq;
using RedCorners.Forms;
using RedCorners.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RedCorners.Demo.ViewModels
{
    public class SearchViewModel : BindableModel
    {
        public SearchViewModel()
        {
            Status = TaskStatuses.Success;
        }

        bool _isCancelVisible = false;
        public bool IsCancelVisible
        {
            get => _isCancelVisible;
            set => SetProperty(ref _isCancelVisible, value);
        }

        string _textChangeResult = "";
        public string TextChangeResult
        {
            get => _textChangeResult;
            set => SetProperty(ref _textChangeResult, value);
        }

        public Command<string> CancelCommand => new Command<string>(s =>
            App.Instance.DisplayAlert("Cancel", s, "OK"));

        public Action<string> TextChangeAction => s => TextChangeResult = s;
    }
}
