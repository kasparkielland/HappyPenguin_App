using MvvmCross;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace HappyPenguin.ViewModels
{
    public class BluetoothViewModel : INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private IBluetoothLE ble;
        private IAdapter adapter;
        private ObservableCollection<IDevice> deviceList;

        private IAdapter _btAdapter;
        private IDevice _btDevice;

        ICommand tapCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand TapCommand
        {
            get { return tapCommand; }
        }

        private bool _deviceList_ItemSelected;

        public bool DeviceList_ItemSelected
        {
            get { return _deviceList_ItemSelected; }
            set
            {
                if (_deviceList_ItemSelected != value)
                {
                    _deviceList_ItemSelected = value;
                    OnPropertyChanged("DeviceList_ItemSelected");
                }
            }
        }

        private bool _isScanning;

        public bool IsScanning
        {
            get { return _isScanning; }
            set
            {
                if (_isScanning != value)
                {
                    _isScanning = value;
                    OnPropertyChanged("IsScanning");
                }
            }
        }

        private string _pairedDevice;

        public string PairedDevice
        {
            get { return _pairedDevice; }
            set
            {
                if (_pairedDevice != value)
                {
                    _pairedDevice = value;
                    OnPropertyChanged("PairedDevice");
                }
            }
        }

        private string _statusText;

        public string StatusText
        {
            get { return _statusText; }
            set
            {
                if (_statusText != value)
                {
                    _statusText = value;
                    OnPropertyChanged("StatusText");
                }
            }
        }

        private string _actionText;

        public string ActionText
        {
            get { return _actionText; }
            set
            {
                if (_actionText != value)
                {
                    _actionText = value;
                    OnPropertyChanged("ActionText");
                }
            }
        }

        private string _isActionEnabled;

        public string IsActionEnabled
        {
            get { return _isActionEnabled; }
            set
            {
                if (_isActionEnabled != value)
                {
                    _isActionEnabled = value;
                    OnPropertyChanged("IsActionEnabled");
                }
            }
        }

        public BluetoothViewModel()
        {
            this.IsScanning = false;
            this.PairedDevice = "No devices connected";
            this.ActionText = "Start Scan";
            this.StatusText = "";
            this.IsActionEnabled = "True";
            if (_btDevice != null) _btAdapter.DisconnectDeviceAsync(_btDevice);

            //ble = CrossBluetoothLE.Current;
            //adapter = CrossBluetoothLE.Current.Adapter;

            // configure the TapCommand with a method
            tapCommand = new DelegateCommand(OnButtonTapped);
        }

        public BluetoothViewModel(INavigationService navigationService, IAdapter btAdapter) //INavigationService navigationService,IAdapter /btAdapter
        {

            _navigationService = navigationService;
            _btAdapter = btAdapter;
            _btAdapter.DeviceDiscovered += OnDeviceDiscovered;
            //tapCommand = new DelegateCommand(OnButtonTapped);
        }

        private async void OnDeviceDiscovered(object sender, DeviceEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Device.Name) && e.Device.Name.Contains("MLT-BT05"))
            {
                _btDevice = e.Device;
                StatusText = "Connecting...";
                try
                {
                    await _btAdapter.ConnectToDeviceAsync(_btDevice);
                }
                catch (DeviceConnectionException exeption)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Could not connect to bluetooth\nError: " + exeption, "OK");
                }
                catch (Exception exeption)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Could not connect to bluetooth\nError: " + exeption, "OK");
                }
                NavigationParameters p = new NavigationParameters
                {
                    { "Device", _btDevice }
                };

                // _navigationService.NavigateAsync(new Uri(nameof(DeviceInfoView), UriKind.Relative), p);
            }
        }

        private async void OnButtonTapped()
        {
            //var state = ble.State;
            //if (state.ToString().Equals("Off"))
            //{
            //    await Application.Current.MainPage.DisplayAlert("Bluetooth is OFF", "Your bluetooth is turned OFF!\nPlease go to settings //and turnbluetooth ON", "OK");
            //}


            IsScanning = !IsScanning;

            if (_isScanning)
            {
                StatusText = "Searching...";
                ActionText = "Stop scan";
                IsActionEnabled = "False";
                try
                {
                    await _btAdapter.StartScanningForDevicesAsync();
                }
                catch (DeviceConnectionException exeption)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Could not scan for devices\nDevice Connection Exception Error: " + exeption, "OK");
                }
                catch (Exception exeption)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Could not scan for devices\nException Error: " + exeption, "OK");
                }
            }
            else
            {
                try
                {
                    await _btAdapter.StopScanningForDevicesAsync();
                }
                catch (DeviceConnectionException exeption)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Could not stop scan for devices\nError: " + exeption, "OK");
                }
                catch (Exception exeption)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Could not stop scan for devices\nError: " + exeption, "OK");
                }
            }
            ActionText = "Start scan";
            IsActionEnabled = "True";
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}