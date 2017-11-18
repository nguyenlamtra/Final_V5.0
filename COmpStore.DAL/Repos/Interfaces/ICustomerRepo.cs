using COmpStore.DAL.Repos.Base;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.DAL.Repos.Interfaces
{
    public interface ICustomerRepo : IRepo<Customer>
    {
        Customer GetCustomerByEmail(string email);
        //======================================================
        int GetCustomerIdByEmailAddressAsNoTracking(string emailAddress);
        int DeleteCustomer(int id, bool persist = true);
        IEnumerable<CustomerAdminIndex> GetCustomerAdminIndex();
        CustomerAdminDetails GetCustomerAdminDetails(int id);
        //======================================================
    }
}
