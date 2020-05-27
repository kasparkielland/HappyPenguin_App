using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Bluetooth;
using System.Threading;
using Java.IO;
using Java.Util;
using Android.OS;

namespace HappyPenguin.ViewModels
{
    public class WeightViewModel : INotifyPropertyChanged
    {
        DateTime timeStamp;
        int cornerRadius;
        string weight;
        int weightLabel_FontSize;
        bool timeStamp_IsVisible;

        private IBluetoothLE ble;
        private IAdapter adapter;
        private IDevice device;

        private CancellationTokenSource _ct { get; set; }

        const int RequestResolveError = 1000;


        ICommand tapCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand TapCommand
        {
            get { return tapCommand; }
        }

        public WeightViewModel()
        {
            this.Weight = "Log new weight";
            this.WeightLabel_FontSize = 25;
            this.CornerRadius = 120;
            this.TimeStamp_IsVisible = false;


            // configure the TapCommand with a method
            tapCommand = new Command(OnTapped);
        }



        public DateTime TimeStamp
        {
            set
            {
                if (timeStamp != value)
                {
                    timeStamp = value;
                    OnPropertyChanged("TimeStamp");
                }
            }
            get
            {
                return timeStamp;
            }
        }

        public int CornerRadius
        {
            set
            {
                if (cornerRadius != value)
                {
                    cornerRadius = value;
                    OnPropertyChanged("CornerRadius");
                }
            }
            get
            {
                return cornerRadius;
            }
        }

        public string Weight
        {
            set
            {
                if (weight != value)
                {
                    weight = value;
                    OnPropertyChanged("Weight");
                }
            }
            get
            {
                return weight;
            }
        }

        public int WeightLabel_FontSize
        {
            set
            {
                if (weightLabel_FontSize != value)
                {
                    weightLabel_FontSize = value;
                    OnPropertyChanged("WeightLabel_FontSize");
                }
            }
            get
            {
                return weightLabel_FontSize;
            }
        }

        public bool TimeStamp_IsVisible
        {
            set
            {
                if (timeStamp_IsVisible != value)
                {
                    timeStamp_IsVisible = value;
                    OnPropertyChanged("TimeStamp_IsVisible");
                }
            }
            get
            {
                return timeStamp_IsVisible;
            }
        }



        void OnTapped(object s)
        {
            changeWeightFrame();
        }

        /// <summary>
		/// Start the "reading" loop 
		/// </summary>
		/// <param name="name">Name of the paired bluetooth device (also a part of the name)</param>
		public void Start(string name, int sleepTime = 200, bool readAsCharArray = false)
        {

            Task.Run(async () => loop(name, sleepTime, readAsCharArray));
        }

