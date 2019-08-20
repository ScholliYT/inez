using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Search;

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
        [IsSearchable, IsFilterable, IsFacetable]
        public string Name { get; set; }
        [DisplayName("Beschreibung")]
        [StringLength(100)]
        [IsSearchable]
        public string Description { get; set; }
    }
}
