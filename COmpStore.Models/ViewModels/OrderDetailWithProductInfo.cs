using COmpStore.Models.Entities.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace COmpStore.Models.ViewModels
{
    public class OrderDetailWithProductInfo : ProductAndSubCategoryBase
    {
        public int OrderId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [DataType(DataType.Currency), Display(Name = "Total")]
        public decimal? LineItemTotal { get; set; }
    }

}
