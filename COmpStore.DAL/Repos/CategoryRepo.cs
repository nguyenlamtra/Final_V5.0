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

    }
}
