using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappyPenguin.Views
{
    public partial class BluetoothPage : ContentPage
    {
        public BluetoothPage()
        {
            InitializeComponent();

            CircleScanner.Colors.Add((Color)Application.Current.Resources["themeColor"]);
            CircleScanner.Colors.Add((Color)Application.Current.Resources["lightthemeColor"]);

        }
    }
}