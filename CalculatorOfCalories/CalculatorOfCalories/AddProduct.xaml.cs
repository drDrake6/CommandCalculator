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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        private string name;
        private double clories;
        private double mass;

        event EventHandler<EventArgs> add;

        public string GetSetName { get => name; set => name = value; }
        public double GetSetClories { get => clories; set => clories = value; }
        public double GetSetMass { get => mass; set => mass = value; }

        public AddProduct(Brush brush)
        {
            InitializeComponent();
            Background = brush;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Owner.Effect = null;
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            name = Name.Text;
            clories = Convert.ToDouble(Calories.Text);
            mass = Convert.ToDouble(Mass.Text);

            //событие добавления
        }
    }
}
