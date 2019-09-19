using INEZ.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INEZ.Classes
{
    public static class FuzzyItemMatcher<T> where T : Item
    {
        public static List<ItemSearchResult<T>> FilterItems(IEnumerable<T> inputItems, string searchterm, int maxcount = 25)
        {
            var results = inputItems.Select(item =>
            new ItemSearchResult<T>
            {
                // Create new anonymous object to hold score along with the item itself
                Item = item,
                Score = FuzzyMatcher.FuzzyMatch(item.Name, searchterm)
            })
            .OrderByDescending(r => r.Score)
            .Take(maxcount)
            .ToList();

            // Look for what items to take
            double avg = results.Select(r => r.Score).Average();

            // Take all above average and ~34% below average
            return results.Where(r => r.Score >= avg || r.Score > 0).OrderByDescending(r => r.Score).ToList();
        }
    }
}
