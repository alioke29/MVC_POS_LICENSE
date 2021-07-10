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
    /// Licence Type By ID
    /// </summary>
    public class APILicenceTypeByIDController : ApiController
    {
        private ModelLicencePOSDB db = new ModelLicencePOSDB();

        /// <summary>
        /// Looks up some data Licence.
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="ID">ID.</param>
        public HttpResponseMessage Get(string KeyToken = "", int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (Token.isValidToken(KeyToken))
            {
                var _qry = (from a in db.POS_Licence_Type.Where(a => a.LicenceTypeID >= ID)
                            select a).FirstOrDefault();

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
