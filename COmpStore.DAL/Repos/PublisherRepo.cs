using COmpStore.DAL.EF;
using COmpStore.DAL.Repos.Base;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels.Base;
using COmpStore.Models.ViewModels.ProductAdmin;
using COmpStore.Models.ViewModels.PublisherAdmin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COmpStore.DAL.Repos
{
    public class PublisherRepo : RepoBase<Publisher>, IPublisherRepo
    {
        public PublisherRepo(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public PublisherRepo()
        {
        }
        public override IEnumerable<Publisher> GetAll()
            => Table.OrderBy(x => x.PublisherName);

        public override IEnumerable<Publisher> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.PublisherName), skip, take);

        public Publisher GetOneWithProductByPublisher(int? id)
            => Table.Include(x => x.Products).FirstOrDefault(x => x.Id == id);

        public IEnumerable<Publisher> GetAllWithProductsByPublisher()
            => Table.Include(x => x.Products);

        //==============================

        internal IEnumerable<ProductRelate> GetRecordPro(IEnumerable<Product> p)
           => p.Select(pro => new ProductRelate()
           {
               Id = pro.Id,
               ProductName = pro.ProductName,
               UnitsInStock = pro.UnitsInStock
           }).ToList();

        public PublisherAdminDetails GetForAdminPublisherDetails(int id)
            => Table.Include(p => p.Products).Select(c => new PublisherAdminDetails
            {
                Id = c.Id,
                Name = c.PublisherName,
                Products = GetRecordPro(c.Products)
            }).SingleOrDefault(p => p.Id == id);

        public IEnumerable<PublisherAdminIndex> GetForAdminPublisherIndex()
            => Table.Select(c => new PublisherAdminIndex
            {
                Id = c.Id,
                Name = c.PublisherName,
                SumProducts = c.Products.Count
            });

        public IEnumerable<PublisherCombobox> GetPublisherCombobox() => Table.Select(p => new PublisherCombobox
        {
            Id = p.Id,
            Name = p.PublisherName
        });
        //===============================
    }
}
