using System;
using System.Threading.Tasks;
using INEZ.Data;
using INEZ.Data.Entities;
using INEZ.Data.Services;
using Microsoft.AspNetCore.Components;

namespace INEZ.Pages
{
    public class EditItemModel : ComponentBase
    {
        [Inject] protected IUriHelper UriHelper { get; set; }
        [Inject] protected CoreDataItemsService CoreDataItemsService { get; set; }

        private bool _creationMode;

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

        protected CoreDataItem Item { get; private set; }

        protected override async Task OnParametersSetAsync()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Item = new CoreDataItem();
                _creationMode = true;
            }
            else
            {
                _creationMode = false;
                Item = await CoreDataItemsService.GetItemAsync(Guid.Parse(Id));
            }
        }

        protected async Task Save()
        {
            // TODO: Add model validation
            if (_creationMode)
                await CoreDataItemsService.CreateItemAsync(Item);
            else
                await CoreDataItemsService.SaveChangesAsync();
            UriHelper.NavigateTo("/coredata");
        }
    }
}