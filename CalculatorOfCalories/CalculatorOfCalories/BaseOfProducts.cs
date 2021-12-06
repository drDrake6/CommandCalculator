﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Windows;

namespace CalculatorOfCalories
{
    namespace Logic
    {
        [Serializable]
        class BaseOfProducts
        {
            private List<Product> products;

            public BaseOfProducts()
            {
                products = new List<Product>();
            }

            public BaseOfProducts(List<Product> _products)
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

            public List<Product> GetListOfProducts()
            {
                return new List<Product>(products.ToArray());
            }

            public void Clear()
            {
                products.Clear();
            }

            public Product FindByName(string name)
            {
                foreach (Product item in products)
                {
                    if (item.Name == name)
                        return item;
                }

                throw new ApplicationException("Product are not axists");
            }

            public void CheckToExistsProduct(string name)
            {
                foreach (Product item in products)
                {
                    if (item.Name == name)
                        throw new ApplicationException("Product with the same name already axists");
                }
            }

            public void CheckForEmpty()
            {
                if (products.Count < 1)
                    throw new ApplicationException("No products");
            }

            public void Save()
            {
                FileStream stream = new FileStream("../../products.json", FileMode.Create);
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Product>));
                jsonFormatter.WriteObject(stream, products);
                stream.Close();
                Console.WriteLine("Сохранено!");
            }

            public void Load()
            {
                FileStream stream = null;

                try
                {
                    stream = new FileStream("../../products.json", FileMode.Open);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.StackTrace);
                    stream.Close();
                    return;
                }

                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Product>));
                products = (List<Product>)jsonFormatter.ReadObject(stream);
                string s = String.Empty;
                foreach (Product p in products)
                {
                    s += p.Name + '\n';
                }
                Console.WriteLine(s);
                Console.WriteLine("Загружено!");
                stream.Close();
            }
        }
    }
}
