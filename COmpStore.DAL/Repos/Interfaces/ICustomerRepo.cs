using COmpStore.DAL.Repos.Base;
using COmpStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.DAL.Repos.Interfaces
{
    public interface ICustomerRepo : IRepo<Customer>
    {
        Customer GetCustomerByEmail(string email);
    }
}
