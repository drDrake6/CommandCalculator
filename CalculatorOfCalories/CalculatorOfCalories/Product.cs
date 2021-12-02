using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorOfCalories
{
    namespace Logic
    {

        class Product
        {
            private string name;
            private double calories_per_100_gramms;
            private double mass;

            public string Name
            {
                get { return name; }
                set 
                {
                    if (value.Trim().Length == 0)
                     throw new Exception("имя продукта не может быть пустым или состоять из одних пробелов");

                    name = value;
                }
            }

            public double CaloriesPer100Gramms
            {
                get { return calories_per_100_gramms; }
                set
                {
                    if (value < 0)
                        throw new Exception("нельзя есть объекты с отрицательной энергией!");

                    calories_per_100_gramms = value;
                }
            }

            public double MassInKilo
            {
                get { return mass; }
                set
                {
                    if (value < 0)
                        throw new Exception("нельзя есть объекты с отрицательной массой!");

                    mass = value;
                }
            }

            public double GetTotalCalories()
            {
                return mass * calories_per_100_gramms * 10;
            }
        }
    }
}
