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
        private double calories;

        public double GetSetCalories { get => calories; set => calories = value; }
        public ComboBox GetSetProducts { get => Dishes; set => Dishes = value; }

        public event EventHandler<EventArgs> add;
        public event EventHandler<EventArgs> choose;

        public ChangeDish(ResourceDictionary resourceDictionary)
        {
            InitializeComponent();
            Resources = resourceDictionary;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Owner.Effect = null;
            Close();
        }

        private void Dishes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Calories.IsEnabled = true;
            Change.IsEnabled = true;

            // событие выбора
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                calories = Convert.ToDouble(Calories.Text);

                // событие измения
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
