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
            mainWindow.EventDeleteDish += new EventHandler<EventArgs>(DeleteDishFunction);
            mainWindow.EventChangeDish += new EventHandler<EventArgs>(ChangeDishFunction);
            mainWindow.count += new EventHandler<EventArgs>(Count);
        }

        private void Count(object? sender, EventArgs e)
        {
            IList dishes = mainWindow.GetSetDishes.SelectedItems;

            if (dishes.Count < 1)
                throw new ApplicationException("Choose at leas one dish");

            List<string> ration = new List<string>();
            foreach (string dish in dishes)
                ration.Add(dish);

            double weight = mainWindow.GetSetWeight;
            double height = mainWindow.GetSetHeight;
            int age = mainWindow.GetSetAge;
            uint mobility = mainWindow.GetSetMobility;
            uint sex = mainWindow.GetSetSex;

            double result = 0;

            result = model.Calc(ration, weight, height, age, (Sex)mainWindow.GetSetSex, (ActivLevel)mainWindow.GetSetMobility);

            mainWindow.GetSetResult = result;
        }

        private void AddProduct(string name, double calories, double mass)
        {
            Product product = new Product();
            product.Name = name;
            product.MassInKilo = mass;
            product.CaloriesPer100Gramms = calories;

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

        private void ChangeProductInDish(int index, string name, double calories, double mass)
        {
            foreach (Dish dish in model.AllDishs.GetListOfDishes())
            {
                foreach (Product product in dish.GetListOfProducts())
                {
                    if (model.AllProducts[index].Name == product.Name)
                    {
                        dish.Delete(product);
                        Product newProduct = new Product(calories, mass, name);

                        dish.Add(newProduct);

                        return;
                    }
                }
            }
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
            ChangeProductInDish(index, name, calories, mass);
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
            addDish.add += new EventHandler<EventArgs>(AddProductForDishInBase);
            addDish.addProduct += new EventHandler<EventArgs>(AddProductForDishInBasez);
            addDish.choose += new EventHandler<EventArgs>(ChooseProductForDish);

            foreach (Product product in model.AllProducts.GetListOfProducts())
                addDish.GetSetProducts.Items.Add(product.Name);
        }

        private void AddProductForDishInBasez(object? sender, EventArgs e)
        {
            AddDish? addDish = sender as AddDish;

            double calories = addDish.GetSetProductsInDish[addDish.GetSetNameOfProduct];
            addDish.GetSetCalories += calories;
        }

        private void AddProductForDishInBase(object? sender, EventArgs e)
        {
            AddDish? addDish = sender as AddDish;
            
            string name = addDish.GetSetName;
            double calories = addDish.GetSetCalories;

            model.AllDishs.CheckToExistsDisht(name);

            SortedList<string, double> products = addDish.GetSetProductsInDish;
            List<Product> productsForDish = new List<Product>();

            foreach (KeyValuePair<string, double> product in products)
                productsForDish.Add(model.AllProducts.FindByName(product.Key).CloneWithNewMass(product.Value));

            AddDish(name, productsForDish);
            addDish.GetSetProductsInDish.Clear();
            ChangeMainDishes();
        }

        private void ChooseProductForDish(object? sender, EventArgs e)
        {
            AddDish? addDish = sender as AddDish;

            int index = addDish.GetSetProducts.SelectedIndex;

            Product product = model.AllProducts[index];
            addDish.GetSetMass = product.MassInKilo;           
        }
        #endregion

        #region ChangeDish
        private void ChangeDishFunction(object? sender, EventArgs e)
        {
            model.AllDishs.CheckForEmpty();

            ChangeDish? changeDish = sender as ChangeDish;
            changeDish.change += new EventHandler<EventArgs>(ChangeDishInBase);
            changeDish.choose += new EventHandler<EventArgs>(ChooseDishForChange);
            changeDish.chooseProduct += new EventHandler<EventArgs>(ChooseProductOfDishForChange);
            changeDish.deleteProduct += new EventHandler<EventArgs>(DeleteProduct);
            changeDish.selectNewProduct += new EventHandler<EventArgs>(SelectNewProduct);
            changeDish.addNewProduct += new EventHandler<EventArgs>(AddNewProduct);

            foreach (Dish dish in model.AllDishs.GetListOfDishes())
                changeDish.GetSetDishes.Items.Add(dish.Name);

            foreach (Product product in model.AllProducts.GetListOfProducts())
                changeDish.GetSetNewProducts.Items.Add(product.Name);
        }

        private void AddNewProduct(object? sender, EventArgs e)
        {
            ChangeDish? changeDish = sender as ChangeDish;

            int dishIndex = changeDish.GetSetDishes.SelectedIndex;
            string newName = changeDish.GetSetNameOfNewProduct;
            double newMass = changeDish.GetSetMassOfNewProduct;

            foreach (Product productInDish in model.AllDishs[dishIndex].GetListOfProducts())
            {
                if (productInDish.Name == newName)
                    throw new ApplicationException("The product already exists");
            } 

            model.AllDishs[dishIndex].Add(model.AllProducts.FindByName(newName).CloneWithNewMass(newMass));

            changeDish.GetSetCalories = model.AllDishs[dishIndex].CalcCalories();

            changeDish.GetSetProducts.Items.Clear();
            foreach (Product productInDish in model.AllDishs[dishIndex].GetListOfProducts())
                changeDish.GetSetProducts.Items.Add(productInDish.Name);

            changeDish.GetSetProducts.SelectedIndex = 0;
        }

        private void SelectNewProduct(object? sender, EventArgs e)
        {
            ChangeDish? changeDish = sender as ChangeDish;

            Product product = model.AllProducts[changeDish.GetSetNewProducts.SelectedIndex];
            changeDish.GetSetMassOfNewProduct = product.MassInKilo;
        }

        private void ChooseProductOfDishForChange(object? sender, EventArgs e)
        {
            ChangeDish? changeDish = sender as ChangeDish; 
            
            int indexOfDish = changeDish.GetSetDishes.SelectedIndex;
            int indexOfProduct = changeDish.GetSetProducts.SelectedIndex;
            
            if (indexOfDish >= 0 && indexOfProduct >= 0)
            {
                double mass = model.AllDishs[indexOfDish].GetListOfProducts()[indexOfProduct].MassInKilo;
                changeDish.GetSetMassOfProduct = mass;
            }
        }

        private void DeleteProduct(object? sender, EventArgs e)
        {
            ChangeDish? changeDish = sender as ChangeDish;

            int dishIndex = changeDish.GetSetDishes.SelectedIndex;
            int productIndex = changeDish.GetSetProducts.SelectedIndex;

            if (model.AllDishs[dishIndex].GetListOfProducts().Count < 2)
                throw new ArgumentException("Impossible remove last product");

            model.AllDishs[dishIndex].DeleteByIndex(productIndex);

            changeDish.GetSetCalories = model.AllDishs[dishIndex].CalcCalories();

            changeDish.GetSetProducts.Items.Clear();
            foreach (Product product in model.AllDishs[dishIndex].GetListOfProducts())
                changeDish.GetSetProducts.Items.Add(product.Name);

            changeDish.GetSetProducts.SelectedIndex = 0;
        }

        private void ChangeDishInBase(object? sender, EventArgs e)
        {
            ChangeDish? changeDish = sender as ChangeDish;

            string name = changeDish.GetSetName;
            double mass = changeDish.GetSetMassOfProduct;

            int dishIndex = changeDish.GetSetDishes.SelectedIndex;
            int productIndex = changeDish.GetSetProducts.SelectedIndex;
            List<Product> products = model.AllDishs[dishIndex].GetListOfProducts();
            products[productIndex].MassInKilo = mass;

            model.AllDishs.DeleteByIndex(dishIndex);

            AddDish(name, products);

            changeDish.GetSetDishes.Items.Clear();

            foreach (Dish dish in model.AllDishs.GetListOfDishes())
                changeDish.GetSetDishes.Items.Add(dish.Name);

            changeDish.GetSetDishes.SelectedIndex = 0;
            changeDish.GetSetProducts.SelectedIndex = 0;
        }

        private void ChooseDishForChange(object? sender, EventArgs e)
        {
            ChangeDish? changeDish = sender as ChangeDish;

            int index = changeDish.GetSetDishes.SelectedIndex;

            if (index >= 0)
            {
                string name = model.AllDishs[index].Name;
                double calories = model.AllDishs[index].CalcCalories();

                changeDish.GetSetName = name;
                changeDish.GetSetCalories = calories;

                changeDish.GetSetProducts.Items.Clear();
                foreach (Product product in model.AllDishs[index].GetListOfProducts())
                    changeDish.GetSetProducts.Items.Add(product.Name);

                changeDish.GetSetProducts.SelectedIndex = 0;
            }
        }
        #endregion

        #region DeleteDish
        private void DeleteDishFunction(object? sender, EventArgs e)
        {
            model.AllDishs.CheckForEmpty();

            DeleteDish? deleteDish = sender as DeleteDish;
            deleteDish.delete += new EventHandler<EventArgs>(DeleteDishImBase);

            foreach (Dish dish in model.AllDishs.GetListOfDishes())
                deleteDish.GetSetDishes.Items.Add(dish.Name);
        }

        private void DeleteDishImBase(object? sender, EventArgs e)
        {
            DeleteDish? deleteDish = sender as DeleteDish;

            int index = deleteDish.GetSetDishes.SelectedIndex;
            model.AllDishs.DeleteByIndex(index);

            ChangeMainDishes();

            if (model.AllDishs.GetListOfDishes().Count < 1)
            {
                deleteDish.Close();
                return;
            }

            deleteDish.GetSetDishes.Items.Clear();
            foreach (Product product in model.AllProducts.GetListOfProducts())
                deleteDish.GetSetDishes.Items.Add(product.Name);
        }
        #endregion
    }
}
