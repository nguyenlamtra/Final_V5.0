using COmpStore.DAL.EF;
using COmpStore.DAL.Repos.Base;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities;
using COmpStore.Models.Entities.ViewModels.Base;
using COmpStore.Models.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COmpStore.DAL.Repos
{
    public class ProductRepo : RepoBase<Product>, IProductRepo
    {
        public ProductRepo(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public ProductRepo() : base()
        {
        }
        public override IEnumerable<Product> GetAll()
            => Table.OrderBy(x => x.ProductName);
        public override IEnumerable<Product> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.ProductName), skip, take);

        internal ProductAndSubCategoryBase GetRecordSub(Product p, SubCategory s)
            => new ProductAndSubCategoryBase()
            {
                SubCategoryName = s.SubCategoryName,
                SubCategoryId = p.SubCategoryId,
                //PublisherName = pub.PublisherName,
                //PublisherId = p.PublisherId,
                CurrentPrice = p.CurrentPrice,
                Description = p.Description,
                IsFeatured = p.IsFeatured,
                Id = p.Id,
                ProductName = p.ProductName,
                Number = p.Number,
                ProductImage = p.ProductImage,
                TimeStamp = p.TimeStamp,
                UnitCost = p.UnitCost,
                UnitsInStock = p.UnitsInStock
            };
        internal ProductAndPublisherBase GetRecordPub(Product p, Publisher pub)
            => new ProductAndPublisherBase()
            {


                PublisherName = pub.PublisherName,
                PublisherId = p.PublisherId,
                CurrentPrice = p.CurrentPrice,
                Description = p.Description,
                IsFeatured = p.IsFeatured,
                Id = p.Id,
                ProductName = p.ProductName,
                Number = p.Number,
                ProductImage = p.ProductImage,
                TimeStamp = p.TimeStamp,
                UnitCost = p.UnitCost,
                UnitsInStock = p.UnitsInStock


            };


        public IEnumerable<ProductAndPublisherBase> GetProductsForPublisher(int id)
            => Table
                .Where(p => p.PublisherId == id)
                .Include(p => p.Publisher)
                .Select(item => GetRecordPub(item, item.Publisher))
                .OrderBy(x => x.ProductName);
        public IEnumerable<ProductAndPublisherBase> GetAllWithPublisherName()
           => Table
               .Include(p => p.Publisher)
               .Select(item => GetRecordPub(item, item.Publisher))
               .OrderBy(x => x.ProductName);
        public IEnumerable<ProductAndSubCategoryBase> GetProductsForSubCategory(int id)
            => Table
                .Where(p => p.SubCategoryId == id)
                .Include(p => p.SubCategory)
                .Select(item => GetRecordSub(item, item.SubCategory))
                .OrderBy(x => x.ProductName);


        public IEnumerable<ProductAndSubCategoryBase> GetAllWithSubCategoryName()
            => Table
                .Include(p => p.SubCategory)
                .Select(item => GetRecordSub(item, item.SubCategory))
                .OrderBy(x => x.ProductName);

        public IEnumerable<ProductAndSubCategoryBase> GetFeaturedWithSubCategoryName()
            => Table
                .Where(p => p.IsFeatured)
                .Include(p => p.Publisher)
                .Select(item => GetRecordSub(item, item.SubCategory))
                .OrderBy(x => x.ProductName);

        public ProductAndSubCategoryBase GetOneWithSubCategoryName(int id)
            => Table
                .Where(p => p.Id == id)
                .Include(p => p.Publisher)
                .Select(item => GetRecordSub(item, item.SubCategory))
                .SingleOrDefault();

        public IEnumerable<ProductAndSubCategoryBase> Search(string searchString)
            => Table
                .Where(p =>
                    p.SubCategory.SubCategoryName.ToLower().Contains(searchString.Trim().ToLower())
                    ||p.Publisher.PublisherName.ToLower().Contains(searchString.Trim().ToLower())
                    || p.ProductName.ToLower().Contains(searchString.Trim().ToLower()))
                .Include(p =>p.SubCategory)
                .Select(item => GetRecordSub(item, item.SubCategory))
                .OrderBy(x => x.ProductName);
    }
}
