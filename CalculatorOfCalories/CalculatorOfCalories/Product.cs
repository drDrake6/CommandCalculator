using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorOfCalories
{
    namespace Logic
    {

        class Product : IComparable<Product>
        {
            private string name;
            private double calories_per_100_gramms;
            private double mass;

            public Product(double calories)
            {
                this.calories_per_100_gramms = calories;
            }

            public Product(double calories, double mass) : this(calories)
            {
                this.mass = mass;
            }

            public Product(double calories, double mass, string name) : this(calories, mass)
            {
                this.name = name;
            }

            public string Name
            {
                get { return name; }
                set 
                {
                    if (value.Trim().Length == 0)
                     throw new Exception("Product name cannot be empty or contain only spaces");

                    name = value;
                }
            }

            public double CaloriesPer100Gramms
            {
                get { return calories_per_100_gramms; }
                set
                {
                    if (value < 0)
                        throw new Exception("You can not eat objects with negative energy");

                    calories_per_100_gramms = value;
                }
            }

            public double MassInKilo
            {
                get { return mass; }
                set
                {
                    if (value < 0)
                        throw new Exception("You cannot eat objects with negative mass");

                    mass = value;
                }
            }

            public int CompareTo(Product? other)
            {
                return Name.CompareTo(other.Name);
            }

            public double GetTotalCalories()
            {
                return mass * calories_per_100_gramms * 10;
            }
        }
    }
}
