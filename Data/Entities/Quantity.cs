using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace INEZ.Data.Entities
{
    public class Quantity
    {
        public Quantity()
        {
            Id = Guid.NewGuid();
        }

        [Key] public Guid Id { get; set; }

        [Required]
        [DisplayName("Menge")]
        [Range(0, float.MaxValue, ErrorMessage = "Die Menge muss positiv sein")]
        public float Count { get; set; }

        [Required]
        [DisplayName("Einheit")]
        [StringLength(20)]
        public string Unit { get; set; }

        [DisplayName("Menge")] public string DisplayValue => ToString();


        public override string ToString()
        {
            return $"{Count} {Unit}";
        }
    }
}