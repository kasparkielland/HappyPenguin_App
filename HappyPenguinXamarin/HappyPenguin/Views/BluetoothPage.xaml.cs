
using Xamarin.Forms;
using HappyPenguin.ViewModels;

namespace HappyPenguin.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BluetoothPage : ContentPage
    {
        private BluetoothViewModel bluetoothViewModel;

        public BluetoothPage()
        {
            InitializeComponent();
            bluetoothViewModel = new BluetoothViewModel();
            this.BindingContext = bluetoothViewModel;

            //CircleScanner.Colors.Add((Color)Application.Current.Resources["themeColor"]);
            //CircleScanner.Colors.Add((Color)Application.Current.Resources["lightthemeColor"]);

        }
    }
}