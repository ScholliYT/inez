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
        protected string Id { get; private set; } = null;
        protected string PageTitle
        {
            get
            {
                if (creationMode)
                {
                    return "Eintrag hinzufügen";
                }
                else
                {
                    return "Eintrag bearbeiten";
                }
            }
        }
        protected Item Item { get; private set; }

        private bool creationMode;


        protected override async Task OnParametersSetAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Item = new Item();
                creationMode = true;
            }
            else
            {
                creationMode = false;
                Item = await ItemsService.GetItemAsync(Guid.Parse(Id));
            }
        }

        protected async Task Save()
        {
            if (creationMode)
            {
                await ItemsService.CreateItemAsync(Item);
            }
            else
            {
                await ItemsService.SaveChangesAsync();
            }
            UriHelper.NavigateTo("/einkaufsliste");
        }
    }
}