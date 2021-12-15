﻿using System;
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
    /// Interaction logic for DeleteDish.xaml
    /// </summary>
    public partial class DeleteDish : Window
    {
        private string name;

        public event EventHandler<EventArgs> delete;

        public string GetSetName { get => name; set => name = value; }
        public ComboBox GetSetDishes { get => Dishes; set => Dishes = value; }

        public DeleteDish(ResourceDictionary resourceDictionary)
        {
            InitializeComponent();
            Resources = resourceDictionary;
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Delete.IsEnabled = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to delete this product", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    name = Dishes.Text;

                    delete.Invoke(this, new EventArgs());
                    MessageBox.Show("Dish was deleted", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
