using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using INEZ.Data.Entities;
using Microsoft.AspNetCore.Components;
using Blazored.Modal.Services;
using INEZ.Classes;
using Blazored.Modal;
using INEZ.Data.Services;
using System.Linq;

namespace INEZ.Pages
{
    public class ShoppingListModel : ComponentBase
    {
        [Inject] protected IModalService Modal { get; set; }
        [Inject] protected IUriHelper UriHelper { get; set; }
        [Inject] protected ShoppingListItemsService ShoppingListItemsService { get; set; }
        [Inject] protected CoreDataItemsService CoreDataItemsService { get; set; }

        public List<ShoppingListItem> ShoppingListItems { get; set; }
        protected List<CoreDataItem> AvailableItems { get; set; } = new List<CoreDataItem>();

        private CoreDataItem _selectedItem;
        protected CoreDataItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    AddCoreDataItemAsync(SelectedItem).ConfigureAwait(false);
                    // TODO: clear SelectedItem
                }
            }
        }

        private string currentUserId;

        #region Display / Layout
        public string QuantityColumnName => DisplayHelper.GetDisplayName<Item>(i => i.Quantity) ?? nameof(Item.Quantity);
        public string NameColumnName => DisplayHelper.GetDisplayName<Item>(i => i.Name) ?? nameof(Item.Name);

        #endregion

        protected override async Task OnInitializedAsync()
        {
            await LoadShoppingListItems();

            AvailableItems.AddRange(await CoreDataItemsService.GetItemsAsync());
        }

        private async Task LoadShoppingListItems()
        {
            ShoppingListItems = new List<ShoppingListItem>();

            if (currentUserId != null)
            {
                ShoppingListItems.AddRange(await ShoppingListItemsService.GetShoppingListItemsAsync(currentUserId));
                ShoppingListItems = ShoppingListItems.OrderByDescending(i => i.CreationTimeStamp).ToList();
            }

            StateHasChanged();
        }

        protected void AddNewItemManual()
        {
            UriHelper.NavigateTo($"/edititem/{(int)EditItemModel.EditDataType.ShoppingList}");
        }

        protected async void UpdateCheckedState(ShoppingListItem item, UIChangeEventArgs args)
        {
            item.Checked = (bool)args.Value;
            await ShoppingListItemsService.SaveChangesAsync();
            StateHasChanged();
        }

        private async Task AddCoreDataItemAsync(CoreDataItem coreDataItem)
        {
            ShoppingListItem shoppingListItem = new ShoppingListItem()
            {
                Name = coreDataItem.Name,
                Quantity = coreDataItem.Quantity,
                OwnerId = currentUserId,
            };
            await ShoppingListItemsService.CreateItemAsync(shoppingListItem);
            await LoadShoppingListItems();
        }

        protected void EditItem(Guid id)
        {
            UriHelper.NavigateTo($"/edititem/{(int)EditItemModel.EditDataType.ShoppingList}/{id}");
        }

        protected void ConfirmDelete(ShoppingListItem item)
        {
            var parameters = new ModalParameters();

            parameters.Add("Item", item);

            Modal.OnClose += DelteConfirmModalClosed;
            Modal.Show("Löschen bestätigen", typeof(ConfirmDeleteDialog), parameters);
        }

        protected async void DelteConfirmModalClosed(ModalResult result)
        {
            Modal.OnClose -= DelteConfirmModalClosed;
            if (!result.Cancelled)
            {
                await ShoppingListItemsService.DeleteItem((ShoppingListItem)result.Data);
                await LoadShoppingListItems();
            }
        }

        /// <summary>
        /// Used to get the userId from Razor component because only there the HTTPContext is available
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>true</returns>
        protected bool SetCurrentUserId(string userid)
        {
            // prevent infinite loop
            if (currentUserId != userid)
            {
                currentUserId = userid;
                LoadShoppingListItems().ConfigureAwait(false);
            }

            return true;
        }

        protected async Task<List<CoreDataItem>> GetItem(string searchText)
        {
            List<CoreDataItem> items = await Task.FromResult(FuzzyItemMatcher<CoreDataItem>.FilterItems(AvailableItems, searchText, maxcount: 7));
            return items;
        }
    }
}