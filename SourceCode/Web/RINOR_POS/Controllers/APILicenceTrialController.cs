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
    /// Update to Trial Mode (table GCS_LICENSE_DEV)
    /// </summary>
    public class APILicenceTrialController : ApiController
    {
        /// <summary>
        /// Update to Trial Mode table GCS_LICENSE_DEV
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="licence">Single data GCS_LICENSE_DEV </param>
        /// <returns></returns>
        public HttpResponseMessage Post(string KeyToken, GCS_LICENSE_DEV licence)
        {
            try
            {
                using (ModelLicencePOSDB db = new ModelLicencePOSDB())
                {
                    GCS_LICENSE_DEV oLicence = db.GCS_LICENSE_DEV.Find(licence.LicenceDataID);
                   
                    oLicence.TenggoStart = licence.TenggoStart;
                    oLicence.TenggoFinish = licence.TenggoFinish;
                    db.Entry(oLicence).State = EntityState.Modified;
                    db.SaveChanges();

                    var msg = Request.CreateResponse(HttpStatusCode.Created, oLicence);
                    return msg;
                }
            }
            catch (Exception ex)
            {

                //return response as bad request  with exception message.  
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
