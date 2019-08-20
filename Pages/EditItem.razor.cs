using System;
using System.Threading.Tasks;
using INEZ.Data;
using INEZ.Data.Entities;
using Microsoft.AspNetCore.Components;

namespace INEZ.Pages
{
    public class EditItemModel : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected ItemsService ItemsService { get; set; }

        [Parameter]
        protected Guid? Id { get; private set; } = null;
        protected string PageTitle { get; private set; }
        protected Item Item { get; private set; }



        protected override async Task OnParametersSetAsync()
        {
            if (!Id.HasValue || Id.Value == default)
            {
                PageTitle = "Eintrag hinzufügen";
                Item = new Item();
            }
            else
            {
                PageTitle = "Eintrag bearbeiten";

                Item = await ItemsService.GetItemAsync(Id.Value);
            }
        }

        protected async Task Save()
        {
            // TODO: Create or update
            await ItemsService.CreateItemAsync(Item);
            UriHelper.NavigateTo("/");
        }
    }
}