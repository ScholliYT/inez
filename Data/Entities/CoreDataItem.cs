using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace INEZ.Data.Entities
{
    public class CoreDataItem : Item
    {
        public CoreDataItem() : base()
        {

        }

        [DisplayName("Generischer Name")]
        [StringLength(255, ErrorMessage = "{0} muss eine Länge zwischen {2} und {1} Zeichen haben")]
        public string GenericName { get; set; }

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
