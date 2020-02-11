using System.Diagnostics;
using Plugin.BLE;
using Xamarin.Forms;

namespace HappyPenguin.View
{
    public partial class BluetoothPage : ContentPage
    {
        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status)); // Notify that there was a change on this property
            }
        }


        public BluetoothPage()
        {
            InitializeComponent();

        }

        void btnScan(System.Object sender, System.EventArgs e)
        {
            status = "Button pressed";
        }
    }
}