using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace COmpStore.Models.ViewModels.Cart
{
    public class OrderModel
    {
        [Required]
        public List<SelectedProduct> SelectedProducts { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, MinimumLength = 20)]
        public string Address { get; set; }
        [StringLength(20, MinimumLength = 9)]
        [Required(ErrorMessage = "This field is required")]
        public string Phone { get; set; }
    }
}
