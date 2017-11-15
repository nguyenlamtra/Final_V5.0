using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace COmpStore.Models.ViewModels.ProductAdmin
{
    public class ProductAdminUpdate
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string ProductName { get; set; }

        public bool IsFeatured { get; set; }

        public string Number { get; set; }

        public string ProductImage { get; set; }

        public decimal UnitCost { get; set; }

        public decimal CurrentPrice { get; set; }

        public int UnitsInStock { get; set; }

        public int SubCategoryId { get; set; }

        public int PublisherId { get; set; }
    }
}
