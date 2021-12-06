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
    /// Interaction logic for ChangeDish.xaml
    /// </summary>
    public partial class ChangeDish : Window
    {
        private string name;
        private double calories;
        private double massOfProduct;

        public string GetSetName { get => name; set => name = value; }
        public double GetSetCalories { get => calories; set => calories = value; }
        public double GetSetMassOfProduct { get => massOfProduct; set => massOfProduct = value; }
        public ComboBox GetSetDishes { get => Dishes; set => Dishes = value; }

        public event EventHandler<EventArgs> change;
        public event EventHandler<EventArgs> choose;
        public event EventHandler<EventArgs> chooseProduct;

        public ChangeDish(ResourceDictionary resourceDictionary)
        {
            InitializeComponent();
            Resources = resourceDictionary;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Dishes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Name.IsEnabled = true;
            Change.IsEnabled = true;
            Products.IsEnabled = true;
            Mass.IsEnabled = true;

            choose.Invoke(this, new EventArgs());

            Name.Text = name;
            Calories.Text = calories.ToString();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                name = Name.Text;

                try
                {
                    massOfProduct = Convert.ToDouble(Mass.Text);
                }
                catch (Exception)
                {
                    massOfProduct = double.Parse(Mass.Text, NumberFormatInfo.InvariantInfo);
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
}
