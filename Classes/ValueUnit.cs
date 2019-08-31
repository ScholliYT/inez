using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace INEZ.Classes
{
    public class ValueUnit
    {
        private static readonly Regex parsingRegex = new Regex(@"([0-9]*([,.][0-9]*)*) *(g|kg|ml|l|Stück|Stk.|Stck.).*", RegexOptions.IgnoreCase);
        
        public double Value { get; set; }
        public Unit Unit { get; set; }

        public ValueUnit(double value, Unit unit)
        {
            this.Value = value;
            this.Unit = unit;
        }

        public override string ToString()
        {
            return Value.ToString("G", new CultureInfo("de-DE")) + " " + Unit.Name;
        }

        public static bool TryParse(string input, out ValueUnit output)
        {
            Match m = parsingRegex.Match(input);

            if (m.Success)
            {
                string value = m.Groups[1].Value;
                string unit = m.Groups[3].Value;

                double parsedValue;

                // Set culture info depending on format of value to be parsed (e.g. 1.0 or 1,0)
                if (value.Contains(",")) Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
                else Thread.CurrentThread.CurrentCulture = new CultureInfo("en-EN");
                
                if (Double.TryParse(value, out parsedValue))
                {
                    output = new ValueUnit(parsedValue, Unit.GetUnitByAbbreviation(unit));
                    return true;
                }
                else
                {
                    output = null;
                    return false;
                }
            }

            output = null;
            return false;
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
                return new ValueUnit(a.Value + Convert(b, a.Unit).Value, a.Unit);
            else if (a.Unit.Index <= b.Unit.Index && Convertable(a.Unit, b.Unit))
                return new ValueUnit(b.Value + Convert(a, b.Unit).Value, b.Unit);

            return null;
        }
    }
}
