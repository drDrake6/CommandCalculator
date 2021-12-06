using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorOfCalories
{
    namespace Logic
    {
        class Dish : IComparable<Dish>
        {
            private List<Product> products;
            private string name;

            public string Name
            {
                get { return name; }
                set
                {
                    if (value.Trim().Length == 0)
                        throw new Exception("Dish name cannot be empty or contain only spaces");

                    name = value;
                }
            }

            public Dish()
            {
                Name = "default";
                products = new List<Product>();
            }

            public Dish(string _name)
            {
                Name = _name;
                products = new List<Product>();
            }

            public Dish(List<Product> _products, string _name) : this(_name)
            {
                products = _products;
            }

            public Product this[int index]
            {
                get
                {
                    if (index >= 0 && index < products.Count)
                    {
                        return products[index];
                    }
                    else
                    {
                        throw new Exception("Некорректный индекс! " + index);
                    }
                }
                set
                {
                    if (index >= 0 && index < products.Count)
                    {
                        products[index] = value;
                    }
                    else
                    {
                        throw new Exception("Некорректный индекс! " + index);
                    }
                }
            }

            public void Add(Product product)
            {
                products.Add(product);
                products.Sort();
            }

            public void Delete(Product product)
            {
                products.Remove(product);
                products.Sort();
            }

            public void DeleteByIndex(int index)
            {
                products.RemoveAt(index);
                products.Sort();
            }

            public int GetIndex(Product product)
            {
                return products.IndexOf(product);
            }

            public void CheckForNoProducts()
            {
                if (products.Count == 0)
                    throw new ApplicationException("Add at least on product");
            }

            public List<Product> GetListOfProducts()
            {
                return new List<Product>(products.ToArray());
            }

            public void Clear()
            {
                products.Clear();
            }

            public double CalcCalories()
            {
                double calories = 0;
                foreach (Product product in products)
                {
                    calories += product.GetTotalCalories();
                }
                return calories;
            }

            public int CompareTo(Dish? other)
            {
                return name.CompareTo(other.name);
            }
        }
    }

    
}
