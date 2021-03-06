﻿using COmpStore.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace COmpStore.Models.ViewModels.Customer
{
    public class CustomerAdminCreate
    {
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public EnumRole Role { get; set; }

        [Compare("Password")]
        [StringLength(50)]
        public string ConfirmPassword { get; set; }
    }
}
