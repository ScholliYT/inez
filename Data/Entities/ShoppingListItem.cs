using System;
using System.Collections.Generic;
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

        public string OwnerId { get; set; }
        public DateTimeOffset CreationTimeStamp { get; set; }
        public bool Checked { get; set; }
    }
}
