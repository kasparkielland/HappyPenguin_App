using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace HappyPenguin.ViewModel
{
    public class WeightViewModel : INotifyPropertyChanged
    {
        DateTime timeStamp;
        int cornerRadius;
        string weight;
        int weightLabel_FontSize;
        bool timeStamp_IsVisible;

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

        private void changeWeightFrame()
        {
            double current_weight;
            string full_weight;
            int new_cornerRadius;
            int new_fontSize;
            bool new_timeStampIsVisible;

            if (cornerRadius != 120)
            {
                new_cornerRadius = 120;
                full_weight = "Log new weight";
                new_fontSize = 25;
                new_timeStampIsVisible = false;
            }
            else
            {
                new_cornerRadius = 70;
                current_weight = 50.5;
                full_weight = current_weight.ToString() + " kg";
                new_fontSize = 60;
                this.TimeStamp = DateTime.Now;
                new_timeStampIsVisible = true;

            }

            this.CornerRadius = new_cornerRadius;
            this.Weight = full_weight;
            this.WeightLabel_FontSize = new_fontSize;
            this.TimeStamp_IsVisible = new_timeStampIsVisible;

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
