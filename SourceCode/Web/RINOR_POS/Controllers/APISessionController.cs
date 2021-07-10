using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using RINOR_POS.Models;
using RINOR_POS.App_Helpers;
using System.Data.Entity;
using System.Collections.Generic;

namespace RINOR_POS.Controllers
{
    /// <summary>
    /// Sync data POS Session
    /// </summary>
    public class APISessionController : ApiController
    {
        /// <summary>
        /// Sync Data table pos_order_complete
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="PosSession">Multi data pos_session </param>
        /// <returns></returns>
        public HttpResponseMessage Post(string KeyToken, pos_session PosSession)
        {
            try
            {
                using (ModelPOSDB db = new ModelPOSDB())
                {
                    pos_session obj = db.pos_session.Find(PosSession.SessionID);

                    if (obj == null)
                    {
                        db.pos_session.Add(PosSession);
                        db.SaveChanges();
                    }
                    var msg = Request.CreateResponse(HttpStatusCode.OK, "Done");
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
