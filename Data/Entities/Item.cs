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
        }

        [Key] public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} wird benötigt")]
        [DisplayName("Name")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "{0} muss eine Länge zwischen {2} und {1} Zeichen haben")]
        public string Name { get; set; }

        [DisplayName("Generischer Name")]
        [StringLength(255, ErrorMessage = "{0} muss eine Länge zwischen {2} und {1} Zeichen haben")]
        public string GenericName { get; set; }

        [DisplayName("Menge")]
        [StringLength(127, ErrorMessage = "{0} muss eine Länge zwischen {2} und {1} Zeichen haben")]
        public string Quantity { get; set; }

        [DisplayName("Marke(n)")]
        [StringLength(127, ErrorMessage = "{0} muss eine Länge zwischen {2} und {1} Zeichen haben")]
        public string Brands { get; set; }

        public ulong EAN { get; set; }

        [StringLength(255)]
        public string DatasourceUrl { get; set; }

        [StringLength(255)]
        public string ImageUrl { get; set; }

        [StringLength(255)]
        public string ImageSmallUrl { get; set; }
    }
}