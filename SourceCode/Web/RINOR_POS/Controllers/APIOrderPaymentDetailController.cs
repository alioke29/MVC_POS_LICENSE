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
    /// Order Payment Detail
    /// </summary>
    public class APIOrderPaymentDetailController : ApiController
    {
        /// <summary>
        /// Sync Data table pos_order_payment_detail
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="OrderPaymentDetail">Multi data pos_order_payment_complete </param>
        /// <returns></returns>
        public HttpResponseMessage Post(string KeyToken, pos_order_payment_detail OrderPaymentDetail)
        {
            try
            {
                using (ModelPOSDB db = new ModelPOSDB())
                {
                    //pos_order_payment_detail obj = db.pos_order_payment_detail.Find(OrderPaymentDetail.IdOrderPaymentDetail);

                    //if (obj == null)
                    //{
                    db.pos_order_payment_detail.Add(OrderPaymentDetail);
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
