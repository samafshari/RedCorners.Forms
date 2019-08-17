using System;
using System.Text;
using System.Linq;
using RedCorners.Forms;
using RedCorners.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RedCorners.Demo.ViewModels
{
    public class Frame2ViewModel : BindableModel
    {
        public Frame2ViewModel()
        {
            Status = TaskStatuses.Success;
        }

        float _shadowRadius = 100.0f;
        public double ShadowRadiusD
        {
            get => _shadowRadius;
            set
            {
                _shadowRadius = (float)value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ShadowRadius));
            }
        }

        public float ShadowRadius
        {
            get => _shadowRadius;
        }

        double _hue = 50;
        double _sat = 50;
        double _lit = 50;
        double _alpha = 100;

        public Color ShadowColor => Color.FromHsla(_hue * 0.01, _sat * 0.01, _lit * 0.01, _alpha * 0.01);
        public double Hue
        {
            get => _hue;
            set
            {
                _hue = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ShadowColor));
            }
        }

        public double Sat
        {
            get => _sat;
            set
            {
                _sat = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ShadowColor));
            }
        }

        public double Lit
        {
            get => _lit;
            set
            {
                _lit = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ShadowColor));
            }
        }

        public double Alpha
        {
            get => _alpha;
            set
            {
                _alpha = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ShadowColor));
            }
        }
    }
}
