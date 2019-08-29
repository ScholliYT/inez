using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INEZ.Classes
{
    public class QuantityCalculations
    {
        public static double? Convert(Tuple<double, Unit> from, Unit to)
        {
            if (!from.Item2.ConvertableTo.ContainsKey(to)) return null;

            return from.Item1 * from.Item2.ConvertableTo[to];
        }

        public static bool Convertable(Unit from, Unit to)
        {
            return from.ConvertableTo.ContainsKey(to);
        }

        public static Tuple<double, Unit> Add(Tuple<double, Unit> a, Tuple<double, Unit> b)
        {
            // Check which unit is bigger (e.g. kg is bigger than g) to determine which unit the output is in
            if (a.Item2.Index > b.Item2.Index && Convertable(b.Item2, a.Item2))
                return new Tuple<double, Unit>(a.Item1 + Convert(b, a.Item2).Value, a.Item2);
            else if (a.Item2.Index <= b.Item2.Index && Convertable(a.Item2, b.Item2))
                return new Tuple<double, Unit>(b.Item1 + Convert(a, b.Item2).Value, b.Item2);

            return null;
        }
    }
}
