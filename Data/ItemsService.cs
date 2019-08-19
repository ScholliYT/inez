using INEZ.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INEZ.Data
{
    public class ItemsService
    {
        private InezContext _context;
        public ItemsService(InezContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<IEnumerable<Item>> SearchItemsAsync(string searchterm)
        {
            return await _context.Items.Where(i => i.Name.Contains(searchterm) || i.Description.Contains(searchterm)).ToListAsync();
        }

        public async Task<Item> CreateItemAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
