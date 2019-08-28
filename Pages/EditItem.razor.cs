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

        [Parameter] public Guid Id { get; set; }
        [Parameter] public int? Datatype { get; set; }

        private string currentUserId;
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
                        Item = CreationMode ? new ShoppingListItem() : await ShoppingListItemsService.GetItemAsync(Id);
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

                if ((EditDataType)Datatype == EditDataType.ShoppingList && CreationMode)
                {
                    ShoppingListItem shoppingListItem = (ShoppingListItem)Item;
                    shoppingListItem.OwnerId = currentUserId;
                }
            }

            return true;
        }
    }
}
