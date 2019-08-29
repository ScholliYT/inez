using System;
using System.Security.Claims;
using System.Threading.Tasks;
using INEZ.Data;
using INEZ.Data.Entities;
using INEZ.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace INEZ.Pages
{
    public class EditItemModel : ComponentBase
    {
        public enum EditDataType
        {
            CoreData,
            ShoppingList
        }

        [Inject] protected IUriHelper UriHelper { get; set; }
        [Inject] protected CoreDataItemsService CoreDataItemsService { get; set; }
        [Inject] protected ShoppingListItemsService ShoppingListItemsService { get; set; }
        [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Parameter] public Guid Id { get; set; }
        [Parameter] public int? Datatype { get; set; }

        protected bool LoadFailed { get; private set; }
        protected Item Item { get; private set; }
        protected bool CreationMode { get; private set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                CreationMode = Id == default;

                if (!Datatype.HasValue)
                {
                    LoadFailed = true;
                    return;
                }

                switch ((EditDataType)Datatype)
                {
                    case EditDataType.CoreData:
                        Item = CreationMode ? new CoreDataItem() : await CoreDataItemsService.GetItemAsync(Id);
                        break;
                    case EditDataType.ShoppingList:
                        Item = CreationMode ? new ShoppingListItem()
                        {
                            // We need to set the ownerId here so the model can be valid before the save button is pressed
                            OwnerId = await GetUserId()

                        } : await ShoppingListItemsService.GetItemAsync(Id);
                        break;
                    default:
                        LoadFailed = true;
                        return;
                }
            }
            catch (Exception)
            {
                LoadFailed = true;
            }
        }

        protected async Task Save()
        {
            switch ((EditDataType)Datatype)
            {
                case EditDataType.CoreData:
                    if (CreationMode)
                        await CoreDataItemsService.CreateItemAsync((CoreDataItem)Item);
                    else
                        await CoreDataItemsService.SaveChangesAsync();
                    UriHelper.NavigateTo("/coredata");
                    break;
                case EditDataType.ShoppingList:
                    if (CreationMode)
                    {
                        ShoppingListItem shoppingListItem = (ShoppingListItem)Item;
                        shoppingListItem.CreationTimeStamp = DateTimeOffset.UtcNow;
                        await ShoppingListItemsService.CreateItemAsync(shoppingListItem);
                    }
                    else
                        await ShoppingListItemsService.SaveChangesAsync();
                    UriHelper.NavigateTo("/shoppinglist");
                    break;
                default:
                    break;
            }
        }

        private async Task<string> GetUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity.IsAuthenticated)
            {
                string userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
                return userId;
            }
            return null;
        }
    }
}
