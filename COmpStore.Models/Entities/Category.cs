using COmpStore.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace COmpStore.Models.Entities
{
    [Table("Categories", Schema = "StoreComp")]
    public class Category : EntityBase
    {
        [DataType(DataType.Text), MaxLength(50)]
        public string CategoryName { get; set; }
        [InverseProperty(nameof(SubCategory.Category))]
        public List<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
