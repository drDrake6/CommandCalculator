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
    /// Interaction logic for AddDish.xaml
    /// </summary>
    public partial class AddDish : Window
    {
        private string name;
        private string calories;

        public string GetSetName { get => name; set => name = value; }
        public string GetSetCalories { get => calories; set => calories = value; }
        public ListBox GetSetProducts { get => Products; set => Products = value; }

        public event EventHandler<EventArgs> add;
        public event EventHandler<EventArgs> change;

        public AddDish(ResourceDictionary resourceDictionary)
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
            Add.IsEnabled = true;

            // событие выбора
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                name = Name.Text;
                calories = Calories.Text;

                //событие добавления
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
