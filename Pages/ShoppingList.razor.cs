using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using INEZ.Data.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Blazored.Modal.Services;
using INEZ.Classes;
using Blazored.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using INEZ.Data.Services;
using System.Security.Claims;


namespace INEZ.Pages
{
    public class ShoppingListModel : ComponentBase
    {
        [Inject] protected IModalService Modal { get; set; }
        [Inject] protected IUriHelper UriHelper { get; set; }
        [Inject] protected ShoppingListItemsService ShoppingListItemsService { get; set; }

        public IEnumerable<ShoppingListItem> ShoppingListItems { get; set; }

        private string currentUserId;

        protected override async Task OnInitializedAsync()
        {
            await LoadItems();
        }

        public string QuantitiyColumnName
        {
            get
            {
                return DisplayHelper.GetDisplayName<Item>(i => i.Quantity) ?? nameof(Item.Quantity);
            }
        }

        public string NameColumnName
        {
            get
            {
                return DisplayHelper.GetDisplayName<Item>(i => i.Name) ?? nameof(Item.Name);
            }
        }

        private string _searchTerm = "";
        [Parameter]
        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                SearchClick().ConfigureAwait(false);
            }
        }

        private bool _UseFuzzySearch;

        public bool UseFuzzySearch
        {
            get { return _UseFuzzySearch; }
            set
            {
                _UseFuzzySearch = value;
                SearchClick().ConfigureAwait(false);
            }
        }

        private async Task LoadItems()
        {
            if (currentUserId != null)
            {
                ShoppingListItems = await ShoppingListItemsService.GetShoppingListItemsAsync(currentUserId);
            }
            StateHasChanged();
        }

        protected void AddNew()
        {
            UriHelper.NavigateTo($"/edititem/{(int)EditItemModel.EditDataType.ShoppingList}");
        }

        protected async Task SearchClick()
        {
            if (string.IsNullOrEmpty(SearchTerm))
            {
                await LoadItems();
                return;
            }

            await Search(SearchTerm);
        }

        private async Task Search(string term)
        {
            if (UseFuzzySearch)
            {
                //ShoppingListItems = await ItemsService.FuzzySearchItemsAsync(term);
            }
            else
            {
                //ShoppingListItems = await ItemsService.SearchItemsAsync(term);
            }
            StateHasChanged();
        }

        protected async Task ClearClick()
        {
            SearchTerm = "";
            await SearchClick();
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
                await LoadItems();
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
                LoadItems().ConfigureAwait(false);
            }

            return true;
        }
    }
}