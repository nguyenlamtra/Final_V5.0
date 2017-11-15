using COmpStore.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace COmpStore.Models.ViewModels.Base
{
    public class SubCategoryAndCategoryBase  : EntityBase
    {
        public int CategoryId { get; set; }
        [Display(Name = "CategoryName")]
        public string CategoryName { get; set; }
        //public int SubCategoryId { get; set; }
        [Display(Name = "SubCategoryName")]
        public string SubCategoryName { get; set; }
    }
}
