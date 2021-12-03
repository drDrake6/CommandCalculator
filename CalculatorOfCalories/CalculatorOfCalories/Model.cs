using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorOfCalories
{
    namespace Logic
    {
        enum Sex { Female, Male }
        enum ActivLevel { Minimal, Low, Middle, High, Extrime}
        
        class Model
        {
            public static readonly Dictionary<ActivLevel, double> ActivLevelDict = new Dictionary<ActivLevel, double>();
            private BaseOfDishs dishs = new BaseOfDishs();
            private BaseOfProducts products = new BaseOfProducts();           

            public Model()
            {
                ActivLevelDict[ActivLevel.Minimal] = 1.2;
                ActivLevelDict[ActivLevel.Low] = 1.375;
                ActivLevelDict[ActivLevel.Middle] = 1.55;
                ActivLevelDict[ActivLevel.High] = 1.7;
                ActivLevelDict[ActivLevel.Extrime] = 1.9;
            }
            public BaseOfDishs AllDishs
            {
                get { return dishs; }
                set { dishs = value; }
            }

            public BaseOfProducts AllProducts
            {
                get { return products; }
                set { products = value; }
            }           

            public double Calc(List<string> ration, double mass_in_kg, double hight_in_sm, int age_in_years, 
                Sex sex, ActivLevel activ_level)
            {
                double norm = 0;

                if(sex == Sex.Female)
                {
                    norm = ActivLevelDict[activ_level] * 
                        (655.1 + (9.563 * mass_in_kg) + (1.85 * hight_in_sm) - (4.676 * age_in_years));
                }
                else if(sex == Sex.Male)
                {
                    norm = ActivLevelDict[activ_level] *
                        (66.5 + (13.75 * mass_in_kg) + (5.003 * hight_in_sm) - (6.775 * age_in_years));
                }

                double calories_in_ration = 0;

                foreach (string item in ration)
                {
                    calories_in_ration += dishs.GetCaloriesByName(item);
                }

                return calories_in_ration - norm;
            }

        }
    }
    
}
