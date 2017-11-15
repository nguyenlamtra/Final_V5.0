using COmpStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStoreClient.ViewModels
{
    public class CartViewModel
    {
        public Customer Customer { get; set; }
        public IList<CartRecordViewModel> CartRecords { get; set; }
    }
}
