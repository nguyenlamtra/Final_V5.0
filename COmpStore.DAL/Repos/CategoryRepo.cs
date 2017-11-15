using COmpStore.DAL.EF;
using COmpStore.DAL.Repos.Base;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels;
using COmpStore.Models.ViewModels.Base;
using COmpStore.Models.ViewModels.CategoryAdmin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COmpStore.DAL.Repos
{
    public class CategoryRepo : RepoBase<Category>, ICategoryRepo
    {
        public CategoryRepo(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public CategoryRepo()
        {
        }
        public override IEnumerable<Category> GetAll()
            => Table.OrderBy(x => x.CategoryName);

        public override IEnumerable<Category> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.CategoryName), skip, take);

       

        public Category GetOneWithCategory(int? id)
            => Table.Include(x => x.SubCategories).FirstOrDefault(x => x.Id == id);

        public IEnumerable<Category> GetAllWithCategories()
            => Table.Include(x => x.SubCategories);
        //==============================
        internal IEnumerable<SubCategoryAdminViewIndex> GetRecordSub(IEnumerable<SubCategory> sub)
           => sub.Select(s => new SubCategoryAdminViewIndex()
           {
               Id = s.Id,
               SubCategoryName = s.SubCategoryName,
               SumProducts = s.Products.Count
           }).ToList();


        public CategoryAdminDetails GetAdminCategoryDetails(int id)
            => Table.Include(c => c.SubCategories).ThenInclude(s => s.Products).Select(c => new CategoryAdminDetails
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                SubCategories = GetRecordSub(c.SubCategories)
            }).SingleOrDefault(c => c.Id == id);

        public IEnumerable<CategoryAdminIndex> GetAdminCategoryIndex()
            => Table.Select(c => new CategoryAdminIndex
            {
                CategoryId = c.Id,
                CategoryName = c.CategoryName,
                SumSubCategories = c.SubCategories.Count
            });
        //===============================
    }
}
