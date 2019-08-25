using System;
using System.Threading.Tasks;
using INEZ.Data;
using INEZ.Data.Entities;
using Microsoft.AspNetCore.Components;

namespace INEZ.Pages
{
    public class EditItemModel : ComponentBase
    {
        private bool _creationMode;

        [Inject] protected IUriHelper UriHelper { get; set; }

        [Inject] protected ItemsService ItemsService { get; set; }

        [Parameter] public string Id { get; set; } = null;

        protected string PageTitle
        {
            get
            {
                if (_creationMode)
                    return "Eintrag hinzufügen";
                return "Eintrag bearbeiten";
            }
        }

        protected Item Item { get; private set; }


        protected override async Task OnParametersSetAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Item = new Item();
                _creationMode = true;
            }
            else
            {
                _creationMode = false;
                Item = await ItemsService.GetItemAsync(Guid.Parse(Id));
            }
        }

        protected async Task Save()
        {
            // TODO: Add model validation
            if (_creationMode)
                await ItemsService.CreateItemAsync(Item);
            else
                await ItemsService.SaveChangesAsync();
            UriHelper.NavigateTo("/einkaufsliste");
        }
    }
}