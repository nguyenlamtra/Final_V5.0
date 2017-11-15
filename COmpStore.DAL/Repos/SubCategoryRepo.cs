using COmpStore.DAL.EF;
using COmpStore.DAL.Repos.Base;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels.Base;
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

    }
}
