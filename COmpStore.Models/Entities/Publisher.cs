using COmpStore.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace COmpStore.Models.Entities
{
    [Table("Publishers", Schema = "StoreComp")]
    public class Publisher : EntityBase
    {
        [DataType(DataType.Text), MaxLength(50)]
        public string PublisherName { get; set; }
        [InverseProperty(nameof(Product.Publisher))]
        public List<Product> Products { get; set; } = new List<Product>();

    }
}
