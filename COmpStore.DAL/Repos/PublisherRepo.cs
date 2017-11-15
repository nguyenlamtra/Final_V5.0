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
        

    }
}
