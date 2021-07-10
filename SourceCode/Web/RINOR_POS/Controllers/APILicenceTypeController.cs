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
    /// API For Data LicenceType
    /// </summary>
    public class APILicenceTypeController : ApiController
    {
        private ModelLicencePOSDB db = new ModelLicencePOSDB();

        /// <summary>
        /// Looks up some data Licence.
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="LastSycn">Last Sycn with julian date format.</param>
        public HttpResponseMessage Get(string KeyToken = "", double LastSycn = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (Token.isValidToken(KeyToken))
            {
                if (LastSycn > 0)
                {
                    DateTime lastsycnDate = JDConverter.JulianToDate(LastSycn);

                    var _qry = (from a in db.POS_Licence_Type.Where(a => a.InsertedDate >= lastsycnDate || a.UpdatedDate >= lastsycnDate || a.DeletedDate >= lastsycnDate)
                                select a).ToList();

                    var json = JsonConvert.SerializeObject(_qry);

                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    return response;
                }
                else
                {
                    var _qry = (from a in db.POS_Licence_Type
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
