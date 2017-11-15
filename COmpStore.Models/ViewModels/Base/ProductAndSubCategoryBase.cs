using COmpStore.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace COmpStore.Models.Entities.ViewModels.Base
{
    public class ProductAndSubCategoryBase : EntityBase
    {
        public int SubCategoryId { get; set; }
        [Display(Name = "SubCategory")]
        public string SubCategoryName { get; set; }
        public int PublisherId { get; set; }
        [Display(Name = "PublisherName")]
        public string PublisherName { get; set; }
        public int ProductId { get; set; }
        [MaxLength(3800)]
        public string Description { get; set; }
        [MaxLength(50)]
        [Display(Name = "ProductName")]
        public string ProductName { get; set; }
        [Display(Name = "Is Featured Product")]
        public bool IsFeatured { get; set; }
        [MaxLength(50)]
        [Display(Name = "Number")]
        public string Number { get; set; }
        [MaxLength(150)]
        public string ProductImage { get; set; }
        [DataType(DataType.Currency), Display(Name = "Cost")]
        public decimal UnitCost { get; set; }
        [DataType(DataType.Currency), Display(Name = "Price")]

        public decimal CurrentPrice { get; set; }
        [Display(Name = "In Stock")]
        public int UnitsInStock { get; set; }
    }
}
