using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CalculatorOfCalories.Logic;
using NLog;

namespace CalculatorOfCalories
{
    internal class Presenter : IDisposable
    {
        private readonly Model model = new Model();
        private MainWindow? mainWindow = null;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private Result result;

        public Presenter(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            Deserialization();

            mainWindow.EventAddProduct += new EventHandler<EventArgs>(AddProductFunction);
            mainWindow.EventChangeProduct += new EventHandler<EventArgs>(ChangeProductFunction);
            mainWindow.EventDeleteProduct += new EventHandler<EventArgs>(DeleteProductFunction);
            mainWindow.EventAddDish += new EventHandler<EventArgs>(AddDishFunction);
            mainWindow.EventDeleteDish += new EventHandler<EventArgs>(DeleteDishFunction);
            mainWindow.EventChangeDish += new EventHandler<EventArgs>(ChangeDishFunction);
            mainWindow.count += new EventHandler<EventArgs>(Count);
            mainWindow.saveResult += new EventHandler<EventArgs>(SaveResult);
            mainWindow.EventSaveListOfProducts += new EventHandler<EventArgs>(SaveListOfProducts);
            mainWindow.EventSaveListOfDishes += new EventHandler<EventArgs>(SaveListOfDishes);

            logger.Info("Application was run");
        }

        private void SaveListOfDishes(object? sender, EventArgs e)
        {
            if (model.AllDishs.GetListOfDishes().Count < 1)
                throw new ApplicationException("No one of dish");

            model.AllDishs.Print();
        }

        private void SaveListOfProducts(object? sender, EventArgs e)
        {
            if (model.AllProducts.GetListOfProducts().Count < 1)
                throw new ApplicationException("No one of product");

            model.AllProducts.Print();
        }

        private void SaveResult(object? sender, EventArgs e)
        {
            model.Print(result.ration, result.weight, result.height, result.age, (Sex)result.sex, (ActivLevel)result.mobility);
        }

        private void Deserialization()
        {
            model.AllProducts.Load();
            model.AllDishs.Load();

            ChangeMainDishes();
        } 

        private void Serialization()
        {
            model.AllProducts.Save();
            model.AllDishs.Save();
        }

        public void Dispose()
        {
            Serialization();
        }

        private void Count(object? sender, EventArgs e)
        {
            IList dishes = mainWindow.GetSetDishes.SelectedItems;

            if (dishes.Count < 1)
            {
                logger.Error("Application was run");
                throw new ApplicationException("Choose at leas one dish");
            }

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
            logger.Info("Calculate was succesfully");

            this.result = new Result(ration, mobility, sex, age, weight, height, result);
            mainWindow.SaveResult.IsEnabled = true;
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
            
            string str = "Dish \"" + dish.Name + "\" was edded. Products:";

            foreach (Product product in products)
                str += " " + product.Name;

            logger.Info(str);
        }

        private void ChangeMainDishes()
        {
            if (mainWindow.GetSetDishes.Items.Count > 0)
                mainWindow.GetSetDishes.Items.Clear();

            foreach (Dish dish in model.AllDishs.GetListOfDishes())
                mainWindow.GetSetDishes.Items.Add(dish.Name);
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

            model.AllProducts.CheckToExistsProduct(name, logger);

            AddProduct(name, calories, mass);
            logger.Info("Product \"" + name + "\" " + calories + "(calories) " + mass + "(kg)" + " was edded");
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

                        logger.Info("Product \"" + product.Name + "\" " + product.CaloriesPer100Gramms + "(calories) " + product.MassInKilo + "(kg)" + " was changed in dish \"" + dish.Name + "\" to "
                             + newProduct.Name + "\" " + newProduct.CaloriesPer100Gramms + "(calories) " + newProduct.MassInKilo + "(kg)");
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

            logger.Info("Prduct \"" + model.AllProducts[index].Name + "\" " + model.AllProducts[index].CaloriesPer100Gramms + "(calories) " + model.AllProducts[index].MassInKilo + "(kg)"
                + " was changed to \"" + name + "\" " + calories + "(calories) " + mass + "(kg)");

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

            logger.Info("Prduct \"" + model.AllProducts[index].Name + "\" was deleted");
            if (model.AllProducts.GetListOfProducts().Count == 1)
            {
                logger.Info("Last product \"" + model.AllProducts[index].Name + "\" was deleted");
                model.AllProducts.DeleteByIndex(index);
                deleteProduct.Close();
                return;
            }

            model.AllProducts.DeleteByIndex(index);

            deleteProduct.GetSetProducts.Items.Clear();
            foreach (Product product in model.AllProducts.GetListOfProducts())
                deleteProduct.GetSetProducts.Items.Add(product.Name);

            deleteProduct.GetSetProducts.SelectedIndex = 0;
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

            double mass = addDish.GetSetProductsInDish[addDish.GetSetNameOfProduct];

            Product product = new Product(addDish.GetSetCaloriesOfProduct, mass);
            double calories = product.GetTotalCalories();

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
            addDish.GetSetCaloriesOfProduct = product.GetTotalCalories();
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
            logger.Info("Product \"" + newName + "\" " + newMass + "(calories) " + model.AllProducts.FindByName(newName).MassInKilo + "(kg)" + 
                " was edded to dish \"" + model.AllDishs[dishIndex].Name + "\"");

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

            logger.Info("Product \"" + model.AllDishs[dishIndex].GetListOfProducts()[productIndex].Name + "\" "
                + " was deleted in dish \"" + model.AllDishs[dishIndex].Name + "\"");

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

            double calories = 0;
            foreach (Product product in model.AllDishs[dishIndex].GetListOfProducts())
                calories += product.CaloriesPer100Gramms;

            string str = "Dish \"" + model.AllDishs[dishIndex].Name + "\" " + model.AllDishs[dishIndex].CalcCalories() + "(calories)"
                + " was changed in \n\t Dish \"" + name + "\" " + calories + "(calories)";

            logger.Info(str);

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

            logger.Info("Dish \"" + model.AllDishs[index].Name + "\" was deleted");
            if (model.AllDishs.GetListOfDishes().Count == 1)
            {
                logger.Info("Last dish \"" + model.AllDishs[index].Name + "\" was deleted");
                model.AllDishs.DeleteByIndex(index);
                ChangeMainDishes();
                deleteDish.Close();
                return;
            }

            model.AllDishs.DeleteByIndex(index);
            ChangeMainDishes();

            deleteDish.GetSetDishes.Items.Clear();
            foreach (Dish dish in model.AllDishs.GetListOfDishes())
                deleteDish.GetSetDishes.Items.Add(dish.Name);

            deleteDish.GetSetDishes.SelectedIndex = 0;
        }
        #endregion
    }
}
