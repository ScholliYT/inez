using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace INEZ.Data.Entities
{
    public class ShoppingListItem : Item
    {
        public ShoppingListItem() : base()
        {
            CreationTimeStamp = DateTimeOffset.UtcNow;
        }

        [Required(ErrorMessage = "{0} wird benötigt")]
        public string OwnerId { get; set; }

        [Required(ErrorMessage = "{0} wird benötigt")]
        public DateTimeOffset CreationTimeStamp { get; set; }

        [Required(ErrorMessage = "{0} wird benötigt")]
        public bool Checked { get; set; } = false;
    }
}
