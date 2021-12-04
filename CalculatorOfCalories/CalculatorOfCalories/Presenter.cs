using System;
using System.Collections;
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
        private MainWindow? mainWindow = null;

        public Presenter(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            mainWindow.EventAddProduct += new EventHandler<EventArgs>(AddProductFunction);
            mainWindow.EventChangeProduct += new EventHandler<EventArgs>(ChangeProductFunction);
            mainWindow.EventDeleteProduct += new EventHandler<EventArgs>(DeleteProductFunction);
            mainWindow.EventAddDish += new EventHandler<EventArgs>(AddDishFunction);
        }

        private void AddProduct(string name, double calories, double mass)
        {
            Product product = new Product();
            product.Name = name;
            product.CaloriesPer100Gramms = calories;
            product.MassInKilo = mass;

            model.AllProducts.Add(product);
        }

        private void AddDish(string name, List<Product> products)
        {
            Dish dish = new Dish(products, name);

            model.AllDishs.Add(dish);
        }

        private void ChangeMainDishes()
        {
            mainWindow.GetSetDishes.Items.Clear();

            foreach (Dish product in model.AllDishs.GetListOfDishes())
                mainWindow.GetSetDishes.Items.Add(product.Name);
        }

        #region AddProduct
        private void AddProductFunction(object? sender, EventArgs e)
        {
            AddProduct? addProduct = sender as AddProduct;
            addProduct.add += new EventHandler<EventArgs>(AddProductImBase);
        }

        private void AddProductImBase(object? sender, EventArgs e)
        {
            AddProduct? addProduct = sender as AddProduct;

            string name = addProduct.GetSetName;
            double calories = addProduct.GetSetClories;
            double mass = addProduct.GetSetMass;

            model.AllProducts.CheckToExistsProduct(name);

            AddProduct(name, calories, mass);
        }
        #endregion

        #region ChangeProduct
        private void ChangeProductFunction(object? sender, EventArgs e)
        {
            model.AllProducts.CheckForEmpty();

            ChangeProduct? changeProduct = sender as ChangeProduct;
            changeProduct.change += new EventHandler<EventArgs>(ChangeProductImBase);
            changeProduct.choose += new EventHandler<EventArgs>(ChooseProduct);

            foreach (Product product in model.AllProducts.GetListOfProducts())
                changeProduct.GetSetProducts.Items.Add(product.Name);
        }

        private void ChooseProduct(object? sender, EventArgs e)
        {
            ChangeProduct? changeProduct = sender as ChangeProduct;

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
            ChangeProduct? changeProduct = sender as ChangeProduct;

            string name = changeProduct.GetSetName;
            double calories = changeProduct.GetSetCalories;
            double mass = changeProduct.GetSetMass;

            int index = changeProduct.GetSetProducts.SelectedIndex;
            model.AllProducts.DeleteByIndex(index);

            AddProduct(name, calories, mass);

            changeProduct.GetSetProducts.Items.Clear();

            foreach (Product product in model.AllProducts.GetListOfProducts())
                changeProduct.GetSetProducts.Items.Add(product.Name);

            changeProduct.GetSetProducts.SelectedIndex = 0;
        }
        #endregion

        #region DeleteProduct
        private void DeleteProductFunction(object? sender, EventArgs e)
        {
            model.AllProducts.CheckForEmpty();

            DaleteProduct? deleteProduct = sender as DaleteProduct;
            deleteProduct.delete += new EventHandler<EventArgs>(DeleteProductImBase);

            foreach (Product product in model.AllProducts.GetListOfProducts())
                deleteProduct.GetSetProducts.Items.Add(product.Name);
        }

        private void DeleteProductImBase(object? sender, EventArgs e)
        {
            DaleteProduct? deleteProduct = sender as DaleteProduct;

            int index = deleteProduct.GetSetProducts.SelectedIndex;
            model.AllProducts.DeleteByIndex(index);

            if (model.AllProducts.GetListOfProducts().Count < 1)
            {
                deleteProduct.Close();
                return;
            }

            deleteProduct.GetSetProducts.Items.Clear();
            foreach (Product product in model.AllProducts.GetListOfProducts())
                deleteProduct.GetSetProducts.Items.Add(product.Name);
        }
        #endregion

        #region AddDish
        private void AddDishFunction(object? sender, EventArgs e)
        {
            model.AllProducts.CheckForEmpty();

            AddDish? addDish = sender as AddDish;
            addDish.add += new EventHandler<EventArgs>(AddDishInBase);
            addDish.choose += new EventHandler<EventArgs>(ChooseDish);

            foreach (Product product in model.AllProducts.GetListOfProducts())
                addDish.GetSetProducts.Items.Add(product.Name);
        }

        private void AddDishInBase(object? sender, EventArgs e)
        {
            AddDish? addDish = sender as AddDish;
            
            string name = addDish.GetSetName;
            double calories = addDish.GetSetCalories;

            model.AllDishs.CheckToExistsDisht(name);

            IList products = addDish.GetSetProducts.SelectedItems;
            List<Product> productsForDish = new List<Product>();

            foreach (string product in products)
                productsForDish.Add(model.AllProducts.FindByName(product));

            AddDish(name, productsForDish);

            ChangeMainDishes();
        }

        private void ChooseDish(object? sender, EventArgs e)
        {
            AddDish? addDish = sender as AddDish;

            IList products = addDish.GetSetProducts.SelectedItems;

            double caloriesOfDish = 0;
            foreach (string name in products)
            {
                foreach (Product product in model.AllProducts.GetListOfProducts())
                {
                    if (product.Name == name)
                    {
                        caloriesOfDish += product.CaloriesPer100Gramms;
                        break;
                    }
                }
            }

            addDish.GetSetCalories = caloriesOfDish;
            
        }
        #endregion
    }
}
