using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities.ViewModels.Base;
using COmpStore.Models.ViewModels.Base;

namespace COmpStoreApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IProductRepo Repo { get; set; }
        public ProductController(IProductRepo repo)
        {
            Repo = repo;
        }

        [HttpGet]
        public IEnumerable<ProductAndSubCategoryBase> Get()
            => Repo.GetAllWithSubCategoryName().ToList();
        [Route("[controller]/[action]")]
       

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = Repo.GetOneWithSubCategoryName(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("featured")]
        public IEnumerable<ProductAndSubCategoryBase> GetFeatured()
            => Repo.GetFeaturedWithSubCategoryName().ToList();
    }
}