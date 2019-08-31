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

        public async Task<ShoppingListItem> GetItemByNameAsync(string userid, string name)
        {
            return await _context.ShoppingListItems.Where(i => i.OwnerId == userid).FirstOrDefaultAsync(i => i.Name.ToLower() == name.ToLower());
        }

        public async Task CreateOrMergeItemAsync(ShoppingListItem item)
        {
            ShoppingListItem itemOnList = await GetItemByNameAsync(item.OwnerId, item.Name);

            // check if items could be merged
            if (itemOnList != null && (ValueUnit.TryParse(itemOnList.Quantity, out ValueUnit onlistValue) && ValueUnit.TryParse(item.Quantity, out ValueUnit coreDataValue)))
            {
                // merge items
                ValueUnit newValueUnit = ValueUnit.Add(onlistValue, coreDataValue);

                itemOnList.Quantity = newValueUnit.ToString();
            }
            else
            {
                // create new item
                _context.ShoppingListItems.Add(item);
            }
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