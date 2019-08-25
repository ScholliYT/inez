using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace INEZ.Data.Entities
{
    public class Unit
    {
        [Key]
        [DisplayName("Einheit")]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
