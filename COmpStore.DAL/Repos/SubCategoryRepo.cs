﻿using COmpStore.DAL.EF;
using COmpStore.DAL.Repos.Base;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels.Base;
using COmpStore.Models.ViewModels.ProductAdmin;
using COmpStore.Models.ViewModels.SubCategoryAdmin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COmpStore.DAL.Repos
{
    public class SubCategoryRepo : RepoBase<SubCategory>, ISubCategoryRepo
    {
        public SubCategoryRepo(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public SubCategoryRepo()
        {
        }


        public override IEnumerable<SubCategory> GetAll()
           => Table.OrderBy(x => x.SubCategoryName);

        public override IEnumerable<SubCategory> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.SubCategoryName), skip, take);


        internal SubCategoryAndCategoryBase GetRecord(SubCategory s, Category c)
           => new SubCategoryAndCategoryBase()
           {
               CategoryName = c.CategoryName,
               CategoryId = s.CategoryId,
               Id = s.Id,
               //SubCategoryId = s.Id,
               SubCategoryName = s.SubCategoryName
           };

        public IEnumerable<SubCategoryAndCategoryBase> GetSubCategoriesForCategory(int id)
            => Table
                .Where(s => s.CategoryId == id)
                .Include(s => s.Category)
                .Select(item => GetRecord(item, item.Category))
                .OrderBy(x => x.SubCategoryName);


        public IEnumerable<SubCategoryAndCategoryBase> GetAllWithCategoryName()
            => Table
                .Include(p => p.Category)
                .Select(item => GetRecord(item, item.Category))
                .OrderBy(x => x.SubCategoryName);


        public SubCategoryAndCategoryBase GetOneWithCategoryName(int id)
            => Table
                .Where(p => p.Id == id)
                .Include(p => p.Category)
                .Select(item => GetRecord(item, item.Category))
                .SingleOrDefault();

        public IEnumerable<SubCategoryAndCategoryBase> Search(string searchString)
            => Table
                .Where(p =>
                   p.SubCategoryName.ToLower().Contains(searchString.ToLower()))
                .Include(p => p.Category)
                .Select(item => GetRecord(item, item.Category))
                .OrderBy(x => x.SubCategoryName);

        //======================================================================================

        public IEnumerable<SubCategoryAdminIndex> GetSubCategoryAdminIndex()
        => Table.Where(x => x.IsDeleted == false).Select(s => new SubCategoryAdminIndex
        {
            Id = s.Id,
            Name = s.SubCategoryName,
            SumProducts = s.Products.Count
        });

        internal IEnumerable<ProductRelate> GetProRecord(IEnumerable<Product> pro)
           => pro.Where(x => x.IsDeleted == false).Select(p => new ProductRelate()
           {
               ProductName = p.ProductName,
               Id = p.Id,
               UnitsInStock = p.UnitsInStock
           });

        public SubCategoryAdminDetails GetSubCategoryAdminDetails(int id)
        => Table.Where(x => x.IsDeleted == false).Select(s => new SubCategoryAdminDetails
        {
            Id = s.Id,
            CategoryName = s.Category.CategoryName,
            Name = s.SubCategoryName,
            Products = GetProRecord(s.Products)
        }).SingleOrDefault(x => x.Id == id);

        public IEnumerable<SubCategoryCombobox> GetSubCategoryCombobox()
            => Table.Where(x => x.IsDeleted == false).Select(s => new SubCategoryCombobox
            {
                Id = s.Id,
                Name = s.SubCategoryName
            });

        public int DeleteSubCategory(int id, bool persist = true)
        {
            var subCategory = Db.SubCategories.Include(x => x.Products).SingleOrDefault(x => x.Id == id);
            if (subCategory != null)
            {
                subCategory.IsDeleted = true;
                subCategory.Products.ForEach(x => x.IsDeleted = true);
                Db.SaveChanges();
                return 1;
            }
            else
                return 0;
        }
        //=======================================================================================
    }
}
































//public override IEnumerable<SubCategory> GetAll()
//         => Table.Where(x => x.IsDeleted == false).OrderBy(x => x.SubCategoryName);

//public override IEnumerable<SubCategory> GetRange(int skip, int take)
//    => GetRange(Table.OrderBy(x => x.SubCategoryName), skip, take);


//internal SubCategoryAndCategoryBase GetRecord(SubCategory s, Category c)
//   => new SubCategoryAndCategoryBase()
//   {
//       CategoryName = c.CategoryName,
//       CategoryId = s.CategoryId,
//       Id = s.Id,
//               //SubCategoryId = s.Id,
//               SubCategoryName = s.SubCategoryName
//   };

//public IEnumerable<SubCategoryAndCategoryBase> GetSubCategoriesForCategory(int id)
//    => Table.Where(x => x.IsDeleted == false)
//        .Where(s => s.CategoryId == id)
//        .Include(s => s.Category)
//        .Select(item => GetRecord(item, item.Category))
//        .OrderBy(x => x.SubCategoryName);


//public IEnumerable<SubCategoryAndCategoryBase> GetAllWithCategoryName()
//    => Table.Where(x => x.IsDeleted == false)
//        .Include(p => p.Category)
//        .Select(item => GetRecord(item, item.Category))
//        .OrderBy(x => x.SubCategoryName);


//public SubCategoryAndCategoryBase GetOneWithCategoryName(int id)
//    => Table.Where(x => x.IsDeleted == false)
//        .Where(p => p.Id == id)
//        .Include(p => p.Category)
//        .Select(item => GetRecord(item, item.Category))
//        .SingleOrDefault();

//public IEnumerable<SubCategoryAndCategoryBase> Search(string searchString)
//    => Table.Where(x => x.IsDeleted == false)
//        .Where(p =>
//           p.SubCategoryName.ToLower().Contains(searchString.ToLower()))
//        .Include(p => p.Category)
//        .Select(item => GetRecord(item, item.Category))
//        .OrderBy(x => x.SubCategoryName);