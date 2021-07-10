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
    /// Api For Reason Group
    /// </summary>
    public class APIReasonGroupController : ApiController
    {
        private ModelPOSDB db = new ModelPOSDB();

        /// <summary>
        /// Looks up some data Reason by ShopID.
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="ShopID">The ID of the data.</param>
        public HttpResponseMessage Get(string KeyToken, int ShopID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (Token.isValidToken(KeyToken))
            {
                var _qry = (from a in db.pos_reason_group.Where(a => a.ShopID == ShopID)
                            select a).ToList();
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
