﻿using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;


namespace CalculatorOfCalories
{
    namespace Logic
    {
        [Serializable]
        class BaseOfDishs
        {
            private List<Dish> dishs;

            public BaseOfDishs()
            {
                dishs = new List<Dish>();
            }

            public BaseOfDishs(List<Dish> _dishs)
            {
                dishs = _dishs;
            }

            public Dish this[int index]
            {
                get
                {
                    if (index >= 0 && index < dishs.Count)
                    {
                        return dishs[index];
                    }
                    else
                    {
                        throw new Exception("Некорректный индекс! " + index);
                    }
                }
                set
                {
                    if (index >= 0 && index < dishs.Count)
                    {
                        dishs[index] = value;
                    }
                    else
                    {
                        throw new Exception("Некорректный индекс! " + index);
                    }
                }
            }

            public void Add(Dish product)
            {
                dishs.Add(product);
                dishs.Sort();
            }

            public void Delete(Dish product)
            {
                dishs.Remove(product);
                dishs.Sort();
            }

            public void DeleteByIndex(int index)
            {
                dishs.RemoveAt(index);
                dishs.Sort();
            }

            public int GetIndex(Dish product)
            {
                return dishs.IndexOf(product);
            }

            public List<Dish> GetListOfDishes()
            {
                return new List<Dish>(dishs.ToArray());
            }

            public void Clear()
            {
                dishs.Clear();
            }     
            
            public Dish FindByName(string name)
            {
                foreach (Dish item in dishs)
                {
                    if (item.Name == name)
                        return item;
                }

                throw new Exception("Dish are not exists");
            }

            public void CheckToExistsDisht(string name)
            {
                foreach (Dish item in dishs)
                {
                    if (item.Name == name)
                        throw new ApplicationException("Dish with the same name already axists");
                }
            }

            public void CheckForEmpty()
            {
                if (dishs.Count < 1)
                    throw new ApplicationException("No dishes");
            }

            public double GetCaloriesByName(string name)
            {
                return FindByName(name).CalcCalories();
            }

            public void Save()
            {
                Directory.CreateDirectory("DataBase");

                FileStream stream = new FileStream("DataBase/dishs.json", FileMode.Create);
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Dish>));
                jsonFormatter.WriteObject(stream, dishs);
                stream.Close();
                Console.WriteLine("Сохранено!");
            }

            public void Load()
            {
                FileStream stream = null;

                try
                {
                    stream = new FileStream("DataBase/dishs.json", FileMode.Open);
                }
                catch (Exception e)
                {
                    stream?.Close();
                    return;
                }
                
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Dish>));
                dishs = (List<Dish>)jsonFormatter.ReadObject(stream);
                stream.Close();
            }

            public void Print()
            {
                Directory.CreateDirectory("Dishes");

                StreamWriter sw = new StreamWriter("Dishes\\Dishes.txt", false);
                foreach (Dish d in dishs)
                {
                    sw.WriteLine(d.Name + ",  " + d.CalcCalories() + " calories, " + d.CalcMass() + " kg");
                    sw.WriteLine("Products: ");
                    for (int i = 0; i < d.CountOfProducts(); i++)
                    {
                        sw.WriteLine("\t" + d[i].Name + ",  " + d[i].CaloriesPer100Gramms + " calories, " + d[i].MassInKilo + " kg");
                    }
                    sw.WriteLine("====================================");
                }
                sw.Close();
            }
        }
    }
}
