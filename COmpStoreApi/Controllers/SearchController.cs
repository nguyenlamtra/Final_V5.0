using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities.ViewModels.Base;

namespace COmpStoreApi.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private IProductRepo Repo { get; set; }
        public SearchController(IProductRepo repo)
        {
            Repo = repo;
        }

        [HttpGet("{searchString}", Name = "SearchProducts")]
        public IEnumerable<ProductAndSubCategoryBase> Search(string searchString) => Repo.Search(searchString);
        //pursuade%20anyone
    }
}