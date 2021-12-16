using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorOfCalories
{
    internal class Regular
    {
        private static Regex name = new Regex(@"^\S[^\/:*?""<>|]*$");
        private static Regex numericWithDotWithoutMass = new Regex(@"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$");
        private static Regex numericWithoutDot = new Regex(@"^\d+\s*$");

        public static bool CheckName(string name)
        {
            return Regular.name.IsMatch(name);
        }

        public static bool CheckNumeric(string numeric)
        {
            return Regular.numericWithDotWithoutMass.IsMatch(numeric);
        }

        public static bool CheckNumericWithoutDot(string numeric)
        {
            return numericWithoutDot.IsMatch(numeric);
        }

        public static Regex HasName{ get => name; }
        public static Regex HasNumericWithDotWithoutMass { get => numericWithDotWithoutMass; }
        public static Regex NumericWithoutDot { get => numericWithoutDot; }
    }
}
