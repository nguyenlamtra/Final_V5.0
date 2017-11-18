using COmpStore.DAL.EF;
using COmpStore.DAL.Repos.Base;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels.CategoryAdmin;
using COmpStore.Models.ViewModels.SubCategoryAdmin;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            => Table.Where(x => x.IsDeleted == false).OrderBy(x => x.CategoryName);

        public override IEnumerable<Category> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.CategoryName), skip, take);

       

        public Category GetOneWithCategory(int? id)
            => Table.Where(x => x.IsDeleted == false).Include(x => x.SubCategories).FirstOrDefault(x => x.Id == id);

        public IEnumerable<Category> GetAllWithCategories()
            => Table.Where(x => x.IsDeleted == false).Include(x => x.SubCategories);
        //==============================
        internal IEnumerable<SubCategoryAdminIndex> GetRecordSub(IEnumerable<SubCategory> sub)
           => sub.Where(x => x.IsDeleted == false).Select(s => new SubCategoryAdminIndex()
           {
               Id = s.Id,
               Name = s.SubCategoryName,
               SumProducts = s.Products.Count
           }).ToList();


        public CategoryAdminDetails GetAdminCategoryDetails(int id)
            => Table.Where(x => x.IsDeleted == false)
            .Include(c => c.SubCategories).ThenInclude(s => s.Products)
            .Select(c => new CategoryAdminDetails
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                SubCategories = GetRecordSub(c.SubCategories)
            }).SingleOrDefault(c => c.Id == id);

        public IEnumerable<CategoryAdminIndex> GetAdminCategoryIndex()
            => Table.Where(x => x.IsDeleted == false).Select(c => new CategoryAdminIndex
            {
                CategoryId = c.Id,
                CategoryName = c.CategoryName,
                SumSubCategories = c.SubCategories.Count
            });

        public int DeleteCategory(int id, bool persist = true)
        {
            var category = Db.Categories.Include(x => x.SubCategories).ThenInclude(x => x.Products).SingleOrDefault(x => x.Id == id);
            if (category != null)
            {
                category.IsDeleted = true;
                category.SubCategories.ForEach(x => { x.Products.ForEach(y => y.IsDeleted = true); x.IsDeleted = true; });
                Db.SaveChanges();
                return 1;
            }
            else
                return 0;
        }
        //===============================
    }
}
