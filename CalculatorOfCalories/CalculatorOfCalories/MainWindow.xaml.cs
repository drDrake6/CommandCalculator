using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorOfCalories
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum Mobility { Low = 1, Middle = 2, High = 3 };
        enum Criteries { WeightReduction = 1, WeightGain = 2, WeightRetention = 3 };

        private ResourceDictionary light = new ResourceDictionary();
        private ResourceDictionary dark = new ResourceDictionary();

        private uint mobility;
        private string criteries;
        private double result;

        public event EventHandler<EventArgs> count;

        public uint GetSetMobility { get => mobility; set => mobility = value; }
        public string GetSetCriteries { get => criteries; set => criteries = value; }
        public double SetSetResult { get => result; set => result = value; }

        public MainWindow()
        {
            InitializeComponent();

            light.Source = new Uri("Resources/LightTheme.xaml", UriKind.Relative);
            dark.Source = new Uri("Resources/DarkTheme.xaml", UriKind.Relative);
        }

        private void Mobility_Checked(object sender, RoutedEventArgs e)
        {
            if (LowMobility.IsChecked == true)
                mobility = (uint)Mobility.Low;
            else if (MiddleMobility.IsChecked == true)
                mobility = (uint)Mobility.Middle;
            else if (HighMobility.IsChecked == true)
                mobility = (uint)Mobility.High;
        }

        private void Criteries_Checked(object sender, RoutedEventArgs e)
        {
            if (WeightReduction.IsChecked == true)
                mobility = (uint)Criteries.WeightReduction;
            else if (WeightGain.IsChecked == true)
                mobility = (uint)Criteries.WeightGain;
            else if (WeightRetention.IsChecked == true)
                mobility = (uint)Criteries.WeightRetention;
        }

        private void LightTheme_Checked(object sender, RoutedEventArgs e)
        {
            Resources.MergedDictionaries[0] = light;
        }

        private void DarkTheme_Checked(object sender, RoutedEventArgs e)
        {
            Resources.MergedDictionaries[0] = dark;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Effect = new BlurEffect();

            AddProduct addProduct = new AddProduct(Background);
            addProduct.Owner = this;
            addProduct.ShowDialog();
        }

        private void Count_Click(object sender, RoutedEventArgs e)
        {
            //событе подсчёта
        }
    }
}
