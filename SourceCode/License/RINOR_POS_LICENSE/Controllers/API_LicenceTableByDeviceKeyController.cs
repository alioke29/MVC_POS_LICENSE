using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using RINOR_POS.ModelLicence;
using RINOR_POS.App_Helpers;
using System.Data.Entity;

namespace RINOR_POS.Controllers
{
    /// <summary>
    /// API For Licence data by Device
    /// </summary>
    public class API_LicenceTableByDeviceKeyController : ApiController
    {
        private ModelLicencePOSDB db = new ModelLicencePOSDB();

        /// <summary>
        /// Looks up some data Bill Tamplate.
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="DeviceKey">The Device Key.</param>
        public HttpResponseMessage Get(string KeyToken = "", string DeviceKey = "")
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (Token.isValidToken(KeyToken))
            {
                var _qry = (from a in db.GCS_LICENSE_DEV.Where(a => a.DeviceKey == DeviceKey)
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
