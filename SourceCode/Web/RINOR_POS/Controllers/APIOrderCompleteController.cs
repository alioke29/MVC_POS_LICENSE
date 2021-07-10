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
    /// Sync data Order Complete
    /// </summary>
    public class APIOrderCompleteController : ApiController
    {
        /// <summary>
        /// Sync Data table pos_order_complete
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="OrderComplete">Multi data pos_order_complete </param>
        /// <returns></returns>
        public HttpResponseMessage Post(string KeyToken, pos_order_complete OrderComplete)
        {
            try
            {
                using (ModelPOSDB db = new ModelPOSDB())
                {
                    //pos_order_complete obj = db.pos_order_complete.Find(OrderComplete.idOrderComplete);

                    //if (obj == null)
                    //{
                    db.pos_order_complete.Add(OrderComplete);
                    db.SaveChanges();
                    //}
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
