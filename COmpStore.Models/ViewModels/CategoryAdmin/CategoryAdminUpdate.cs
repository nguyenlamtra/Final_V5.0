using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace COmpStore.Models.ViewModels.CategoryAdmin
{
    public class CategoryAdminUpdate
    {
        [Required(ErrorMessage ="This field is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string CategoryName { get; set; }
    }
}