        private async Task loop(string name, int sleepTime, bool readAsCharArray)
        {
            BluetoothDevice device = null;
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            BluetoothSocket BthSocket = null;

            _ct = new CancellationTokenSource();
            while (_ct.IsCancellationRequested == false)
            {

                try
                {
                    Thread.Sleep(sleepTime);

                    adapter = BluetoothAdapter.DefaultAdapter;

                    if (adapter == null)
                        System.Diagnostics.Debug.WriteLine("No Bluetooth adapter found.");
                    else
                        System.Diagnostics.Debug.WriteLine("Adapter found!!");

                    if (!adapter.IsEnabled)
                        System.Diagnostics.Debug.WriteLine("Bluetooth adapter is not enabled.");
                    else
                        System.Diagnostics.Debug.WriteLine("Adapter enabled!");

                    System.Diagnostics.Debug.WriteLine("Try to connect to " + name);

                    foreach (var bd in adapter.BondedDevices)
                    {
                        System.Diagnostics.Debug.WriteLine("Paired devices found: " + bd.Name.ToUpper());
                        if (bd.Name.ToUpper().IndexOf(name.ToUpper()) >= 0)
                        {

                            System.Diagnostics.Debug.WriteLine("Found " + bd.Name + ". Try to connect with it!");
                            device = bd;
                            break;
                        }
                    }


                    if (device == null)
                        System.Diagnostics.Debug.WriteLine("Named device not found.");
                    else
                    {
                        UUID uuid = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");
                        try
                        {
                            if ((int)Android.OS.Build.VERSION.SdkInt >= 10) // Gingerbread 2.3.3 2.3.4
                                BthSocket = device.CreateInsecureRfcommSocketToServiceRecord(uuid);

                            else
                                BthSocket = device.CreateRfcommSocketToServiceRecord(uuid);
                        }
                        catch (IOException e)
                        {
                            System.Diagnostics.Debug.WriteLine("Stacktrace!");
                            e.PrintStackTrace();
                        }


                        if (BthSocket != null)
                        {


                            //Task.Run ((Func<Task>)loop); /*) => {
                            try
                            {
                                await BthSocket.ConnectAsync();
                            }
                            catch (Exception e)
                            {
                                System.Diagnostics.Debug.WriteLine("'ConnectAsync()' - Connection failed with exception: " + e);
                                try
                                {
                                    BthSocket.Connect();
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine("Connect() - Connection failed with exception: " + ex);
                                }
                            }
                            if (BthSocket.IsConnected)
                            {
                                System.Diagnostics.Debug.WriteLine("Connected!");
                                var mReader = new InputStreamReader(BthSocket.InputStream);
                                var buffer = new BufferedReader(mReader);
                                //buffer.re
                                while (_ct.IsCancellationRequested == false)
                                {
                                    if (buffer.Ready())
                                    {
                                        //string data =  buffer
                                        //string data = buffer.

                                        //string data = await buffer.ReadLineAsync();
                                        char[] chr = new char[100];
                                        //await buffer.ReadAsync(chr);
                                        string data = "";
                                        if (readAsCharArray)
                                        {

                                            await buffer.ReadAsync(chr);
                                            foreach (char c in chr)
                                            {

                                                if (c == '\0')
                                                    break;
                                                data += c;
                                            }

                                        }
                                        else
                                            data = await buffer.ReadLineAsync();

                                        if (data.Length > 0)
                                        {
                                            System.Diagnostics.Debug.WriteLine("Letto: " + data);
                                            Xamarin.Forms.MessagingCenter.Send<App, string>((App)Xamarin.Forms.Application.Current, "Data", data);
                                            this.Weight = data + " kg";
                                        }
                                        else
                                            System.Diagnostics.Debug.WriteLine("No data");

                                    }
                                    else
                                        System.Diagnostics.Debug.WriteLine("No data to read");

                                    // A little stop to the uneverending thread...
                                    System.Threading.Thread.Sleep(sleepTime);
                                    if (!BthSocket.IsConnected)
                                    {
                                        System.Diagnostics.Debug.WriteLine("BthSocket.IsConnected = false, Throw exception");
                                        throw new Exception();
                                    }
                                }

                                System.Diagnostics.Debug.WriteLine("Exit the inner loop");

                            }
                            else
                                System.Diagnostics.Debug.WriteLine("BthSocket is not connected. BthSocket.IsConnected(): " + BthSocket.IsConnected);

                        }
                        else
                            System.Diagnostics.Debug.WriteLine("BthSocket = null");

                    }


                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Failed to establish connection and read data");
                }

                finally
                {
                    if (BthSocket != null)
                        BthSocket.Close();
                    device = null;
                    adapter = null;
                }
            }

            System.Diagnostics.Debug.WriteLine("Exit the external loop");
        }

        /// <summary>
		/// Cancel the Reading loop
		/// </summary>
		/// <returns><c>true</c> if this instance cancel ; otherwise, <c>false</c>.</returns>
		public void Cancel()
        {
            if (_ct != null)
            {
                System.Diagnostics.Debug.WriteLine("Send a cancel to task!");
                _ct.Cancel();
            }
        }

        private void changeWeightFrame()
        {
            double current_weight;
            string full_text;
            int new_cornerRadius;
            int new_fontSize;
            bool new_timeStampIsVisible;

            if (cornerRadius != 120)
            {
                Cancel();
                new_cornerRadius = 120;
                full_text = "Log new weight";
                new_fontSize = 25;
                new_timeStampIsVisible = false;
            }
            else
            {
                full_text = "-- kg";
                Start("MLT-BT05", 200, true);
                new_cornerRadius = 70;
                //current_weight = double.Parse(recieved_weight, System.Globalization.CultureInfo.InvariantCulture); ;
                //full_text = current_weight.ToString() + " kg";
                new_fontSize = 60;
                this.TimeStamp = DateTime.Now;
                new_timeStampIsVisible = true;

            }

            this.CornerRadius = new_cornerRadius;
            this.Weight = full_text;
            this.WeightLabel_FontSize = new_fontSize;
            this.TimeStamp_IsVisible = new_timeStampIsVisible;

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
