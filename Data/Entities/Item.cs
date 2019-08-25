using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INEZ.Data.Entities
{
    public class Item
    {
        public Item()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        [DisplayName("Name")]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        [DisplayName("Beschreibung")]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public Quantity BaseQuantity { get; set; }
    }
}
