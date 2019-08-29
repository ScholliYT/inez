using INEZ.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INEZ.Classes
{
    public static class FuzzyItemMatcher<T> where T : Item
    {
        public static List<T> FilterItems(IEnumerable<T> inputItems, string searchterm, int maxcount = 25)
        {
            List<T> result = inputItems.Select(item =>
            new
            {
                // Create new anonymous object to hold score along with the item itself
                Item = item,
                Score = FuzzyMatcher.FuzzyMatch(item.Name, searchterm)
            })
            .OrderByDescending(i => i.Score)
            .Take(maxcount)
            .Select(i => i.Item)
            .ToList();

            return result;
        }
    }
}
