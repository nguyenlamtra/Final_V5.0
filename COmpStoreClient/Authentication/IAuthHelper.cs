using COmpStore.Models.Entities;

namespace COmpStoreClient.Authentication
{
    public interface IAuthHelper
    {
        Customer GetCustomerInfo();
    }
}