using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INEZ.Classes;
using INEZ.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace INEZ.Data.Services
{
    public class CoreDataItemsService
    {
        private readonly InezContext _context;

        public CoreDataItemsService(InezContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CoreDataItem>> GetItemsAsync()
        {
            return await _context.CoreDataItems.ToListAsync();
        }

        public async Task<CoreDataItem> GetItemAsync(Guid id)
        {
            return await _context.CoreDataItems
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<CoreDataItem>> SearchItemsAsync(string searchterm)
        {
            return await _context.CoreDataItems.Where(i => i.Name.Contains(searchterm))
                .ToListAsync();
        }

        public async Task<IEnumerable<CoreDataItem>> FuzzySearchItemsAsync(string searchterm)
        {
            // apply lowercase filter
            //Fastenshtein.Levenshtein lev = new Fastenshtein.Levenshtein(searchterm.ToLower());
            //return (await GetItemsAsync()).Where(item => IsMatch(searchterm.ToLower(), lev, item.Name.ToLower()));

            var items = await GetItemsAsync();

            IEnumerable<CoreDataItem> result = items.Select(item =>
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

        public async Task<CoreDataItem> CreateItemAsync(CoreDataItem item)
        {
            _context.CoreDataItems.Add(item);
            await _context.SaveChangesAsync();
            return await GetItemAsync(item.Id);
        }

        public async Task DeleteItem(CoreDataItem item)
        {
            _context.CoreDataItems.Remove(item);
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