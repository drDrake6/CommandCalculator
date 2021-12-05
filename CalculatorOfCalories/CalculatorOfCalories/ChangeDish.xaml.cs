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

        public string GetSetName { get => name; set => name = value; }
        public double GetSetCalories { get => calories; set => calories = value; }
        public ComboBox GetSetDishes { get => Dishes; set => Dishes = value; }

        public event EventHandler<EventArgs> change;
        public event EventHandler<EventArgs> choose;

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

            choose.Invoke(this, new EventArgs());

            Name.Text = name;
            Calories.Text = calories.ToString();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                name = Name.Text;

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
