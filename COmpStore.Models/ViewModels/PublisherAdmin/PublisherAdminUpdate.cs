using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace COmpStore.Models.ViewModels.PublisherAdmin
{
    public class PublisherAdminUpdate
    {
        [Required]
        public int Id { get; set; }
        [StringLength(30)]
        [Required]
        public string Name { get; set; }
    }
}
