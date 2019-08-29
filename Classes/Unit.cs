using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INEZ.Classes
{
    public class Unit
    {
        public static readonly Unit Grams       = new Unit("Gramm", "g", 0, new Dictionary<Unit, double>
        {
            { Kilograms, 1000 },
        });
        public static readonly Unit Kilograms   = new Unit("Kilogramm", "kg", 1, new Dictionary<Unit, double>
        {
            { Grams, 0.001 },
        });
        public static readonly Unit Milliliters = new Unit("Milliliter", "ml", 0, new Dictionary<Unit, double>
        {
            { Liters, 1000 },
        });
        public static readonly Unit Liters      = new Unit("Liter", "l", 1, new Dictionary<Unit, double>
        {
            { Milliliters, 0.001 },
        });
        public static readonly Unit Pieces = new Unit("Stück", "Stk.", 0, new Dictionary<Unit, double>
        { });

        public string Name { get; private set; }
        public string Abbreviation { get; private set; }
        public int Index { get; private set; }
        public Dictionary<Unit, Double> ConvertableTo { get; private set; }

        private Unit(string name, string abbreviation, int index, Dictionary<Unit, double> convertableTo)
        {
            this.Name = name;
            this.Abbreviation = abbreviation;
            this.Index = index;
            this.ConvertableTo = convertableTo;
        }
    }
}
