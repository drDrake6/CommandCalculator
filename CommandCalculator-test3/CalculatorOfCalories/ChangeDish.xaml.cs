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
        private string nameOfNewProduct;
        private double calories;
        private double massOfProduct;
        private double massOfNewProduct;

        public string GetSetNameOfNewProduct { get => nameOfNewProduct; set => nameOfNewProduct = value; }
        public string GetSetName { get => name; set => name = value; }
        public double GetSetCalories { get => calories; set => calories = value; }
        public double GetSetMassOfProduct { get => massOfProduct; set => massOfProduct = value; }
        public double GetSetMassOfNewProduct { get => massOfNewProduct; set => massOfNewProduct = value; }
        public ComboBox GetSetDishes { get => Dishes; set => Dishes = value; }
        public ComboBox GetSetProducts { get => Products; set => Products = value; }
        public ComboBox GetSetNewProducts { get => newProducts; set => newProducts = value; }

        public event EventHandler<EventArgs> change;
        public event EventHandler<EventArgs> choose;
        public event EventHandler<EventArgs> chooseProduct;
        public event EventHandler<EventArgs> deleteProduct;
        public event EventHandler<EventArgs> selectNewProduct;
        public event EventHandler<EventArgs> addNewProduct;

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
            Products.IsEnabled = true;
            newProducts.IsEnabled = true;

            choose.Invoke(this, new EventArgs());

            Name.Text = name;
            Calories.Text = calories.ToString().Replace(',','.');
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mass.IsEnabled = true;
            Delete.IsEnabled = true;

            chooseProduct.Invoke(this, new EventArgs());

            Mass.Text = massOfProduct.ToString().Replace(',', '.');
        }

        private void newProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Add.IsEnabled = true;
            productMass.IsEnabled = true;

            selectNewProduct.Invoke(this, new EventArgs());

            productMass.Text = massOfNewProduct.ToString().Replace(',', '.');
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                nameOfNewProduct = newProducts.Text;

                try
                {
                    massOfNewProduct = Convert.ToDouble(productMass.Text);
                }
                catch (Exception)
                {
                    massOfNewProduct = double.Parse(productMass.Text, NumberFormatInfo.InvariantInfo);
                }

                addNewProduct.Invoke(this, new EventArgs());
                Calories.Text = calories.ToString().Replace(',', '.');
                MessageBox.Show("Product was added", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                        massOfProduct = Convert.ToDouble(Mass.Text);
                    }
                    catch (Exception)
                    {
                        massOfProduct = double.Parse(Mass.Text, NumberFormatInfo.InvariantInfo);
                    }

                    change.Invoke(this, new EventArgs());
                    MessageBox.Show("Dish was edit", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to delete this dish", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    deleteProduct.Invoke(this, new EventArgs());
                    Calories.Text = calories.ToString().Replace(',', '.');
                    MessageBox.Show("Product was delete from dish", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CheckText(object sender, TextChangedEventArgs e)
        {
            if (Name.Text.Length > 0 && Mass.Text.Length > 0)
            {
                if (Regular.CheckName(Name.Text)
                    && Regular.CheckNumeric(Mass.Text))
                    Change.IsEnabled = true;
                else
                    Change.IsEnabled = false;
            }
            else
                Add.IsEnabled = false;
        }

        private void CheckTextForNewProduct(object sender, TextChangedEventArgs e)
        {
            if (productMass.Text.Length > 0)
            {
                if (Regular.CheckNumeric(productMass.Text))
                    Add.IsEnabled = true;
                else
                    Add.IsEnabled = false;
            }
            else
                Add.IsEnabled = false;
        }
    }
}
