using COmpStoreClient.Validation;
using COmpStoreClient.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStoreClient.ViewModels
{
    public class CartRecordViewModel : CartViewModelBase
    {
        [MustNotBeGreaterThan(nameof(UnitsInStock))]
        public int Quantity { get; set; }
    }
}
