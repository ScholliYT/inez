using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INEZ.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace INEZ.Data.Services
{
    public class ShoppingListItemsService
    {
        private readonly InezContext _context;

        public ShoppingListItemsService(InezContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShoppingListItem>> GetShoppingListItemsAsync(string userId)
        {
            return await _context.ShoppingListItems.Where(i => i.OwnerId == userId).OrderBy(i => i.Name).ToListAsync();
        }


        public async Task<ShoppingListItem> GetItemAsync(Guid id)
        {
            return await _context.ShoppingListItems.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task CreateItemAsync(ShoppingListItem item)
        {
            _context.ShoppingListItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(ShoppingListItem item)
        {
            _context.ShoppingListItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}