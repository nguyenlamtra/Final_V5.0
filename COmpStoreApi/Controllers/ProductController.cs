using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities.ViewModels.Base;
using COmpStore.Models.ViewModels.Base;
using COmpStore.Models.ViewModels.ProductAdmin;
using Microsoft.AspNetCore.Authorization;
using COmpStore.Models.ViewModels.Paging;
using COmpStoreApi.Filters;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text.RegularExpressions;
using COmpStore.Models.Entities;
using COmpStoreApi.Helper;
using COmpStore.Models.ViewModels.Cart;

namespace COmpStoreApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IProductRepo Repo { get; set; }
        private IHostingEnvironment _hostingEnvironment { get; set; }
        public ProductController(IProductRepo repo, IHostingEnvironment hostingEnvironment)
        {
            Repo = repo;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IEnumerable<ProductAndSubCategoryBase> Get()
            => Repo.GetAllWithSubCategoryName().ToList();
        //[Route("[controller]/[action]")]


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

        //================================================================================================================

        [HttpGet("admin")]
        [Authorize(Policy = "Admin")]
        public PageOutput<ProductAdminIndex> GetProductAdminIndex(int pageNumber = 1, int pageSize = 2)
        => Repo.GetProductAdminIndex(pageNumber, pageSize);


        [HttpGet("admin/update/{id}")]
        [Authorize(Policy = "Admin")]
        public ProductAdminUpdate GetProductAdminUpdate(int id)
        {
            var product = Repo.Find(id);

            return product == null ? null : new ProductAdminUpdate
            {
                CurrentPrice = product.CurrentPrice,
                Description = product.Description,
                Id = product.Id,
                IsFeatured = product.IsFeatured,
                Number = product.Number,
                ProductImage = product.ProductImage,
                ProductName = product.ProductName,
                PublisherId = product.PublisherId,
                SubCategoryId = product.SubCategoryId,
                UnitCost = product.UnitCost,
                UnitsInStock = product.UnitsInStock
            };
        }

        [HttpPost("admin")]
        [ValidateForm]
        [Authorize(Policy = "Admin")]
        public async Task<int> CreateProduct([FromBody]ProductAdminCreate model)
        {
            if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
            {
                _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            var filePath = _hostingEnvironment.WebRootPath + "\\images";
            var base64String = Regex.Replace(model.ProductImage, "^data:image\\/[a-zA-Z]+;base64,", String.Empty);
            if (base64String.IsBase64())
            {
                var product = new Product
                {
                    ProductImage = Guid.NewGuid() + ".jpg",
                    CurrentPrice = model.CurrentPrice,
                    UnitsInStock = model.UnitsInStock,
                    Description = model.Description,
                    IsFeatured = model.IsFeatured,
                    Number = model.Number,
                    ProductName = model.ProductName,
                    PublisherId = model.PublisherId,
                    SubCategoryId = model.SubCategoryId,
                    UnitCost = model.UnitCost,
                };



                if (Repo.Add(product) == 0)
                {
                    throw new Exception("something went wrong when adding a new subcategory");
                }
                await FileHelper.AddFileAsync(filePath, base64String, product.ProductImage);

                return 1;
            }
            else
            {
                return 2;
            }
        }

        [HttpPut("admin")]
        [ValidateForm]
        [Authorize(Policy = "Admin")]
        public async Task<int> UpdateProduct([FromBody]ProductAdminUpdate model)
        {
            var product = Repo.Find(model.Id);
            if (product != null)
            {
                if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
                {
                    _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }
                var filePath = _hostingEnvironment.WebRootPath + "\\images";
                var base64String = Regex.Replace(model.ProductImage, "^data:image\\/[a-zA-Z]+;base64,", String.Empty);
                if (base64String.IsBase64())
                {
                    var imageName = Repo.GetImageProduct(product.Id);
                    if (imageName != null)
                        FileHelper.DeleteFile(filePath, imageName);

                    product.ProductImage = Guid.NewGuid() + ".jpg";
                    product.UnitsInStock = model.UnitsInStock;
                    product.CurrentPrice = model.CurrentPrice;
                    product.Description = model.Description;
                    product.IsFeatured = model.IsFeatured;
                    product.Number = model.Number;
                    product.ProductName = model.ProductName;
                    product.PublisherId = model.PublisherId;
                    product.SubCategoryId = model.SubCategoryId;
                    product.UnitCost = model.UnitCost;

                    ;
                    if (Repo.Update(product) == 0)
                        throw new Exception("something went wrong when UpdateProduct");
                    await FileHelper.AddFileAsync(filePath, base64String, product.ProductImage);
                    return 1;
                }
                else
                {

                    if (Repo.UpdateExceptImage(product) == 0)
                        throw new Exception("something went wrong when adding a new subcategory");
                    else
                        return 1;
                }
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

        [HttpGet("cart/{id}")]
        public CartModel GetCartView(int id)
        {
            try
            {
                return Repo.GetCartView(id);
            }
            catch(Exception e)
            {
                Console.Write(e.ToString());
                return null;
            }
        } 

        [HttpPut("admin/change-isfeature")]
        public int ChangeIsFeature([FromBody]ProductAdminUpdateIsFeature model)
        {
            var product = Repo.Find(model.Id);
            if (product != null)
            {
                product.IsFeatured = !product.IsFeatured;
                Repo.Update(product);
                return 1;
            }
            return 0;
        }

        //================================================================================================================
    }
}