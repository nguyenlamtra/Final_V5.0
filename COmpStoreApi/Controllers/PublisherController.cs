using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.ViewModels.Base;

namespace COmpStoreApi.Controllers
{
    [Route("api/[controller]")]
    public class PublisherController : Controller
    {
        private IPublisherRepo Repo { get; set; }
        private IProductRepo ProductRepo { get; set; }

        public PublisherController(IPublisherRepo repo, IProductRepo productRepo)
        {
            Repo = repo;
            ProductRepo = productRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Repo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = Repo.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Json(item);
        }

        [HttpGet("{publisherId}/products")]
        public IEnumerable<ProductAndPublisherBase> GetProductsForPublisher(int publisherId)
             => ProductRepo.GetProductsForPublisher(publisherId).ToList();
    }
}