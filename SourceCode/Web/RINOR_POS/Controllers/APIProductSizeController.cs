using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using RINOR_POS.Models;
using RINOR_POS.App_Helpers;

namespace RINOR_POS.Controllers
{
    /// <summary>
    /// API Product Size
    /// </summary>
    public class APIProductSizeController : ApiController
    {
        private ModelPOSDB db = new ModelPOSDB();

        /// <summary>
        /// Looks up some data Product Size.
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="MasterShopID">The ID of the data.</param>
        /// <param name="LastSycn">Last Sycn with julian date format.</param>
        public HttpResponseMessage Get(string KeyToken = "", int MasterShopID = 0, double LastSycn = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (Token.isValidToken(KeyToken))
            {
                if (LastSycn > 0)
                {
                    DateTime lastsycnDate = JDConverter.JulianToDate(LastSycn);

                    var _qry = (from a in db.pos_product_size.Where(a => (a.CreatedDate >= lastsycnDate || a.UpdatedDate >= lastsycnDate || a.DeletedDate >= lastsycnDate))
                                join b in db.pos_products.Where(b => b.ShopID == MasterShopID) on a.ProductID equals b.ProductID
                                select a).ToList();

                    var json = JsonConvert.SerializeObject(_qry);

                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    return response;
                }
                else
                {
                    var _qry = (from a in db.pos_product_size
                                join b in db.pos_products.Where(b => b.ShopID == MasterShopID) on a.ProductID equals b.ProductID
                                select a).ToList();
                    var json = JsonConvert.SerializeObject(_qry);

                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    return response;
                }
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid Token");
                return response;
            }
        }
    }
}
