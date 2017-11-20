using COmpStore.Models.ViewModels;
using COmpStore.Models.ViewModels.Cart;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStoreClient.Extension
{
    public static class SessionExtensions
    {
        public static void SetAuthSession(this ISession session, SessionAuth sessionAuth)
        {
            var data = GetAuthSession(session);
            if (data != null && data.Token != null)
                session.Clear();
            session.SetString("auth", JsonConvert.SerializeObject(sessionAuth));
        }
        public static SessionAuth GetAuthSession(this ISession session)
        {
            var data = session.GetString("auth");
            if (data == null)
            {
                return new SessionAuth();
            }
            return JsonConvert.DeserializeObject<SessionAuth>(data);
        }

        public static void SetSelectedProducts(this ISession session, List<SelectedProduct> selectedProducts)
        {
            session.SetString("selectedProducts", JsonConvert.SerializeObject(selectedProducts));
        }

        public static List<SelectedProduct>
           GetSelectedProducts(this ISession session)
        {
            var data = session.GetString("selectedProducts");
            if (data == null)
            {
                return new List<SelectedProduct>();
            }

            return JsonConvert.DeserializeObject<List<SelectedProduct>>(data);
        }

        public static void ClearSelectedProducts(this ISession session) => session.Remove("selectedProducts");

        public static void ClearAuth(this ISession session) => session.Remove("auth");
    }
}
