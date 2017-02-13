using System;
using System.ComponentModel;

namespace P3_Rellotge
{
    [Serializable()]
    class Alarm : INotifyPropertyChanged
    {
        Boolean actived = false;
        int hour = 0;
        int minutes = 0;
        int seconds = 0;


        public bool Actived
        {
            get
            {
                return actived;
            }

            set
            {
                actived = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Actived"));
            }
        }

        public int Hour
        {
            get
            {
                return hour;
            }

            set
            {
                hour = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Hour"));
            }
        }

        public int Minutes
        {
            get
            {
                return minutes;
            }

            set
            {
                minutes = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Minutes"));
            }
        }

        public int Seconds
        {
            get
            {
                return seconds;
            }

            set
            {
                seconds = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Seconds"));
            }
        }


        public Alarm(int Hour, int Minutes, int Seconds, Boolean Actived)
        {
            this.hour = Hour;
            this.minutes = Minutes;
            this.seconds = Seconds;
            this.actived = Actived;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

    }
}
