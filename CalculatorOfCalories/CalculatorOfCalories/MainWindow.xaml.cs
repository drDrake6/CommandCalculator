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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorOfCalories
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum Mobility { Minimal = 0, Low = 1, Middle = 2, High = 3, Extrime = 4 };
        private enum Sex { Male = 1, Famale = 0 };

        private ResourceDictionary light = new ResourceDictionary();
        private ResourceDictionary dark = new ResourceDictionary();

        private uint mobility;
        private double result;
        private uint sex;
        private double weight;
        private double height;
        private int age;

        public event EventHandler<EventArgs> count;

        public event EventHandler<EventArgs> EventAddProduct;
        public event EventHandler<EventArgs> EventChangeProduct;
        public event EventHandler<EventArgs> EventDeleteProduct;

        public event EventHandler<EventArgs> EventAddDish;
        public event EventHandler<EventArgs> EventChangeDish;
        public event EventHandler<EventArgs> EventDeleteDish;

        public uint GetSetMobility { get => mobility; set => mobility = value; }
        public double GetSetResult { get => result; set => result = value; }
        public uint GetSetSex { get => sex; set => sex = value; }
        public double GetSetWeight { get => weight; set => weight = value; }
        public double GetSetHeight { get => height; set => height = value; }
        public int GetSetAge { get => age; set => age = value; }
        public ListBox GetSetDishes { get => Dishes; set => Dishes = value; }

        public MainWindow()
        {
            InitializeComponent();

            light.Source = new Uri("Resources/LightTheme.xaml", UriKind.Relative);
            dark.Source = new Uri("Resources/DarkTheme.xaml", UriKind.Relative);

            Presenter presenter = new Presenter(this);
        }

        private void Mobility_Checked(object sender, RoutedEventArgs e)
        {
            if (MinimalMobility.IsChecked == true)
                mobility = (uint)Mobility.Minimal;
            else if (LowMobility.IsChecked == true)
                mobility = (uint)Mobility.Low;
            else if (MiddleMobility.IsChecked == true)
                mobility = (uint)Mobility.Middle;
            else if (HighMobility.IsChecked == true)
                mobility = (uint)Mobility.High;
            else if (ExtrimMobility.IsChecked == true)
                mobility = (uint)Mobility.Extrime;
        }

        private void Sex_Checked(object sender, RoutedEventArgs e)
        {
            if (Male.IsChecked == true)
                sex = (uint)Sex.Male;
            else if (Famale.IsChecked == true)
                sex = (uint)Sex.Famale;
        }

        private void LightTheme_Checked(object sender, RoutedEventArgs e)
        {
            Resources.MergedDictionaries[0] = light;
        }

        private void DarkTheme_Checked(object sender, RoutedEventArgs e)
        {
            Resources.MergedDictionaries[0] = dark;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Effect = new BlurEffect();

            AddProduct addProduct = new AddProduct(Resources.MergedDictionaries[0]);
            EventAddProduct.Invoke(addProduct, new EventArgs());
            addProduct.Owner = this;
            addProduct.ShowDialog();
        }

        private void ChangeProduct_Click(object sender, RoutedEventArgs e)
        {
            Effect = new BlurEffect();

            ChangeProduct changeProduct = new ChangeProduct(Resources.MergedDictionaries[0]);

            try
            {
                EventChangeProduct.Invoke(changeProduct, new EventArgs());
                changeProduct.Owner = this;
                changeProduct.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Effect = null;
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Effect = new BlurEffect();

            DaleteProduct deleteProduct = new DaleteProduct(Resources.MergedDictionaries[0]);

            try
            {
                EventDeleteProduct.Invoke(deleteProduct, new EventArgs());
                deleteProduct.Owner = this;
                deleteProduct.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Effect = null;
        }

        private void AddDish_Click(object sender, RoutedEventArgs e)
        {
            Effect = new BlurEffect();

            AddDish addDish = new AddDish(Resources.MergedDictionaries[0]);
            
            try
            {
                EventAddDish.Invoke(addDish, new EventArgs());
                addDish.Owner = this;
                addDish.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Effect = null;
        }

        private void ChangeDish_Click(object sender, RoutedEventArgs e)
        {
            Effect = new BlurEffect();

            ChangeDish changeDish = new ChangeDish(Resources.MergedDictionaries[0]);

            try
            {
                EventChangeDish.Invoke(changeDish, new EventArgs());
                changeDish.Owner = this;
                changeDish.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Effect = null;
        }

        private void DeleteDish_Click(object sender, RoutedEventArgs e)
        {
            Effect = new BlurEffect();

            DeleteDish deleteDish = new DeleteDish(Resources.MergedDictionaries[0]);
            
            try
            {
                EventDeleteDish.Invoke(deleteDish, new EventArgs());
                deleteDish.Owner = this;
                deleteDish.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Effect = null;
        }

        private void Count_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                age = Convert.ToInt32(Age.Text);

                try
                {
                    weight = Convert.ToDouble(Weight.Text);
                    height = Convert.ToDouble(Height.Text);
                }
                catch (Exception)
                {
                    weight = double.Parse(Weight.Text, NumberFormatInfo.InvariantInfo);
                    height = double.Parse(Height.Text, NumberFormatInfo.InvariantInfo);
                }

                count.Invoke(sender, new EventArgs());
                Result.Text = result.ToString();

                MessageBox.Show("Calculate was succesfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
