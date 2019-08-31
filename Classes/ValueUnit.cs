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
        // Matchs units on abbreviations and names only
        private static readonly IEnumerable<string> unitAbbreviations = Unit.AllUnits.Select(u => u.Abbreviation);
        private static readonly IEnumerable<string> unitNames = Unit.AllUnits.Select(u => u.Name);

        private static readonly Regex parsingRegex = new Regex(@"^([0-9]*([,.][0-9]*)*) *(" + string.Join("|", unitAbbreviations.Concat(unitNames)) + @")$", RegexOptions.IgnoreCase);

        public double Value { get; set; }
        public Unit Unit { get; set; }

        public ValueUnit(double value, Unit unit)
        {
            this.Value = value;
            this.Unit = unit;
        }

        public override string ToString()
        {
            return Value.ToString("G", new CultureInfo("de-DE")) + " " + Unit.Abbreviation;
        }

        public static bool TryParse(string input, out ValueUnit output)
        {
            Match m = parsingRegex.Match(input);

            if (m.Success)
            {
                string value = m.Groups[1].Value;
                string unit = m.Groups[3].Value;

                // Set culture info depending on format of value to be parsed (e.g. 1.0 or 1,0)
                if (value.Contains(",")) Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
                else Thread.CurrentThread.CurrentCulture = new CultureInfo("en-EN");


                if (double.TryParse(value, out double parsedValue))
                {
                    output = new ValueUnit(parsedValue, Unit.GetUnitByAbbreviation(unit) ?? Unit.GetUnitByName(unit));
                    if (output == null || output.Unit == null)
                    {
                        output = null;
                        return false;
                    }
                    return true;
                }
            }
            output = null;
            return false;
        }

        public static double? Convert(ValueUnit from, Unit to)
        {
            if (from.Unit.Convertable(to, out double factor))
            {
                return from.Value * factor;
            }
            else
            {
                return null;
            }
        }

        public static bool Convertable(Unit from, Unit to)
        {
            return from.Convertable(to, out _); // declare out as an inline discard variable
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
