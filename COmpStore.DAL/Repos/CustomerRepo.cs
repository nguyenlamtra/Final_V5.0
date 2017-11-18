using COmpStore.DAL.EF;
using COmpStore.DAL.Repos.Base;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels.Customer;
using COmpStore.Models.ViewModels.OrderAdmin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COmpStore.DAL.Repos
{
    public class CustomerRepo : RepoBase<Customer>, ICustomerRepo
    {
        public CustomerRepo(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public CustomerRepo() : base()
        {
        }

        public override IEnumerable<Customer> GetAll()
            => Table.Where(x => x.IsDeleted == false).OrderBy(x => x.FullName);

        public override IEnumerable<Customer> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.FullName), skip, take);
        //==========
        public Customer GetCustomerByEmail(string email) => Table.Where(x => x.IsDeleted == false).SingleOrDefault(c => c.EmailAddress == email);

        public int GetCustomerIdByEmailAddressAsNoTracking(string emailAddress) => Table.Where(x => x.IsDeleted == false).AsNoTracking().SingleOrDefault(x => x.EmailAddress == emailAddress).Id;

        public int DeleteCustomer(int id, bool persist = true)
        {
            var customer = Db.Customers.Include(x => x.Orders).ThenInclude(x => x.OrderDetails).SingleOrDefault(x => x.Id == id);
            if (customer != null)
            {
                customer.IsDeleted = true;
                customer.Orders.ForEach(x => { x.OrderDetails.ForEach(y => y.IsDeleted = true); x.IsDeleted = true; });
                Db.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public IEnumerable<CustomerAdminIndex> GetCustomerAdminIndex()
        => Table.Where(c => c.IsDeleted == false).Select(c => new CustomerAdminIndex
        {
            EmailAddress = c.EmailAddress,
            Id = c.Id,
            Name = c.FullName,
            SumOrders = c.Orders.Count
        }).ToList();

        internal IEnumerable<OrderRelate> GetOrder(IEnumerable<Order> orders) => orders.Select(o => new OrderRelate
        {
            Id = o.Id,
            OrderDate = o.OrderDate,
            OrderTotal = o.OrderTotal ?? 0
        });

        public CustomerAdminDetails GetCustomerAdminDetails(int id)
        {
            return Table.Include(c=>c.Orders).Where(c => c.IsDeleted == false).Select(c => new CustomerAdminDetails
            {
                EmailAddress = c.EmailAddress,
                FullName = c.FullName,
                Id = c.Id,
                Role = c.Role,
                Orders = GetOrder(c.Orders)
            }).SingleOrDefault(c => c.Id == id);
        }


        //===========
    }

}
