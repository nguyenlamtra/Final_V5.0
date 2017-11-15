using COmpStore.Models.Entities.ViewModels.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStoreClient.ViewModels.Base
{
    public class CartViewModelBase : ProductAndSubCategoryBase
    {
        public int? CustomerId { get; set; }

        [DataType(DataType.Currency), Display(Name = "Total")]
        public decimal LineItemTotal { get; set; }
        public string TimeStampString =>
           TimeStamp != null ? JsonConvert.SerializeObject(TimeStamp).Replace("\"", "") : string.Empty;
    }
}
