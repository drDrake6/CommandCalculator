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
    /// Interaction logic for ChangeProduct.xaml
    /// </summary>
    public partial class ChangeProduct : Window
    {
        private string name;
        private double calories = 0;
        private double mass = 0;

        public event EventHandler<EventArgs> change;
        public event EventHandler<EventArgs> choose;

        public string GetSetName { get => name; set => name = value; }
        public double GetSetCalories { get => calories; set => calories = value; }
        public double GetSetMass { get => mass; set => mass = value; }
        public ComboBox GetSetProducts { get => Products; set => Products = value; }

        public ChangeProduct(ResourceDictionary resourceDictionary)
        {
            InitializeComponent();
            Resources = resourceDictionary;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Name.IsEnabled = true;
            Calories.IsEnabled = true;
            Mass.IsEnabled = true;

            choose.Invoke(this, new EventArgs());

            Name.Text = name;
            Calories.Text = calories.ToString();
            Mass.Text = mass.ToString();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to change this product", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
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

                    change.Invoke(this, new EventArgs());
                    MessageBox.Show("Product was edit", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CheckText(object sender, TextChangedEventArgs e)
        {
            if (Name.Text.Length > 0 && Calories.Text.Length > 0 && Mass.Text.Length > 0)
            {
                if (Regular.CheckName(Name.Text)
                    && Regular.CheckNumeric(Calories.Text)
                    && Regular.CheckNumeric(Mass.Text))
                    Change.IsEnabled = true;
                else
                    Change.IsEnabled = false;
            }
        }
    }
}
