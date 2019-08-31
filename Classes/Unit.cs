using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INEZ.Classes
{
    public class Unit
    {
        public static readonly Unit Grams = new Unit("Gramm", "g", 0);
        public static readonly Unit Kilograms = new Unit("Kilogramm", "kg", 1);
        public static readonly Unit Milliliters = new Unit("Milliliter", "ml", 0);
        public static readonly Unit Liters = new Unit("Liter", "l", 1);
        public static readonly Unit Pieces = new Unit("Stück", "Stk.", 0);

        public static readonly List<Unit> List = new List<Unit> { Grams, Kilograms, Milliliters, Liters, Pieces };
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }
        public int Index { get; private set; }

        private Unit(string name, string abbreviation, int index)
        {
            this.Name = name;
            this.Abbreviation = abbreviation;
            this.Index = index;
        }

        public bool Convertable(Unit otherUnit, out double factor)
        {
            if (this == Grams)
            {
                factor = 1000;
                return otherUnit == Kilograms;
            }
            else if (this == Kilograms)
            {
                factor = 0.001;
                return otherUnit == Grams;
            }
            else if (this == Milliliters)
            {
                factor = 1000;
                return otherUnit == Liters;
            }
            else if (this == Liters)
            {
                factor = 0.001;
                return otherUnit == Milliliters;
            }

            factor = default;
            return false;
        }

        public static Unit GetUnitByAbbreviation(string abbreviation)
        {
            foreach (Unit unit in List)
            {
                if (unit.Abbreviation == abbreviation) return unit;
            }

            return null;
        }
    }
}
