using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace INEZ.Classes
{
    public class ValueUnit
    {
        public double Value { get; set; }
        public Unit Unit { get; set; }

        public ValueUnit(double value, Unit unit)
        {
            this.Value = value;
            this.Unit = unit;
        }

        public static bool TryParse(string input, out ValueUnit output)
        {
            string expr = @"([0-9]*([,.][0-9]*)*) *(g|kg|ml|l|Stück|Stk|Stck).*";

            Regex r = new Regex(expr, RegexOptions.IgnoreCase);

            return true;
        }

        public static double? Convert(ValueUnit from, Unit to)
        {
            if (!from.Unit.ConvertableTo.ContainsKey(to)) return null;

            return from.Value * from.Unit.ConvertableTo[to];
        }

        public static bool Convertable(Unit from, Unit to)
        {
            return from.ConvertableTo.ContainsKey(to);
        }

        public static ValueUnit Add(ValueUnit a, ValueUnit b)
        {
            // Check which unit is bigger (e.g. kg is bigger than g) to determine which unit the output is in
            if (a.Unit.Index > b.Unit.Index && Convertable(b.Unit, a.Unit))
                return new Tuple<double, Unit>(a.Value + Convert(b, a.Unit).Value, a.Unit);
            else if (a.Unit.Index <= b.Unit.Index && Convertable(a.Unit, b.Unit))
                return new Tuple<double, Unit>(b.Value + Convert(a, b.Unit).Value, b.Unit);

            return null;
        }
    }
}
