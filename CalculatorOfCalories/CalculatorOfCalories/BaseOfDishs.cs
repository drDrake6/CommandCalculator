using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorOfCalories
{
    namespace Logic
    {
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
            }

            public void Delete(Dish product)
            {
                dishs.Remove(product);
            }

            public void DeleteByIndex(int index)
            {
                dishs.RemoveAt(index);
            }

            public int GetIndex(Dish product)
            {
                return dishs.IndexOf(product);
            }

            public List<Dish> GetListOfProducts()
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

                throw new Exception("блюдо с таким именем ненайдено!");
            }

            public double GetCaloriesByName(string name)
            {
                return FindByName(name).CalcCalories();
            }
        }
    }
}
