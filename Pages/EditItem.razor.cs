using System;
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
        [Inject] protected IHttpContextAccessor HTTPContextAccessor { get; set; }
        [Inject] protected UserManager<IdentityUser> UserManager { get; set; }


        [Parameter] public Guid Id { get; set; }

        [Parameter] public EditDataType Datatype { get; set; }

        protected bool LoadFailed { get; private set; }

        protected Item Item { get; private set; }

        protected bool CreationMode { get; private set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                CreationMode = Id == default;

                switch (Datatype)
                {
                    case EditDataType.CoreData:
                        Item = CreationMode ? new CoreDataItem() : await CoreDataItemsService.GetItemAsync(Id);
                        break;
                    case EditDataType.ShoppingList:


                        Item = CreationMode ? new ShoppingListItem() : await ShoppingListItemsService.GetItemAsync(Id);
                        break;
                    default:
                        LoadFailed = true;
                        break;
                }
            }
            catch (Exception)
            {
                LoadFailed = true;
            }
        }

        private async Task<IdentityUser> GetCurrentUserAsync() => await UserManager.GetUserAsync(HTTPContextAccessor.HttpContext.User);

        protected async Task Save()
        {
            switch (Datatype)
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
                        var user = await GetCurrentUserAsync();
                        if (user != null)
                        {
                            ShoppingListItem shoppingListItem = (ShoppingListItem)Item;
                            shoppingListItem.OwnerId = user.Id;
                            await ShoppingListItemsService.CreateItemAsync(shoppingListItem);
                        }
                    }
                    else
                        await ShoppingListItemsService.SaveChangesAsync();
                    UriHelper.NavigateTo("/shoppinglist");
                    break;
                default:
                    break;
            }
        }
    }
}
