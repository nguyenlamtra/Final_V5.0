using COmpStore.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace COmpStore.Models.Entities
{
    [Table("Products", Schema = "StoreComp")]
    public class Product : EntityBase
    {
        [MaxLength(3800)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string ProductName { get; set; }
        public bool IsFeatured { get; set; }
        [MaxLength(50)]
        public string Number { get; set; }
        [MaxLength(150)]
        public string ProductImage { get; set; }
        [MaxLength(150)]
       
        [DataType(DataType.Currency)]
        public decimal UnitCost { get; set; }
        [DataType(DataType.Currency)]
        public decimal CurrentPrice { get; set; }
        public int UnitsInStock { get; set; }
        [Required]
        public int SubCategoryId { get; set; }
        [ForeignKey(nameof(SubCategoryId))]
        public SubCategory SubCategory { get; set; }
        [Required]
        public int PublisherId { get; set; }
        [ForeignKey(nameof(PublisherId))]
        public Publisher Publisher { get; set; }

        [InverseProperty(nameof(OrderDetail.Product))]
        public List<OrderDetail> OrderDetails { get; set; }
            = new List<OrderDetail>();
    }
}
