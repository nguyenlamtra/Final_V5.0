using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels.Customer;
using COmpStoreApi.Helper;
using COmpStoreApi.Filters;
using Microsoft.AspNetCore.Authorization;
using COmpStore.Models.Enum;

namespace COmpStoreApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private ICustomerRepo Repo { get; set; }
        public CustomerController(ICustomerRepo repo)
        {
            Repo = repo;
        }

        [HttpGet]
        public IEnumerable<Customer> Get() => Repo.GetAll();

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = Repo.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        //=========================================
        [HttpGet("admin")]
        [Authorize(Policy = "Admin")]
        public IEnumerable<CustomerAdminIndex> GetCustomerAdminIndex()
            => Repo.GetCustomerAdminIndex();

        [HttpGet("admin/{id}")]
        [Authorize(Policy = "Admin")]
        public CustomerAdminDetails GetCustomerAdminDetails(int id)
            => Repo.GetCustomerAdminDetails(id);

        [HttpPost("admin")]
        [Authorize(Policy = "Admin")]
        [ValidateForm]
        public int AddCustomer([FromBody]CustomerAdminCreate model)
            => Repo.Add(new Customer
            {
                EmailAddress = model.EmailAddress,
                FullName = model.FullName,
                Password = StringHelper.EncryptPassword(model.Password),
                Role = model.Role.ToString()
            });

        [HttpPut("admin")]
        [Authorize(Policy = "Admin")]
        [ValidateForm]
        public int UpdateCustomer([FromBody]CustomerAdminUpdate model)
        {
            var customer = Repo.Find(model.Id);
            if (customer != null)
            {
                customer.EmailAddress = model.EmailAddress;
                customer.FullName = model.FullName;
                customer.Role = model.Role.ToString();
                return Repo.Update(customer);
            }
            return 0;
        }

        [HttpDelete("admin/{id}")]
        [Authorize(Policy = "Admin")]
        public int Delete(int id) => Repo.DeleteCustomer(id);

        [HttpGet("admin/update/{id}")]
        [Authorize(Policy = "Admin")]
        public CustomerAdminUpdate GetCustomerAdminUpdate(int id)
        {
            var customer = Repo.Find(id);
            return customer != null ? new CustomerAdminUpdate
            {
                EmailAddress = customer.EmailAddress,
                FullName = customer.FullName,
                Id = customer.Id,
                Role = (EnumRole)Enum.Parse(typeof(EnumRole), customer.Role)
            } : null;
        }
        //=========================================
    }
}