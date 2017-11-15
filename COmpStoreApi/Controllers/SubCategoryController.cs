using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities.ViewModels.Base;
using COmpStore.Models.ViewModels.Base;
using Microsoft.AspNetCore.Authorization;
using COmpStore.Models.ViewModels.SubCategoryAdmin;
using COmpStoreApi.Filters;
using COmpStore.Models.Entities;

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

        //==================================================================================================

        [HttpGet("admin")]
        [Authorize(Policy = "Admin")]
        public IEnumerable<SubCategoryAdminIndex> GetAdminSubCategoryIndex() => Repo.GetSubCategoryAdminIndex();

        [HttpGet("admin/{id}")]
        [Authorize(Policy = "Admin")]
        public SubCategoryAdminDetails GetAdminSubCategoryDetails(int id) => Repo.GetSubCategoryAdminDetails(id);

        [HttpGet("admin/update/{id}")]
        [Authorize(Policy = "Admin")]
        public SubCategoryAdminUpdate GetAdminSubCategoryUpdate(int id)
        {
            var sub = Repo.Find(id);
            return new SubCategoryAdminUpdate { Id = sub.Id, Name = sub.SubCategoryName, CategoryId = sub.CategoryId };
        }

        [HttpPost("admin")]
        [ValidateForm]
        [Authorize(Policy = "Admin")]
        public int CreateSubCategory([FromBody]SubCategoryAdminCreate model) => Repo.Add(new SubCategory
        {
            CategoryId = model.CategoryId,
            SubCategoryName = model.Name
        });

        [HttpPut("admin")]
        [ValidateForm]
        [Authorize(Policy = "Admin")]
        public int UpdateSubCategory([FromBody]SubCategoryAdminUpdate model)
        {
            var sub = Repo.Find(model.Id);
            if (sub != null)
            {
                sub.CategoryId = model.CategoryId;
                sub.SubCategoryName = model.Name;
                Repo.Update(sub);
                return 1;
            }
            return 0;
        }

        [HttpDelete("admin/{id}")]
        [Authorize(Policy = "Admin")]
        public int DeleteSubCategory(int id)
        {
            var sub = Repo.Find(id);
            if (sub != null)
            {
                Repo.Delete(sub);
                return 1;
            }
            return 0;
        }

        [HttpGet("admin/combobox")]
        public IEnumerable<SubCategoryCombobox> GetSubCategoryCombobox()
            => Repo.GetSubCategoryCombobox();
        //==================================================================================================
    }
}