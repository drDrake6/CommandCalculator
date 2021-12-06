using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CalculatorOfCalories
{
    /// <summary>
    /// Interaction logic for AddDish.xaml
    /// </summary>
    public partial class AddDish : Window
    {
        private string name;
        private double calories = 0;
        private double mass = 0;

        public string GetSetName { get => name; set => name = value; }
        public double GetSetCalories { get => calories; set => calories = value; }
        public double GetSetMass { get => mass; set => mass = value; }
        public ListBox GetSetProducts { get => Products; set => Products = value; }

        public event EventHandler<EventArgs> add;
        public event EventHandler<EventArgs> choose;

        public AddDish(ResourceDictionary resourceDictionary)
        {
            InitializeComponent();
            Resources = resourceDictionary;
        }

        private void Clear()
        {
            name = "";
            calories = 0;
            mass = 0;

            Name.Text = null;
            Calories.Text = null;
            Mass.Text = null;

            Products.SelectedIndex = -1;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Name.IsEnabled = true;
            Mass.IsEnabled = true;
            Add.IsEnabled = true;

            choose.Invoke(this, new EventArgs());

            Calories.Text = calories.ToString();
            Mass.Text = mass.ToString();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                name = Name.Text;

                try
                {
                    calories = Convert.ToDouble(Calories.Text);
                    mass = Convert.ToDouble(Mass.Text);
                }
                catch (Exception)
                {
                    calories = double.Parse(Calories.Text, NumberFormatInfo.InvariantInfo);
                    mass = double.Parse(Mass.Text, NumberFormatInfo.InvariantInfo);
                }

                add.Invoke(this, new EventArgs());

                MessageBox.Show("Dish was edded", "", MessageBoxButton.OK, MessageBoxImage.Information);
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
