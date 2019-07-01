using RedCorners.Demo.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RedCorners.Forms;
using System.Threading.Tasks;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RedCorners.Demo
{
    public partial class App : Application2
    {
        public override void InitializeSystems()
        {
            InitializeComponent();

            base.InitializeSystems();

            LooseMessages.Ping.Subscribe<string>(this, (message) =>
            {
                Console.WriteLine(message);
            });

            LooseMessages.Ping.Signal("Hello, World!");

            SplashTasks.Add(async () =>
            {
                await Task.Delay(5000);
            });
        }

        public override Page GetFirstPage() => 
            new Views.MainPage();
    }
}
