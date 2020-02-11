using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace HappyPenguin.ViewModel
{
    public class BluetoothViewModel : INotifyPropertyChanged
    {
        string status;

        ICommand tapCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand TapCommand
        {
            get { return tapCommand; }
        }


        public BluetoothViewModel()
        {
            // configure the TapCommand with a method
            tapCommand = new Command(OnTapped);

        }

        public string Status
        {
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
            get
            {
                return status;
            }
        }


        void OnTapped()
        {
            this.Status = "Button pressed";

        }



        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
