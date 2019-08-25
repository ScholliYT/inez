using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace INEZ.Data.Entities
{
    public class Item
    {
        public Item()
        {
            Id = Guid.NewGuid();
            BaseQuantity = new Quantity();
        }

        [Key] public Guid Id { get; set; }

        [Required]
        [DisplayName("Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Der Name muss eine Länge zwischen 3 und 30 Zeichen haben")]
        public string Name { get; set; }

        [DisplayName("Beschreibung")]
        [StringLength(100, ErrorMessage = "Die Beschreibung darf maximal 100 Zeichen lang sein")]
        public string Description { get; set; }

        [Required] public Quantity BaseQuantity { get; set; }
    }
}