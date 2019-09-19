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
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace INEZ.Pages
{
    public class ShoppingListModel : ComponentBase
    {
        [Inject] protected IModalService Modal { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected ShoppingListItemsService ShoppingListItemsService { get; set; }
        [Inject] protected CoreDataItemsService CoreDataItemsService { get; set; }
        [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public List<ShoppingListItem> ShoppingListItems { get; set; }
        protected List<CoreDataItem> AvailableItems { get; set; } = new List<CoreDataItem>();

        private ItemSearchResult<CoreDataItem> _selectedResult;
        protected ItemSearchResult<CoreDataItem> SelectedResult
        {
            get => _selectedResult;
            set
            {
                _selectedResult = value;
                SelectedItem = _selectedResult?.Item ?? null;
            }
        }

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
                }
            }
        }

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
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                string userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
                ShoppingListItems = new List<ShoppingListItem>(await ShoppingListItemsService.GetShoppingListItemsAsync(userId));
                ShoppingListItems = ShoppingListItems.OrderByDescending(i => i.CreationTimeStamp).ToList();
            }
            else
            {
                ShoppingListItems = null;
            }

            StateHasChanged();
        }

        protected void AddNewItemManual()
        {
            NavigationManager.NavigateTo($"/edititem/{(int)EditItemModel.EditDataType.ShoppingList}");
        }

        protected async void UpdateCheckedState(ShoppingListItem item, ChangeEventArgs args)
        {
            item.Checked = (bool)args.Value;
            await ShoppingListItemsService.SaveChangesAsync();
            StateHasChanged();
        }

        private async Task AddCoreDataItemAsync(CoreDataItem coreDataItem)
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                string userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;

                // create item
                ShoppingListItem shoppingListItem = new ShoppingListItem()
                {
                    Name = coreDataItem.Name,
                    Quantity = coreDataItem.Quantity,
                    OwnerId = userId,
                };
                await ShoppingListItemsService.CreateOrMergeItemAsync(shoppingListItem);

                await LoadShoppingListItems();
            }
        }

        protected void EditItem(Guid id)
        {
            NavigationManager.NavigateTo($"/edititem/{(int)EditItemModel.EditDataType.ShoppingList}/{id}");
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

        protected async Task<IEnumerable<ItemSearchResult<CoreDataItem>>> GetItem(string searchText)
        {
            var items = await Task.FromResult(FuzzyItemMatcher<CoreDataItem>.FilterItems(AvailableItems, searchText, maxcount: 7));
            return items;
        }
    }
}