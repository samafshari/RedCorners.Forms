using RedCorners.Demo.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RedCorners.Forms;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RedCorners.Demo
{
    public partial class App : AppBase
    {
        public override void InitializeSystems()
        {
            InitializeComponent();

            base.InitializeSystems();

            string input = "Hello #World #Everybody!";
            var hashtags = input.Hashtags();

            LooseMessages.Ping.Subscribe<string>(this, (message) =>
            {
                Console.WriteLine(message);
            });

            LooseMessages.Ping.Send("Hello, World!");
        }

        public override Page GetFirstPage() => new Views.MainPage();
    }
}
