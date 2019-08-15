using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace INEZ.Data.Entities
{
    public class TestModel
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Test Eigenschaft")]
        public string TestProp { get; set; }
    }
}