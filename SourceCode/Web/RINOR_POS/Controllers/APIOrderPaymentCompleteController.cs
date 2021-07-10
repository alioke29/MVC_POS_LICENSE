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
    /// API For Order Payment Complete
    /// </summary>
    public class APIOrderPaymentCompleteController : ApiController
    {
        /// <summary>
        /// Sync Data table pos_order_payment_complete
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="OrderPaymentComplete">Multi data pos_order_payment_complete </param>
        /// <returns></returns>
        public HttpResponseMessage Post(string KeyToken, pos_order_payment_complete OrderPaymentComplete)
        {
            try
            {
                using (ModelPOSDB db = new ModelPOSDB())
                {
                    //pos_order_payment_complete obj = db.pos_order_payment_complete.Find(OrderPaymentComplete.IdOrderPaymentComplete);

                    //if (obj == null)
                    //{
                    db.pos_order_payment_complete.Add(OrderPaymentComplete);
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
