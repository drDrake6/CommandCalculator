using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorOfCalories
{
    internal class Result
    {
        public Result(List<string> ration, uint mobility, uint sex, int age, double weight, double height, double result)
        {
            this.ration = ration;
            this.mobility = mobility;
            this.sex = sex;
            this.age = age;
            this.weight = weight;
            this.height = height;
            this.result = result;
        }

        public List<string> ration { get; set; }
        public uint mobility { get; set; }
        public uint sex { get; set; }
        public int age { get; set; }
        public double weight { get; set; }
        public double height { get; set; }
        public double result { get; set; }
    }
}
