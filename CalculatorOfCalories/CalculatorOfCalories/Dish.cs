using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorOfCalories
{
    namespace Logic
    {
        class Dish
        {
            private List<Product> products;

            public Dish()
            {
                products = new List<Product>();
            }

            public Dish(List<Product> _products)
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
            }

            public void Delete(Product product)
            {
                products.Remove(product);
            }

            public void DeleteByIndex(int index)
            {
                products.RemoveAt(index);
            }

            public int GetIndex(Product product)
            {
                return products.IndexOf(product);
            }
        }
    }

    
}
