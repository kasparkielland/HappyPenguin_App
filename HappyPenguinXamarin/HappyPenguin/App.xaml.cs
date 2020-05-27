using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappyPenguin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}


//using HappyPenguin;
//using HappyPenguin.ViewModels;
//using HappyPenguin.Views;
//using Plugin.BLE;
//using Prism;
//using Prism.Ioc;
//using Prism.Unity;
//using System;
//using Xamarin.Forms;
//namespace HappyPenguin
//{
//    public partial class App : PrismApplication
//    {
//        public App(IPlatformInitializer initializer = null) : base(initializer)
//        {
//        }


//        protected override void RegisterTypes(IContainerRegistry containerRegistry)
//        {
//            containerRegistry.RegisterInstance(CrossBluetoothLE.Current.Adapter);
//            containerRegistry.RegisterForNavigation<AppShell>();
//            containerRegistry.RegisterForNavigation<BluetoothPage, BluetoothViewModel>();
//            //containerRegistry.RegisterForNavigation<DeviceInfoView, DeviceInfoViewViewModel>();
//        }
//        protected override void OnInitialized()
//        {
//            InitializeComponent();
//            MainPage = new AppShell();
//            NavigationService.NavigateAsync(new Uri("http://www.HappyPenguin/AppShell/BluetoothPage", UriKind.Absolute));
//        }
//        protected override void OnStart()
//        {
//            // Handle when your app starts
//        }
//        protected override void OnSleep()
//        {
//            // Handle when your app sleeps
//        }
//        protected override void OnResume()
//        {
//            // Handle when your app resumes
//        }
//    }
//}