using COmpStore.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace COmpStore.Models.Entities
{
    [Table("Orders", Schema = "StoreComp")]
    public class Order : EntityBase
    {
        public int CustomerId { get; set; }
        [Display(Name = "Total")]
        public decimal? OrderTotal { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Ordered")]
        public DateTime OrderDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Shipped")]
        public DateTime ShipDate { get; set; }
        public string Status { get; set; }
        [StringLength(20, MinimumLength = 8)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [InverseProperty("Order")]
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
