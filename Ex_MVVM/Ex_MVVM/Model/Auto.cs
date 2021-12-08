using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Ex_MVVM.Model
{
    public class Auto : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string img;
        private string mark;
        private string model;
        private string number;
        private string volume;
        private string oilWaste;

        public string Img
        {
            get => img;
            set
            {
                img = value;
                OnPropertyChanged();
            }
        }

        public string Mark
        {
            get => mark;
            set
            {
                mark = value;
                OnPropertyChanged();
            }
        }
        public string Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged();
            }
        }

        public string Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }

        public string OilWaste
        {
            get => oilWaste;
            set
            {
                oilWaste = value;
                OnPropertyChanged();
            }
        }

        public string Volume
        {
            get => volume;
            set
            {
                volume = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string prop="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
