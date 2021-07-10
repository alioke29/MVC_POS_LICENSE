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
using System.Collections.Generic;

namespace RINOR_POS.Controllers
{
    /// <summary>
    /// API For Register New Merchant to Rinor License
    /// </summary>
    public class APIRegisterMerchantController : ApiController
    {
        /// <summary>
        /// Register New Merchant to Rinor License
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="MerchantData">data pos_merchant_data </param>
        /// <returns></returns>
        public HttpResponseMessage Post(string KeyToken, pos_merchant_data MerchantData)
        {
            try
            {
                if (Token.isValidToken(KeyToken))
                {
                    using (ModelLicencePOSDB db = new ModelLicencePOSDB())
                    {
                        pos_merchant_data obj = db.pos_merchant_data.Find(MerchantData.MerchantID);
                        HttpResponseMessage msg;
                        if (obj == null)
                        {
                            db.pos_merchant_data.Add(MerchantData);
                            db.SaveChanges();

                            msg = Request.CreateResponse(HttpStatusCode.OK, "OK");
                        }
                        else if (obj != null)
                        {
                            obj.MerchantCode = MerchantData.MerchantCode;
                            obj.MerchantName = MerchantData.MerchantName;
                            obj.IPaddress = MerchantData.IPaddress;
                            obj.DatabaseName = MerchantData.DatabaseName;
                            obj.UpdatedBy = MerchantData.UpdatedBy;
                            obj.UpdatedDate = MerchantData.UpdatedDate;

                            db.Entry(obj).State = EntityState.Modified;
                            db.SaveChanges();

                            msg = Request.CreateResponse(HttpStatusCode.OK, "OK");
                        }
                        else
                            msg = Request.CreateResponse(HttpStatusCode.NotModified, "NotModified");
                        return msg;
                    }
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.Forbidden);
                    response.Content = new StringContent("Invalid Token");
                    return response;
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
