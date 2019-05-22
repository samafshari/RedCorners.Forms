
Screenshots and more information: [https://github.com/saeedafshari/RedCorners.Forms](https://github.com/saeedafshari/RedCorners.Forms)

![](https://github.com/saeedafshari/RedCorners.Forms/raw/master/Screenshots/5.gif)

## Preparing the Application

RedCorners.Forms provides its own Application base class, called `AppBase`, which is located under the `RedCorners.Forms` namespace. Modify your App to inherit from the new `AppBase` class instead of Xamarin.Forms Application:

```c#
//App.xaml.cs
using System;
using Xamarin.Forms;
using RedCorners.Forms;

namespace RedCorners.Demo
{
    public partial class App : AppBase
    {
        public override void InitializeSystems()
        {
            // Because we also have an App.xaml file
            InitializeComponent();

            base.InitializeSystems();
        }

        // Tell RedCorners.Forms what our first page should be
        public override Page GetFirstPage() => 
            new Views.MainPage();
    }
}
```

If you use an App.xaml file too, you have to change the base class there as well:
```c#
<rf:AppBase xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:rf="clr-namespace:RedCorners.Forms;assembly=RedCorners.Forms"
             x:Class="RedCorners.Demo.App">
</rf:AppBase>
```

Now if you run your application, RedCorners.Forms should boot up and launch your `Views.MainPage` Page.

