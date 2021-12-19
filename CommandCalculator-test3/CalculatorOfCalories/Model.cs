using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

            public void Print(List<string> ration, double mass_in_kg, double hight_in_sm, int age_in_years,
                Sex sex, ActivLevel activ_level)
            {
                Directory.CreateDirectory("Result");

                string data = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day
                    + " " + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;

                StreamWriter sw = new StreamWriter("Result\\" + data + ".txt", false);
                sw.WriteLine("dishs: ");
                foreach (string item in ration)
                {
                    sw.WriteLine("\t" + item);
                }
                sw.WriteLine("Mass: " + mass_in_kg + " kg");
                sw.WriteLine("Hight: " + hight_in_sm + " sm");
                sw.WriteLine("Age: " + age_in_years);
                if (sex == Sex.Male)
                    sw.WriteLine("Sex: Male");
                else
                    sw.WriteLine("Sex: Female");

                switch (activ_level)
                {
                    case ActivLevel.Minimal:
                        sw.WriteLine("Level of activity: Minimal");
                        break;
                    case ActivLevel.Low:
                        sw.WriteLine("Level of activity: Low");
                        break;
                    case ActivLevel.Middle:
                        sw.WriteLine("Level of activity: Middle");
                        break;
                    case ActivLevel.High:
                        sw.WriteLine("Level of activity: High");
                        break;
                    case ActivLevel.Extrime:
                        sw.WriteLine("Level of activity: Extrime");
                        break;
                    default:
                        break;
                }

                sw.WriteLine("Result in calories: " + 
                    Calc(ration, mass_in_kg, hight_in_sm, age_in_years, sex, activ_level));

                sw.Close();
            }
        }
    }
    
}
