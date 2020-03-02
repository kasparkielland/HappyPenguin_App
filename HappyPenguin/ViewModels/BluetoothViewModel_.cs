using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace HappyPenguin.ViewModels
{
    public class BluetoothViewModel_ : INotifyPropertyChanged
    {

        ICommand tapCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand TapCommand
        {
            get { return tapCommand; }
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


        public BluetoothViewModel_()
        {
            this.IsScanning = false;
            this.StatusText = "No devices connected";
            this.ActionText = "Tap to Scan";


            // configure the TapCommand with a method
            tapCommand = new Command(OnButtonTapped);
        }

        private void OnButtonTapped()
        {
            IsScanning = !IsScanning;

            if (_isScanning)
            {
                StatusText = "Searching...";
                //await _btAdapter.StartScanningForDevicesAsync();
            }
            else
            {
                //await _btAdapter.StopScanningForDevicesAsync();
                ActionText = "Tap to scan";
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
