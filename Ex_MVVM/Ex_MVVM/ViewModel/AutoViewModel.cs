using Ex_MVVM.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Ex_MVVM.ViewModel
{
    public class AutoViewModel : INotifyPropertyChanged
    {

      
        private DelegateCommand openVindowCommand;

        public DelegateCommand OpenVindowCommand
        {
            get
            {
                return openVindowCommand ?? (openVindowCommand = new DelegateCommand(obj =>
               {
                   MainWindow mainVind = new MainWindow();
                   AddWindow vind = new AddWindow(mainVind);
                   vind.Show();

                   vind.DataContext = mainVind.DataContext;
               }));
            }
        }

        //AddWin start
        private Auto addingAuto = new Auto();
        public Auto AddingAuto
        {
            get => addingAuto;
            set
            {
                addingAuto = value;
                OnPropertyChanged();
            }
        }

        private DelegateCommand clearCommand;

        public DelegateCommand ClearCommand
        {
            get
            {
                return clearCommand ?? (clearCommand = new DelegateCommand(obj =>
                {
                    AddingAuto = new Auto();
                }));
            }
        }

        private DelegateCommand addCommand;

        public DelegateCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new DelegateCommand(obj =>
                {
                    Auto auto = addingAuto;
                    Autos.Insert(0, auto);
                    SelectedAuto = auto;
                    addingAuto = new Auto();
                }));
            }
        }

        private DelegateCommand addImg;

        public DelegateCommand AddImg
        {
            get
            {
                return addImg ?? (addImg = new DelegateCommand(obj =>
                {
                    var file = new OpenFileDialog();
                    file.Filter = "Image (*.jpg)|*.jpg| All files (*.*)|*.*";
                    file.Multiselect = false;

                    if (file.ShowDialog() == true)
                    {
                        try
                        {
                            AddingAuto.Img = file.FileName;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Can't open image");
                        }
                    }
                }));
            }
        }
        //AddWin end
        public event PropertyChangedEventHandler PropertyChanged;

        private string showText = "Hidden";

        public string ShowText
        {
            get => showText;
            set
            {
                showText = value;
                OnPropertyChanged();
            }
        }

        private Auto selectedAuto;

        public Auto SelectedAuto
        {
            get => selectedAuto;
            set
            {
                selectedAuto = value;
                if (selectedAuto != null)
                    ShowText = "Visible";
                else
                    ShowText = "Hidden";
                OnPropertyChanged();
            }
        }


        private DelegateCommand delCommand;

        public DelegateCommand DelCommand
        {
            get
            {
                return delCommand ?? (delCommand = new DelegateCommand(obj =>
                {
                    Autos.Remove(SelectedAuto);
                }));
            }
        }

        private DelegateCommand sortByMark;

        public DelegateCommand SortByMark
        {
            get
            {
                return sortByMark ?? (sortByMark = new DelegateCommand(obj =>
                {
                    for (int i = 0; i < Autos.Count; i++)
                    {
                        for (int j = i; j < Autos.Count; j++)
                        {
                            if (Autos[i].Mark.CompareTo(Autos[j].Mark) > 0)
                            {
                                (Autos[i], Autos[j]) = (Autos[j], Autos[i]);
                            }
                        }
                    }
                }));
            }
        }

        private DelegateCommand sortByModel;

        public DelegateCommand SortByModel
        {
            get
            {
                return sortByModel ?? (sortByModel = new DelegateCommand(obj =>
                {
                    for (int i = 0; i < Autos.Count; i++)
                    {
                        for (int j = i; j < Autos.Count; j++)
                        {
                            if (Autos[i].Model.CompareTo(Autos[j].Model) > 0)
                            {
                                (Autos[i], Autos[j]) = (Autos[j], Autos[i]);
                            }
                        }
                    }
                }));// orderBy doesn't work correctly or like i need, so i used default loops sort method
            }
        }

        private DelegateCommand saveToFile;

        public DelegateCommand SaveToFile
        {
            get
            {
                return saveToFile ?? (saveToFile = new DelegateCommand(obj =>
                {
                    using (var fs = new FileStream("AutoPark.txt", FileMode.Create, FileAccess.Write))
                    {
                        using (var sw = new StreamWriter(fs, Encoding.Default))
                        {
                            foreach (var item in Autos)
                            {
                                sw.WriteLine($"{item.Img} {item.Mark} {item.Model} {item.Number} {item.Volume} {item.OilWaste}");
                            }
                        }
                        fs.Close();
                    }
                   
                }));
            }
        }

        private bool darkTheme;
        public bool DarkTheme
        {
            get => darkTheme;
            set
            {
                darkTheme = value;
                OnThemeChanged();
            }
        }

        private Brush buttonColor;

        public Brush ButtonColor
        {
            get => buttonColor;
            set
            {
                buttonColor = value;
                OnPropertyChanged();
            }
        }

        private Brush menuColor;

        public Brush MenuColor
        {
            get => menuColor;
            set
            {
                menuColor = value;
                OnPropertyChanged();
            }
        }

        private Brush garageColor;

        public Brush GarageColor
        {
            get => garageColor;
            set
            {
                garageColor = value;
                OnPropertyChanged();
            }
        }

        private Brush textColor;

        public Brush TextColor
        {
            get => textColor;
            set
            {
                textColor = value;
                OnPropertyChanged();
            }
        }
        private void OnThemeChanged()
        {
            if (DarkTheme)
            {
                ButtonColor = Brushes.Black;
                MenuColor = Brushes.DarkMagenta;
                GarageColor = Brushes.LightGray;
                TextColor = Brushes.White;
            }
            else
            {
                ButtonColor = Brushes.AliceBlue;
                MenuColor = Brushes.Aqua;
                GarageColor = Brushes.White;
                TextColor = Brushes.Black;
            }
        }

        private DelegateCommand darkThemeCommand;

        public DelegateCommand DarkThemeCommand
        {
            get 
            {
                return darkThemeCommand ?? (darkThemeCommand = new DelegateCommand(obj =>
                {
                    DarkTheme = true;
                }));
            }
        }

        private DelegateCommand lightThemeCommand;

        public DelegateCommand LightThemeCommand
        {
            get
            {
                return lightThemeCommand ?? (lightThemeCommand = new DelegateCommand(obj =>
                {
                    DarkTheme = false;
                }));
            }
        }

        private DelegateCommand sortByNumber;

        public DelegateCommand SortByNumber
        {
            get
            {
                return sortByNumber ?? (sortByNumber = new DelegateCommand(obj =>
                {
                    for (int i = 0; i < Autos.Count; i++)
                    {
                        for (int j = i; j < Autos.Count; j++)
                        {
                            if (Autos[i].Number.CompareTo(Autos[j].Number) > 0)
                            {
                                (Autos[i], Autos[j]) = (Autos[j], Autos[i]);
                            }
                        }
                    }
                }));
            }
        }

        private DelegateCommand sortByVolume;

        public DelegateCommand SortByVolume
        {
            get
            {
                return sortByVolume ?? (sortByVolume = new DelegateCommand(obj =>
                {
                    double a, b;
                    for (int i = 0; i < Autos.Count; i++)
                    {
                        for (int j = i; j < Autos.Count; j++)
                        {

                            if (!double.TryParse(Autos[i].Volume, out a))
                                continue;
                            if (!double.TryParse(Autos[j].Volume, out b))
                                continue;

                            if (a > b)
                            {
                                (Autos[i], Autos[j]) = (Autos[j], Autos[i]);
                            }
                        }
                    }
                }));
            }
        }

        private DelegateCommand sortByOilWaste;

        public DelegateCommand SortByOilWaste
        {
            get
            {
                return sortByOilWaste ?? (sortByOilWaste = new DelegateCommand(obj =>
                {
                    double a, b;
                    for (int i = 0; i < Autos.Count; i++)
                    {
                        for (int j = i; j < Autos.Count; j++)
                        {

                            if (!double.TryParse(Autos[i].OilWaste, out a))
                                continue;
                            if (!double.TryParse(Autos[j].OilWaste, out b))
                                continue;

                            if (a > b)
                            {
                                (Autos[i], Autos[j]) = (Autos[j], Autos[i]);
                            }
                        }
                    }
                }));
            }
        }

        public ObservableCollection<Auto> Autos { get; set; }

        public AutoViewModel()
        {
            Autos = new ObservableCollection<Auto>()
            {
                new Auto {Img = "/../img/1.jpg", Mark = "Lamborgini", Model = "Aventador", Number = "777", Volume = "6.6", OilWaste = "19"},
                new Auto {Img = "/../img/2.jpg", Mark = "Chevrolet", Model = "Corvette", Number = "666", Volume = "7", OilWaste = "25"},
                new Auto {Img = "/../img/3.jpg", Mark = "Porsche", Model = "Panamera", Number = "AA9999AA", Volume = "4", OilWaste = "9"},
                new Auto {Img = "/../img/4.jpg", Mark = "Volkswagen", Model = "Transporter", Number = "BB6666BX", Volume = "2", OilWaste = "10.4"},
                new Auto {Img = "/../img/5.jpg", Mark = "Ferrari", Model = "Superfast", Number = "GGWP", Volume = "6.5", OilWaste = "15"}
            };
            DarkTheme = false;
        }
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
