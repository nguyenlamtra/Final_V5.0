using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.Models.ViewModels.Base;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities;

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
    }
}