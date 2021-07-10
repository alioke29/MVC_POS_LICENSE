using Newtonsoft.Json;
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
    /// API For Data Shop Property
    /// </summary>
    public class APIShopPropertyController : ApiController
    {
        private ModelPOSDB db = new ModelPOSDB();

        /// <summary>
        /// Looks up some data Shop Property by ShopID.
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="ShopID">The ID of the data.</param>
        public HttpResponseMessage Get(string KeyToken, int ShopID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (Token.isValidToken(KeyToken))
            {
                var _qry = (from sp in db.pos_shop_property.Where(sp => sp.ShopID == ShopID) select sp).ToList();
                var json = JsonConvert.SerializeObject(_qry);

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return response;
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
