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
    /// API For Data Merchant
    /// </summary>
    public class APIMerchantController : ApiController
    {
        private ModelPOSDB db = new ModelPOSDB();

        /// <summary>
        /// Looks up some data Merchant by ShopID.
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="ShopID">The ID of the data.</param>
        public HttpResponseMessage Get(string KeyToken, int ShopID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (Token.isValidToken(KeyToken))
            {
                var _qry = (from m in db.pos_merchant_data.Where(m => m.DeletedDate == null)
                            join a in db.pos_brand_data.Where(a => a.Activate == true && a.DeletedDate == null)
                            on m.MerchantID equals a.MerchantID
                            join b in db.pos_shop_data.Where(b => b.DeletedDate == null && b.ShopID == ShopID)
                            on a.BrandID equals b.BrandID
                            select m).ToList();
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
