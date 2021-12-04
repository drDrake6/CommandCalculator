using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CalculatorOfCalories.Logic;

namespace CalculatorOfCalories
{
    internal class Presenter
    {
        private readonly Model model = new Model();
        private MainWindow mainWindow = null;

        public Presenter(MainWindow mainWindow)
        {
            mainWindow = mainWindow;

            mainWindow.EventAddProduct += new EventHandler<EventArgs>(AddProductFunction);
            mainWindow.EventChangeProduct += new EventHandler<EventArgs>(ChangeProductFunction);
            mainWindow.EventDeleteProduct += new EventHandler<EventArgs>(DeleteProductFunction);
        }

        private void AddProduct(string name, double calories, double mass)
        {
            Product product = new Product();
            product.Name = name;
            product.CaloriesPer100Gramms = calories;
            product.MassInKilo = mass;

            model.AllProducts.Add(product);
        }

        #region AddProduct
        private void AddProductFunction(object? sender, EventArgs e)
        {
            AddProduct addProduct = sender as AddProduct;
            addProduct.add += new EventHandler<EventArgs>(AddProductImBase);
        }

        private void AddProductImBase(object? sender, EventArgs e)
        {
            AddProduct addProduct = sender as AddProduct;

            string name = addProduct.GetSetName;
            double calories = addProduct.GetSetClories;
            double mass = addProduct.GetSetMass;

            AddProduct(name, calories, mass);
        }
        #endregion

        #region ChangeProduct
        private void ChangeProductFunction(object? sender, EventArgs e)
        {
            if (model.AllProducts.GetListOfProducts().Count < 1)
                throw new ApplicationException("No products");

            ChangeProduct changeProduct = sender as ChangeProduct;
            changeProduct.change += new EventHandler<EventArgs>(ChangeProductImBase);
            changeProduct.choose += new EventHandler<EventArgs>(ChooseProduct);

            foreach (Product product in model.AllProducts.GetListOfProducts())
                changeProduct.GetSetProducts.Items.Add(product.Name);
        }

        private void ChooseProduct(object? sender, EventArgs e)
        {
            ChangeProduct changeProduct = sender as ChangeProduct;

            int index = changeProduct.GetSetProducts.SelectedIndex;

            if (index >= 0)
            {
                string name = model.AllProducts[index].Name;
                double calories = model.AllProducts[index].CaloriesPer100Gramms;
                double mass = model.AllProducts[index].MassInKilo;

                changeProduct.GetSetName = name;
                changeProduct.GetSetCalories = calories;
                changeProduct.GetSetMass = mass;
            }
        }

        private void ChangeProductImBase(object? sender, EventArgs e)
        {
            ChangeProduct changeProduct = sender as ChangeProduct;

            string name = changeProduct.GetSetName;
            double calories = changeProduct.GetSetCalories;
            double mass = changeProduct.GetSetMass;

            int index = changeProduct.GetSetProducts.SelectedIndex;
            model.AllProducts.DeleteByIndex(index);

            AddProduct(name, calories, mass);

            changeProduct.GetSetProducts.Items.Clear();
            foreach (Product product in model.AllProducts.GetListOfProducts())
                changeProduct.GetSetProducts.Items.Add(product.Name);

            changeProduct.GetSetProducts.SelectedIndex = 
                model.AllProducts.GetListOfProducts().Count - 1;
        }
        #endregion

        #region DeleteProduct
        private void DeleteProductFunction(object? sender, EventArgs e)
        {
            if (model.AllProducts.GetListOfProducts().Count < 1)
                throw new ApplicationException("No products");

            DaleteProduct deleteProduct = sender as DaleteProduct;
            deleteProduct.delete += new EventHandler<EventArgs>(DeleteProductImBase);

            foreach (Product product in model.AllProducts.GetListOfProducts())
                deleteProduct.GetSetProducts.Items.Add(product.Name);
        }

        private void DeleteProductImBase(object? sender, EventArgs e)
        {
            DaleteProduct deleteProduct = sender as DaleteProduct;

            int index = deleteProduct.GetSetProducts.SelectedIndex;
            model.AllProducts.DeleteByIndex(index);

            deleteProduct.GetSetProducts.Items.Clear();
            foreach (Product product in model.AllProducts.GetListOfProducts())
                deleteProduct.GetSetProducts.Items.Add(product.Name);
        }
        #endregion
    }
}
