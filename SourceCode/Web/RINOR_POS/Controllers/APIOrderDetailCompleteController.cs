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
    /// API For Order Detail Complete
    /// </summary>
    public class APIOrderDetailCompleteController : ApiController
    {
        /// <summary>
        /// Sync Data table pos_order_detail_complete
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="OrderDetailComplete">Multi data pos_order_detail_complete </param>
        /// <returns></returns>
        public HttpResponseMessage Post(string KeyToken, pos_order_detail_complete OrderDetailComplete)
        {
            try
            {
                using (ModelPOSDB db = new ModelPOSDB())
                {
                    //pos_order_detail_complete obj = db.pos_order_detail_complete.Find(OrderDetailComplete.idOrderDetailComplete);

                    //if (obj == null)
                    //{
                    db.pos_order_detail_complete.Add(OrderDetailComplete);
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
