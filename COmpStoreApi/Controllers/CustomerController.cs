using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities;

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
    }
}