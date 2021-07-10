using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using RINOR_POS.ModelLicence;
using RINOR_POS.App_Helpers;

namespace RINOR_POS.Controllers
{
    /// <summary>
    /// Brand By ID
    /// </summary>
    public class APIBrandByIDController : ApiController
    {
        /// <summary>
        /// Looks up some data Brand
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="BrandID">ID of Data</param>
        public HttpResponseMessage Get(string KeyToken = "", int BrandID = 0)
        {
            using (ModelLicencePOSDB db = new ModelLicencePOSDB())
            {
                db.Configuration.ProxyCreationEnabled = false;

                if (Token.isValidToken(KeyToken))
                {
                    var _qry = db.pos_brand_data.Find(BrandID);

                    var json = JsonConvert.SerializeObject(_qry);

                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    return response;
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.Forbidden);
                    response.Content = new StringContent("Invalid Token");
                    return response;
                }
            }
        }
    }
}
