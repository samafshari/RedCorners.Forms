# RedCorners.Forms

RedCorners.Forms brings some neat utilities to your Xamarin.Forms applications

# Getting Started

## Installing the NuGet

In a Xamarin.Forms solution with a Shared Project structure, install the NuGet separately on your iOS and Android projects. **Note: This is not intended to be installed on a .NET Standard Xamarin Forms project.**

NuGet: [https://www.nuget.org/packages/RedCorners.Forms](https://www.nuget.org/packages/RedCorners.Forms)

<table>
<tr>
<td>
![Alt text](Screenshots/1.png?raw=true "Title")
</td>
<td>
![Alt text](Screenshots/2.png?raw=true "Title")
</td>
</tr>
<tr>
<td>
![Alt text](Screenshots/3.png?raw=true "Title")
</td>
<td>
![Alt text](Screenshots/4.png?raw=true "Title")
</td>
</tr>
</table>

### Dependencies

RedCorners.Forms depends on the following packages for both platforms:
- RedCorners (4.1.1)
- Xamarin.Forms (3.6.0.344457)

On Android, it depends on:
- Xamarin.Android.Support.v7.AppCompat (28.0.0.1)

## Demo Project

To have a quick demo of the project, run the `RedCorners.Demo.iOS` or `RedCorners.Demo.Android` projects from the `RedCorners.Forms.sln` solution.


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

## AliveContentPage

RedCorners.Forms provides `AliveContentPage`, which is ContentPage on steroids. Some of its notable features are:
- Fixing the paddings on iPhone X series
- Bindable property to change the status bar style on the iOS
- Bindable property to make the status bar translucent on Android
- Bindable properties for changing Android's status bar color, and paddings
- Letting `BindableModel` contexts know when the page appears and disappears

## AliveContentView

`AliveContentView` is like `AliveContentPage`, but is based on a `ContentView` instead. It offers properties that fix the paddings on iPhone X series.

## BindableModel

`BindableModel` is intended to be used as the base class for view models. When used with `AliveContentPage` and `AliveContentView` it gains the power to know when its views and pages appear and disappear.

## PageCommand

`PageCommand` is a `Command` that when executed, it can show a page or activate a new one based on a supplied type.

## PopCommand

`PopCommand` is a `Command` that dismisses the current popup page.

## Sidebar

`Sidebar` is a powerful Sidebar with the following features:
- Customizable width as a GridLength. Supports Star values (i.e. width proportional to the screen size) as well as absolute values.
- Supports any Xamarin.Forms element as its content
- FadeIn effect with customizable duration
- SlideIn/Out effects with customizable duration
- Customizable side (you can specify which side of the screen it sticks to)
- Swipe gestures support

# Example

## MainPage.xaml

Define namespaces:
```
<rf:AliveContentPage 
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:views="clr-namespace:RedCorners.Demo.Views"
    xmlns:rf="clr-namespace:RedCorners.Forms;assembly=RedCorners.Forms"
    xmlns:ff="clr-namespace:FFImageLoading.Forms;assembly=FFImageLoading.Forms"
    xmlns:vm="clr-namespace:RedCorners.Demo.ViewModels"
```

We don't want to fix paddings just yet.
```
    FixBottomPadding="False"
    FixTopPadding="False"
```

iOS Status Bar style:
```
    UIStatusBarStyle="{Binding UIStatusBarStyle}"
```

Android Status Bar style, color, translucency and margins:
```
    AndroidLayoutInScreen="{Binding AndroidLayoutInScreen}"
    AndroidTranslucentStatus="{Binding AndroidTranslucentStatus}"
    AndroidStatusBarColor="{Binding AndroidStatusBarColor}"
```

Class name:
```
    x:Class="RedCorners.Demo.Views.MainPage">
```

Binding context:
```
    <rf:AliveContentPage.BindingContext>
        <vm:MainViewModel />
    </rf:AliveContentPage.BindingContext>
```

Colorful background image that goes below the status bar:
```
    <Grid>
        <ff:CachedImage
            Aspect="AspectFill"
            Source="{Binding BackgroundImage}" />
```     

Fix paddings with an AliveContentView:
```
        <rf:AliveContentView FixTopPadding="True" BackgroundColor="Transparent">
```

Add a button that opens a new page using a `PageCommand`. The inner page contains a button that pops the page using a `PopCommand`:
```
            <StackLayout Padding="10">
                <Button Text="Test Modal">
                    <Button.Command>
                        <rf:PageCommand IsModal="True">
                            <rf:AliveContentPage>
                                <Grid>
                                    <Button Text="Pop" HorizontalOptions="Center" VerticalOptions="Center">
                                        <Button.Command>
                                            <rf:PopCommand FireOnce="False" FireDelay="1000" />
                                        </Button.Command>
                                    </Button>
                                </Grid>
                            </rf:AliveContentPage>
                        </rf:PageCommand>
                    </Button.Command>
                </Button>
```

Add a button that shows a modal page defined in a separate class:
```
                <Button Text="Show Modal From Page">
                    <Button.Command>
                        <rf:PageCommand IsModal="True">
                            <rf:PageCommand.Page>
                                <views:CounterPage />
                            </rf:PageCommand.Page>
                        </rf:PageCommand>
                    </Button.Command>
                </Button>
```

Add a button that shows the same modal page, but every time activates a new instance of that page from its type:
```
                <Button Text="Show Modal From Type">
                    <Button.Command>
                        <rf:PageCommand IsModal="True" PageType="{Type views:CounterPage}" />
                    </Button.Command>
                </Button>
```

Add a button that shows the sidebar:
```
                <Button Text="Show Sidebar" Command="{Binding ShowSidebarCommand}" />
```

Add a switch that toggles the iOS status bar style:
```
                <Grid Padding="2" BackgroundColor="#EE000000">
                    <Label Text="LightContent" TextColor="White" VerticalOptions="Center" />
                    <Switch HorizontalOptions="End" IsToggled="{Binding LightContent}" />
                </Grid>
```                

Add switches and buttons that toggle Android status bar properties:
```             
                <Grid Padding="2" BackgroundColor="#99000000">
                    <Label Text="AndroidLayoutInScreen" TextColor="White" VerticalOptions="Center" />
                    <Switch HorizontalOptions="End" IsToggled="{Binding AndroidLayoutInScreen}" />
                </Grid>
                
                <Grid Padding="2" BackgroundColor="#99000000">
                    <Label Text="AndroidTranslucentStatus" TextColor="White" VerticalOptions="Center" />
                    <Switch HorizontalOptions="End" IsToggled="{Binding AndroidTranslucentStatus}" />
                </Grid>

                <Grid>
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                    </Grid.ColumnDefinitions>
                    <Button Grid.Column="0" Text="Blue" Command="{Binding BlueStatusBarCommand}" />
                    <Button Grid.Column="1" Text="Green" Command="{Binding GreenStatusBarCommand}" />
                </Grid>                
```

Add a switch that toggles the sidebar's side:
```
                <Grid Padding="2" BackgroundColor="#99000000">
                    <Label Text="IsRight" TextColor="White" VerticalOptions="Center" />
                    <Switch HorizontalOptions="End" IsToggled="{Binding IsRight}" />
                </Grid>
                

            </StackLayout>
        </rf:AliveContentView>
```

Define the sidebar:
```
        <rf:Sidebar Side="{Binding Side}">
            <Frame
                HasShadow="True"
                HorizontalOptions="Fill"
                VerticalOptions="Fill"
                BackgroundColor="#BB000000"
                Padding="{Static rf:Values.PageMargin}"
                CornerRadius="0">
                <StackLayout Padding="10" Spacing="20">
                    <ff:CachedImage Source="http://ooze.redcorners.com/redcorners_forms_logo.png" HorizontalOptions="Center" HeightRequest="128" WidthRequest="128" />
                    <Label Text="RedCorners.Forms" TextColor="White" FontSize="Large" HorizontalTextAlignment="Center" HorizontalOptions="Center" />
                    <BoxView HeightRequest="1" HorizontalOptions="Fill" BackgroundColor="White" />
                </StackLayout>
            </Frame>
        </rf:Sidebar>
    </Grid>
    
</rf:AliveContentPage>
```

## MainViewModel
```c#
using RedCorners.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Reflection;
using static RedCorners.Forms.Sidebar;

namespace RedCorners.Demo.ViewModels
{
    public class MainViewModel : BindableModel
    {
        bool _androidTranslucentStatus = true;
        public bool AndroidTranslucentStatus
        {
            get => _androidTranslucentStatus;
            set => SetProperty(ref _androidTranslucentStatus, value);
        }

        bool _androidLayoutInScreen = false;
        public bool AndroidLayoutInScreen
        {
            get => _androidLayoutInScreen;
            set => SetProperty(ref _androidLayoutInScreen, value);
        }

        bool _lightContent = true;
        public bool LightContent
        {
            get => _lightContent;
            set
            {
                _lightContent = value;
                UpdateProperties();
            }
        }

        public UIStatusBarStyles UIStatusBarStyle => LightContent ? UIStatusBarStyles.LightContent : UIStatusBarStyles.Default;

        Color _androidStatusBarColor = Color.Red;
        public Color AndroidStatusBarColor
        {
            get => _androidStatusBarColor;
            set => SetProperty(ref _androidStatusBarColor, value);
        }

        Sides _side = Sides.Right;
        public Sides Side
        {
            get => _side;
        }

        public bool IsRight
        {
            get => _side == Sides.Right;
            set
            {
                _side = value ? Sides.Right : Sides.Left;
                UpdateProperties();
            }
        }

        public Command BlueStatusBarCommand => new Command(() => AndroidStatusBarColor = Color.FromHex("#770000FF"));
        public Command GreenStatusBarCommand => new Command(() => AndroidStatusBarColor = Color.FromHex("#7700FF00"));

        public ImageSource BackgroundImage => "https://images.pexels.com/photos/163822/color-umbrella-red-yellow-163822.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260";
    }
}

```