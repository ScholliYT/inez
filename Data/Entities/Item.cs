using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace INEZ.Data.Entities
{
    public abstract class Item
    {
        public Item()
        {
            Id = Guid.NewGuid();
        }

        [Key] public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} wird benötigt")]
        [DisplayName("Name")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "{0} muss eine Länge zwischen {2} und {1} Zeichen haben")]
        public string Name { get; set; }

        [DisplayName("Menge")]
        [StringLength(127, ErrorMessage = "{0} muss eine Länge zwischen {2} und {1} Zeichen haben")]
        public string Quantity { get; set; }
    }
}