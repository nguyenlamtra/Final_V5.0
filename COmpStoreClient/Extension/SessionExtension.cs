using COmpStore.Models.ViewModels;
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
        public static void SetToken(this ISession session, string token)
        {
            session.SetString("token", token);
        }

        public static string GetToken(this ISession session)
        {
            return session.GetString("token");
        }
        public static void SetAuthSession(this ISession session, SessionAuth sessionAuth)
        {
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
        #region cart
        //public static List<SelectedProduct>
        //   GetSession(this ISession session, string key)
        //{
        //    var data = session.GetString(key);
        //    if (data == null)
        //    {
        //        return new List<SelectedProduct>();
        //    }

        //    return JsonConvert.DeserializeObject<List<SelectedProduct>>(data);
        //}

        //public static void SetSession(this ISession session, string key, object value)
        //{
        //    session.SetString(key, JsonConvert.SerializeObject(value));
        //}

        //public static void SetSelectedProducts(this ISession session, List<SelectedProduct> selectedProducts)
        //{
        //    session.SetString("selectedProducts", JsonConvert.SerializeObject(selectedProducts));
        //}

        //public static List<SelectedProduct>
        //   GetSelectedProducts(this ISession session)
        //{
        //    var data = session.GetString("selectedProducts");
        //    if (data == null)
        //    {
        //        return new List<SelectedProduct>();
        //    }

        //    return JsonConvert.DeserializeObject<List<SelectedProduct>>(data);
        //}
        #endregion
    }
}
