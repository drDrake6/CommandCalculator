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
    /// Interaction logic for ChangeProduct.xaml
    /// </summary>
    public partial class ChangeProduct : Window
    {
        private string name;
        private double clories;
        private double mass;

        public event EventHandler<EventArgs> choose;
        public event EventHandler<EventArgs> change;

        public string GetSetName { get => name; set => name = value; }
        public double GetSetClories { get => clories; set => clories = value; }
        public double GetSetMass { get => mass; set => mass = value; }
        public ComboBox GetSetProducts { get => Products; set => Products = value; }

        public ChangeProduct(ResourceDictionary resourceDictionary)
        {
            InitializeComponent();
            Resources = resourceDictionary;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Owner.Effect = null;
            Close();
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Name.IsEnabled = true;
            Calories.IsEnabled = true;
            Mass.IsEnabled = true;
            Change.IsEnabled = true;

            //событие выбора
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                name = Name.Text;
                clories = Convert.ToDouble(Calories.Text);
                mass = Convert.ToDouble(Mass.Text);

                //событие
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
