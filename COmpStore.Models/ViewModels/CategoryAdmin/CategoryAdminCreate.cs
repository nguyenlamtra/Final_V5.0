using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace COmpStore.Models.ViewModels.CategoryAdmin
{
    public class CategoryAdminCreate
    {
        [Required(ErrorMessage = "This field is required")]
        [StringLength(30)]
        public string CategoryName { get; set; }
    }
}
