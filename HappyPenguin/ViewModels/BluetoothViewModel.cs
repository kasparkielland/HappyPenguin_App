using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace HappyPenguin.ViewModels
{
    public class BluetoothViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;
        private IAdapter _btAdapter;
        private IDevice _btDevice;

        public ICommand TapCommand { get; set; }

        private bool _isScanning;

        public bool IsScanning
        {
            get { return _isScanning; }
            set { SetProperty(ref _isScanning, value); }
        }

        private string _statusText;

        public string StatusText
        {
            get { return _statusText; }
            set { SetProperty(ref _statusText, value); }
        }

        private string _actionText;

        public string ActionText
        {
            get { return _actionText; }
            set { SetProperty(ref _actionText, value); }
        }

        public BluetoothViewModel() //INavigationService navigationService, IAdapter btAdapter
        {

            //_navigationService = navigationService;
            //_btAdapter = btAdapter;
            _btAdapter.DeviceDiscovered += OnDeviceDiscovered;
            TapCommand = new DelegateCommand(OnButtonTapped);
            ActionText = "Tap to scan";
        }

        private void OnDeviceDiscovered(object sender, DeviceEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Device.Name) && e.Device.Name.Contains("HC-05"))
            {
                _btDevice = e.Device;
                StatusText = "Connecting...";
                NavigationParameters p = new NavigationParameters
                {
                    { "Device", _btDevice }
                };

                // _navigationService.NavigateAsync(new Uri(nameof(DeviceInfoView), UriKind.Relative), p);
            }
        }

        private async void OnButtonTapped()
        {
            IsScanning = !IsScanning;

            if (_isScanning)
            {
                StatusText = "Searching...";
                await _btAdapter.StartScanningForDevicesAsync();
            }
            else
            {
                await _btAdapter.StopScanningForDevicesAsync();
                ActionText = "Tap to scan";
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (_btDevice != null) _btAdapter.DisconnectDeviceAsync(_btDevice);
            ActionText = "Tap to scan";
            IsScanning = false;
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {

        }
    }
}

//using System.ComponentModel;
//using System.Windows.Input;
//using Xamarin.Forms;
//using Plugin.BluetoothLE;
//using Java.Util;

//namespace HappyPenguin.ViewModels
//{
//    public class BluetoothViewModel : INotifyPropertyChanged
//    {
//        // Unique ID for bloetooth device
//        private const string UUID = "00001101-0000-1000-8000-00805F9B34FB";



//        string status;
//        ICommand tapCommand;
//        public event PropertyChangedEventHandler PropertyChanged;

//        public ICommand TapCommand
//        {
//            get { return tapCommand; }
//        }


//        public BluetoothViewModel()
//        {
//            // configure the TapCommand with a method
//            tapCommand = new Command(OnTapped);

//        }

//        public string Status
//        {
//            set
//            {
//                if (status != value)
//                {
//                    status = value;
//                    OnPropertyChanged("Status");
//                }
//            }
//            get
//            {
//                return status;
//            }
//        }


//        void OnTapped()
//        {
//            this.Status = "Button pressed";

//        }



//        protected virtual void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}