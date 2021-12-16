using System;
using System.Collections;
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
        private string nameOfProduct;
        private double caloriesOfProduct;

        private SortedList<string, double> productsInDish = new SortedList<string, double>();

        public string GetSetName { get => name; set => name = value; }
        public double GetSetCalories { get => calories; set => calories = value; }
        public double GetSetMass { get => mass; set => mass = value; }
        public string GetSetNameOfProduct { get => nameOfProduct; set => nameOfProduct = value; }
        public ListBox GetSetProducts { get => Products; set => Products = value; }
        public SortedList<string, double> GetSetProductsInDish { get => productsInDish; set => productsInDish = value; }
        public double GetSetCaloriesOfProduct { get => caloriesOfProduct; set => caloriesOfProduct = value; }

        public event EventHandler<EventArgs> add;
        public event EventHandler<EventArgs> addProduct;
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

            Name.Text = null;
            Calories.Text = null;

            Add.IsEnabled = false;
            Products.SelectedIndex = 0;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Name.IsEnabled = true;
            AddProduct.IsEnabled = true;
            MassOfProduct.IsEnabled = true;

            choose.Invoke(this, new EventArgs());

            MassOfProduct.Text = mass.ToString();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                name = Name.Text;

                try
                {
                    calories = Convert.ToDouble(Calories.Text);
                }
                catch (Exception)
                {
                    calories = double.Parse(Calories.Text, NumberFormatInfo.InvariantInfo);
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

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Add.IsEnabled = true;

            try
            {
                mass = Convert.ToDouble(MassOfProduct.Text);
            }
            catch (Exception)
            {
                mass = double.Parse(MassOfProduct.Text, NumberFormatInfo.InvariantInfo);
            }

            try
            { 
                string product = (string)Products.SelectedItem;
                nameOfProduct = product;

                productsInDish.Add(product, mass);
                addProduct.Invoke(this, new EventArgs()); 
                Calories.Text = calories.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("The product already exists", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckTextMass(object sender, TextChangedEventArgs e)
        {
            if (MassOfProduct.Text.Length > 0)
            {
                if (Regular.CheckNumeric(MassOfProduct.Text))
                    AddProduct.IsEnabled = true;
                else
                    AddProduct.IsEnabled = false;
            }
            else
                AddProduct.IsEnabled = false;
        }
    }
}
