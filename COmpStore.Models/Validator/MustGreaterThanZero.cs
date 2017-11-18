using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace COmpStore.Models.Validator
{
    public class MustGreaterThanZero : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int? temp = value as int?;
            return (temp != null && temp.Value > 0) ? true : false;
        }
    }
}
