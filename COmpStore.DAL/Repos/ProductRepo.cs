﻿using COmpStore.DAL.EF;
using COmpStore.DAL.Repos.Base;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities;
using COmpStore.Models.Entities.ViewModels.Base;
using COmpStore.Models.ViewModels.Base;
using COmpStore.Models.ViewModels.Paging;
using COmpStore.Models.ViewModels.ProductAdmin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COmpStore.Models.ViewModels.Cart;

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
            => Table.Where(x => x.IsDeleted == false).OrderBy(x => x.ProductName);
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
            => Table.Where(x => x.IsDeleted == false)
                .Where(p => p.PublisherId == id)
                .Include(p => p.Publisher)
                .Select(item => GetRecordPub(item, item.Publisher))
                .OrderBy(x => x.ProductName);
        public IEnumerable<ProductAndPublisherBase> GetAllWithPublisherName()
           => Table.Where(x => x.IsDeleted == false)
               .Include(p => p.Publisher)
               .Select(item => GetRecordPub(item, item.Publisher))
               .OrderBy(x => x.ProductName);
        public IEnumerable<ProductAndSubCategoryBase> GetProductsForSubCategory(int id)
            => Table.Where(x => x.IsDeleted == false)
                .Where(p => p.SubCategoryId == id)
                .Include(p => p.SubCategory)
                .Select(item => GetRecordSub(item, item.SubCategory))
                .OrderBy(x => x.ProductName);


        public IEnumerable<ProductAndSubCategoryBase> GetAllWithSubCategoryName()
            => Table.Where(x => x.IsDeleted == false)
                .Include(p => p.SubCategory)
                .Select(item => GetRecordSub(item, item.SubCategory))
                .OrderBy(x => x.ProductName);

        public IEnumerable<ProductAndSubCategoryBase> GetFeaturedWithSubCategoryName()
            => Table.Where(x => x.IsDeleted == false)
                .Where(p => p.IsFeatured)
                .Include(p => p.Publisher)
                .Select(item => GetRecordSub(item, item.SubCategory))
                .OrderBy(x => x.ProductName);

        public ProductAndSubCategoryBase GetOneWithSubCategoryName(int id)
            => Table.Where(x => x.IsDeleted == false)
                .Where(p => p.Id == id)
                .Include(p => p.Publisher)
                .Select(item => GetRecordSub(item, item.SubCategory))
                .SingleOrDefault();

        public IEnumerable<ProductAndSubCategoryBase> Search(string searchString)
            => Table.Where(x => x.IsDeleted == false)
                .Where(p =>
                    p.SubCategory.SubCategoryName.ToLower().Contains(searchString.Trim().ToLower())
                    || p.Publisher.PublisherName.ToLower().Contains(searchString.Trim().ToLower())
                    || p.ProductName.ToLower().Contains(searchString.Trim().ToLower()))
                .Include(p => p.SubCategory)
                .Select(item => GetRecordSub(item, item.SubCategory))
                .OrderBy(x => x.ProductName);

        //======================================================================================================
        public string GetImageProduct(int id)
            => Table.Where(x => x.IsDeleted == false).SingleOrDefault(p => p.Id == id).ProductImage;

        public int UpdateExceptImage(Product product, bool persist = true)
        {
            Db.Products.Attach(product);
            Db.Entry(product).State = EntityState.Modified;
            Db.Entry(product).Property(x => x.ProductImage).IsModified = false;
            return persist ? SaveChanges() : 0;
        }

        public PageOutput<ProductAdminIndex> GetProductAdminIndex(int pageNumber, int pageSize)
            => new PageOutput<ProductAdminIndex>
            {
                TotalPage = (Table.Count() % pageSize == 0) ? (Table.Count() / pageSize) : (Table.Count() / pageSize + 1),
                PageNumber = pageNumber,
                Items = Table.Where(x => x.IsDeleted == false).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(p => new ProductAdminIndex
                {
                    Id = p.Id,
                    Name = p.ProductName,
                    ProductImage = p.ProductImage,
                    UnitsInStock = p.UnitsInStock,
                    IsFeature = p.IsFeatured
                }).ToList()
            };

        public CartModel GetCartView(int id)
        {
            var p = Find(id);
            if (p != null)
            {
                return new CartModel
                {
                    ProductImage = p.ProductImage,
                    UnitsInStock = p.UnitsInStock,
                    UnitCost = p.UnitCost,
                    ProductId = p.Id,
                    ProductName = p.ProductName
                };
            }
            else
                return null;
        }

        public decimal GetProductUnitCostByProductIdAsNoTracking(int productId)
            => Table.Where(x => x.IsDeleted == false).AsNoTracking().SingleOrDefault(x => x.Id == productId).UnitCost;


        //======================================================================================================
    }
}
