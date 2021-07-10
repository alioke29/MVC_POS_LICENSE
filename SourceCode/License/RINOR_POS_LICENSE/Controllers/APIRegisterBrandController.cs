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
    public class APIRegisterBrandController : ApiController
    {
        /// <summary>
        /// Register New Merchant to Rinor License
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="BrandData">data.</param>
        /// <returns></returns>
        public HttpResponseMessage Post(string KeyToken, pos_brand_data BrandData)
        {
            try
            {
                if (Token.isValidToken(KeyToken))
                {
                    using (ModelLicencePOSDB db = new ModelLicencePOSDB())
                    {
                        pos_brand_data obj = db.pos_brand_data.Find(BrandData.BrandID);

                        if (obj == null)
                        {
                            db.pos_brand_data.Add(BrandData);
                            db.SaveChanges();
                        }
                        else
                        {
                            obj.BrandCode = BrandData.BrandCode;
                            obj.BrandName = BrandData.BrandName;
                            obj.IPAddress = BrandData.IPAddress;
                            obj.UpdatedDate = BrandData.UpdatedDate;
                            obj.UpdatedBy = BrandData.UpdatedBy;

                            db.Entry(obj).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        var msg = Request.CreateResponse(HttpStatusCode.OK, "Done");
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
