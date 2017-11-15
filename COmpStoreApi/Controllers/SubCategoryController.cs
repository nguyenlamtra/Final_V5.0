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
    public class SubCategoryController : Controller
    {
            private ISubCategoryRepo Repo { get; set; }
            private IProductRepo ProductRepo { get; set; }

            public SubCategoryController(ISubCategoryRepo repo, IProductRepo productRepo)
            {
                Repo = repo;
                ProductRepo = productRepo;
            }

       

        [HttpGet]
        public IEnumerable<SubCategoryAndCategoryBase> Get()
            => Repo.GetAllWithCategoryName().ToList();

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = Repo.GetOneWithCategoryName(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("{subcategoryId}/products")]
        public IEnumerable<ProductAndSubCategoryBase> GetProductsForSubCategory(int subcategoryId)
            => ProductRepo.GetProductsForSubCategory(subcategoryId).ToList();
    }
}