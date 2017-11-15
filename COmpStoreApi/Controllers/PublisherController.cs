using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.ViewModels.Base;
using Microsoft.AspNetCore.Authorization;
using COmpStore.Models.ViewModels.PublisherAdmin;
using COmpStore.Models.Entities;

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

        //=================================
        [Authorize(Policy = "Admin")]
        [HttpGet("admin")]
        public IEnumerable<PublisherAdminIndex> GetAdminPublisherIndex()
        => Repo.GetForAdminPublisherIndex();

        [HttpGet("admin/{id}")]
        [Authorize(Policy = "Admin")]
        public PublisherAdminDetails GetAdminPublisherDetails(int id) =>
            Repo.GetForAdminPublisherDetails(id);

        [HttpPost("admin")]
        [Authorize(Policy = "Admin")]
        public int InsertPublisher([FromBody]PublisherAdminCreate publisher)
        => Repo.Add(new Publisher { PublisherName = publisher.Name });

        [HttpPut("admin")]
        [Authorize(Policy = "Admin")]
        public int UpdatePublisher([FromBody]PublisherAdminUpdate Publisher)
        {
            var cat = Repo.Find(Publisher.Id);
            if (cat != null)
            {
                cat.PublisherName = Publisher.Name;
                Repo.Update(cat);
                return 1;
            }
            return 0;
        }

        [HttpDelete("admin/{id}")]
        [Authorize(Policy = "Admin")]
        public int Delete(int id)
        {
            try
            {
                var cat = Repo.Find(id);
                if (cat != null)
                    Repo.Delete(cat);
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        [HttpGet("admin/update/{id}")]
        [Authorize(Policy = "Admin")]
        public PublisherAdminUpdate GetAdminPublisherUpdate(int id)
        {
            var cat = Repo.Find(id);
            return new PublisherAdminUpdate { Id = cat.Id, Name = cat.PublisherName };
        }

        [HttpGet("admin/combobox")]
        [Authorize(Policy = "Admin")]
        public IEnumerable<PublisherCombobox> GetPublisherCombobox() => Repo.GetPublisherCombobox();

        //=================================
    }
}