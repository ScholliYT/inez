using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INEZ.Classes;
using INEZ.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace INEZ.Data
{
    public class ItemsService
    {
        private readonly InezContext _context;

        public ItemsService(InezContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _context.Items
                .ToListAsync();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            return await _context.Items
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Item>> SearchItemsAsync(string searchterm)
        {
            return await _context.Items.Where(i => i.Name.Contains(searchterm))
                .ToListAsync();
        }

        public async Task<IEnumerable<Item>> FuzzySearchItemsAsync(string searchterm)
        {
            // apply lowercase filter
            //Fastenshtein.Levenshtein lev = new Fastenshtein.Levenshtein(searchterm.ToLower());
            //return (await GetItemsAsync()).Where(item => IsMatch(searchterm.ToLower(), lev, item.Name.ToLower()));

            var items = await GetItemsAsync();

            IEnumerable<Item> result = items.Select(item =>
            new
            {
                // Create new anonymous object to hold score along with the item itself
                Item = item,
                Score = FuzzyMatcher.FuzzyMatch(item.Name, searchterm)
            })
            .OrderByDescending(i => i.Score)
            .Take(25)
            .Select(i => i.Item);

            return result;
        }

        public async Task<Item> CreateItemAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return await GetItemAsync(item.Id);
        }

        public async Task DeleteItem(Item item)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool IsMatch(string compSource, Fastenshtein.Levenshtein compSourceLev, string compTo)
        {
            return compSource.Equals(compTo) ||
                   compSource.Contains(compTo) ||
                   compTo.Contains(compSource) ||
                   1 - LevenshteinDistanceNormalized(compSourceLev, compSource.Length, compTo) > 0.8d;
        }

        private double LevenshteinDistanceNormalized(Fastenshtein.Levenshtein lev, int lenLev, string str)
        {
            int maxlen = Math.Max(lenLev, str.Length);
            int distance = lev.DistanceFrom(str);
            return (double)distance / maxlen;
        }
    }
}