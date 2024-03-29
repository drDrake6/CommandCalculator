﻿using System;
using System.Collections;
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
        enum Mobility { Low = 1, Middle = 2, High = 3 };
        enum Sex { Male = 1, Famale = 2 };

        private ResourceDictionary light = new ResourceDictionary();
        private ResourceDictionary dark = new ResourceDictionary();

        private uint mobility;
        private double result;
        private uint sex;
        private double weight;
        private double height;
        private double age;

        public event EventHandler<EventArgs> count;

        public event EventHandler<EventArgs> EventAddProduct;
        public event EventHandler<EventArgs> EventChangeProduct;
        public event EventHandler<EventArgs> EventDeleteProduct;

        public event EventHandler<EventArgs> EventAddDish;
        public event EventHandler<EventArgs> EventChangeDish;
        public event EventHandler<EventArgs> EventDeleteDish;

        public uint GetSetMobility { get => mobility; set => mobility = value; }
        public double SetSetResult { get => result; set => result = value; }
        public uint GetSetSex { get => sex; set => sex = value; }
        public double GetSetWeight { get => weight; set => weight = value; }
        public double GetSetHeight { get => height; set => height = value; }
        public double GetSetAge { get => age; set => age = value; }
        public ListBox GetSetDishes { get => Dishes; set => Dishes = value; }

        public MainWindow()
        {
            InitializeComponent();

            light.Source = new Uri("Resources/LightTheme.xaml", UriKind.Relative);
            dark.Source = new Uri("Resources/DarkTheme.xaml", UriKind.Relative);
        }

        private void Mobility_Checked(object sender, RoutedEventArgs e)
        {
            if (LowMobility.IsChecked == true)
                mobility = (uint)Mobility.Low;
            else if (MiddleMobility.IsChecked == true)
                mobility = (uint)Mobility.Middle;
            else if (HighMobility.IsChecked == true)
                mobility = (uint)Mobility.High;
        }

        private void Sex_Checked(object sender, RoutedEventArgs e)
        {
            if (Male.IsChecked == true)
                mobility = (uint)Sex.Male;
            else if (Famale.IsChecked == true)
                mobility = (uint)Sex.Famale;
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
            // вызов события добавления продукта
            addProduct.Owner = this;
            addProduct.ShowDialog();
        }

        private void ChangeProduct_Click(object sender, RoutedEventArgs e)
        {
            Effect = new BlurEffect();

            ChangeProduct addProduct = new ChangeProduct(Resources.MergedDictionaries[0]);
            // вызов события изменения пробукта
            addProduct.Owner = this;
            addProduct.ShowDialog();
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Effect = new BlurEffect();

            DaleteProduct addProduct = new DaleteProduct(Resources.MergedDictionaries[0]);
            // вызов события удаления пробукта
            addProduct.Owner = this;
            addProduct.ShowDialog();
        }

        private void AddDish_Click(object sender, RoutedEventArgs e)
        {
            Effect = new BlurEffect();

            AddDish addProduct = new AddDish(Resources.MergedDictionaries[0]);
            // вызов события добавления блюда
            addProduct.Owner = this;
            addProduct.ShowDialog();
        }

        private void ChangeDish_Click(object sender, RoutedEventArgs e)
        {
            Effect = new BlurEffect();

            ChangeDish addProduct = new ChangeDish(Resources.MergedDictionaries[0]);
            // вызов события добавления блюда
            addProduct.Owner = this;
            addProduct.ShowDialog();
        }

        private void DeleteDish_Click(object sender, RoutedEventArgs e)
        {
            Effect = new BlurEffect();

            DeleteDish addProduct = new DeleteDish(Resources.MergedDictionaries[0]);
            // вызов события добавления блюда
            addProduct.Owner = this;
            addProduct.ShowDialog();
        }

        private void Count_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                weight = Convert.ToDouble(Weight.Text);
                height = Convert.ToDouble(Height.Text);
                age = Convert.ToDouble(Age.Text);

                // это должно быть в презентере
                //List<string> list = new List<string>();
                //IList dishes = Dishes.SelectedItems;
                //foreach (string dish in dishes)
                //    list.Add(dish);

                //событе подсчёта
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
