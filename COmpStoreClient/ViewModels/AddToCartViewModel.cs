using COmpStoreClient.Validation;
using COmpStoreClient.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStoreClient.ViewModels
{
    public class AddToCartViewModel : CartViewModelBase
    {
        [MustNotBeGreaterThan(nameof(UnitsInStock)), MustBeGreaterThanZero]
        public int Quantity { get; set; }
    }
}
