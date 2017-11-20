using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace COmpStore.Models.ViewModels.PublisherAdmin
{
    public class PublisherAdminCreate
    {
        [StringLength(30)]
        public string Name { get; set; }
    }
}
