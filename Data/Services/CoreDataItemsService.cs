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
            var items = await GetItemsAsync();

            return FuzzyItemMatcher<CoreDataItem>.FilterItems(items, searchterm).Select(r => r.Item);
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
    }
}