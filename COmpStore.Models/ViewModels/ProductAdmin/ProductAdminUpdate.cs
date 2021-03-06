﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace COmpStore.Models.ViewModels.ProductAdmin
{
    public class ProductAdminUpdate
    {
        [Required]
        public int Id { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(30)]
        public string ProductName { get; set; }

        public bool IsFeatured { get; set; }

        [Required]
        public string Number { get; set; }

        public string ProductImage { get; set; }

        [Required]
        public decimal UnitCost { get; set; }

        [Required]
        public decimal CurrentPrice { get; set; }

        [Required]
        public int UnitsInStock { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        [Required]
        public int PublisherId { get; set; }
    }
}
