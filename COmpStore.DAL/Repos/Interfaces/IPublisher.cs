using COmpStore.DAL.Repos.Base;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels.PublisherAdmin;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.DAL.Repos.Interfaces
{
    public interface IPublisherRepo : IRepo<Publisher>
    {
        IEnumerable<Publisher> GetAllWithProductsByPublisher();
        Publisher GetOneWithProductByPublisher(int? id);
        //=================
        IEnumerable<PublisherAdminIndex> GetForAdminPublisherIndex();
        PublisherAdminDetails GetForAdminPublisherDetails(int id);
        IEnumerable<PublisherCombobox> GetPublisherCombobox();
        int DeletePublisher(int id, bool persist = true);
        //=====================
    }
}
