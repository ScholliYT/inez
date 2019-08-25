using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace INEZ.Data.Entities
{
    public class Quantity
    {
        public Quantity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Menge")]
        public float Count { get; set; }

        [Required]
        [DisplayName("Einheit")]
        public Unit Unit { get; set; }


        public override string ToString()
        {
            return $"{Count} {Unit.Name}";
        }

        [DisplayName("Menge")]
        public string DisplayValue
        {
            get
            {
                return this.ToString();
            }
        }
    }
}
