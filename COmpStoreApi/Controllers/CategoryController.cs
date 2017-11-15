using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.Models.ViewModels.Base;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using COmpStore.Models.ViewModels.CategoryAdmin;
using COmpStoreApi.Filters;

namespace COmpStoreApi.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private ICategoryRepo Repo { get; set; }
        private ISubCategoryRepo SubCategoryRepo { get; set; }

        public CategoryController(ICategoryRepo repo, ISubCategoryRepo subCategoryRepo)
        {
            Repo = repo;
            SubCategoryRepo = subCategoryRepo;
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

        [HttpGet("{categoryId}/subcategories")]
        public IEnumerable<SubCategoryAndCategoryBase> GetSubCategoriesForCategory(int categoryId)
            =>   SubCategoryRepo.GetSubCategoriesForCategory(categoryId).ToList();
        //=================================
        [HttpGet("admin")]
        [Authorize(Policy = "Admin")]
        public IEnumerable<CategoryAdminIndex> GetAdminCategoryIndex()
        => Repo.GetAdminCategoryIndex();

        [HttpGet("admin/{id}")]
        [Authorize(Policy = "Admin")]
        public CategoryAdminDetails GetAdminCategoryDetails(int id) =>
            Repo.GetAdminCategoryDetails(id);

        [HttpPost("admin")]
        [ValidateForm]
        [Authorize(Policy = "Admin")]
        public int InsertCategory([FromBody]CategoryAdminCreate category)
        => Repo.Add(new Category { CategoryName = category.CategoryName });

        [HttpPut("admin")]
        [ValidateForm]
        [Authorize(Policy = "Admin")]
        public int UpdateCategory([FromBody]CategoryAdminUpdate category)
        {
            var cat = Repo.Find(category.Id);
            if (cat != null)
            {
                cat.CategoryName = category.CategoryName;
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
        public CategoryAdminUpdate GetAdminCategoryUpdate(int id)
        {
            var cat = Repo.Find(id);
            return new CategoryAdminUpdate { Id = cat.Id, CategoryName = cat.CategoryName };
        }

        //=================================
    }
}