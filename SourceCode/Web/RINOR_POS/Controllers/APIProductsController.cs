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
    /// API For Data Product Items
    /// </summary>
    public class APIProductsController : ApiController
    {
        private ModelPOSDB db = new ModelPOSDB();

        /// <summary>
        /// Looks up some data Products.
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="ShopID">The ID of the data.</param>
        /// <param name="LastSycn">Last Sycn with julian date format.</param>
        public HttpResponseMessage Get(string KeyToken = "", int ShopID = 0, double LastSycn = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (Token.isValidToken(KeyToken))
            {
                if (LastSycn > 0)
                {
                    DateTime lastsycnDate = JDConverter.JulianToDate(LastSycn);

                    var _qry = (from a in db.pos_products.Where(a => a.ShopID == ShopID && (a.CreatedDate >= lastsycnDate || a.UpdatedDate >= lastsycnDate || a.DeletedDate >= lastsycnDate))
                                select a).ToList();

                    var json = JsonConvert.SerializeObject(_qry);

                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    return response;
                }
                else
                {
                    var _qry = (from a in db.pos_products.Where(a => a.ShopID == ShopID) select a).ToList();
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
