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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        private string name;
        private double clories = 0;
        private double mass = 0;

        public event EventHandler<EventArgs> add;

        public string GetSetName { get => name; set => name = value; }
        public double GetSetClories { get => clories; set => clories = value; }
        public double GetSetMass { get => mass; set => mass = value; }

        public AddProduct(ResourceDictionary resourceDictionary)
        {
            InitializeComponent();
            Resources = resourceDictionary;
        }

        private void Clear()
        {
            name = "";
            clories = 0;
            mass = 0;

            Name.Text = null;
            Calories.Text = null;
            Mass.Text = null;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Owner.Effect = null;
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                name = Name.Text;

                try
                {
                    clories = Convert.ToDouble(Calories.Text);
                    mass = Convert.ToDouble(Mass.Text);
                }
                catch (Exception)
                {
                    clories = double.Parse(Calories.Text, NumberFormatInfo.InvariantInfo);
                    mass = double.Parse(Mass.Text, NumberFormatInfo.InvariantInfo);
                }

                add.Invoke(this, new EventArgs());

                MessageBox.Show("Product was added", "", MessageBoxButton.OK, MessageBoxImage.Information);
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckText(object sender, TextChangedEventArgs e)
        {
            if (Name.Text.Length > 0 && Calories.Text.Length > 0 && Mass.Text.Length > 0)
            {
                if (Regular.CheckName(Name.Text)
                    && Regular.CheckNumeric(Calories.Text)
                    && Regular.CheckNumeric(Mass.Text))
                    Add.IsEnabled = true;
                else
                    Add.IsEnabled = false;
            }
            else
                Add.IsEnabled = false;
        }
    }
}
