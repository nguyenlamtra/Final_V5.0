using COmpStore.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace COmpStore.Models.Entities
{
    [Table("SubCategories", Schema = "StoreComp")]
    public class SubCategory : EntityBase
    {
        [DataType(DataType.Text), MaxLength(50)]
        public string SubCategoryName { get; set; }
        [InverseProperty(nameof(Product.SubCategory))]
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();

    }
}
