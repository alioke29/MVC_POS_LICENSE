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
    /// Register Shop
    /// </summary>
    public class APIRegisterShopController : ApiController
    {
        /// <summary>
        /// Register New Merchant to Rinor License
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="ShopData">data.</param>
        /// <returns></returns>
        public HttpResponseMessage Post(string KeyToken, pos_shop_data ShopData)
        {
            try
            {
                if (Token.isValidToken(KeyToken))
                {
                    using (ModelLicencePOSDB db = new ModelLicencePOSDB())
                    {
                        pos_shop_data obj = db.pos_shop_data.Find(ShopData.ShopID);

                        if (obj == null)
                        {
                            db.pos_shop_data.Add(ShopData);
                            db.SaveChanges();
                        }
                        else
                        {
                            obj.ShopName = ShopData.ShopName;
                            obj.ShopCode = ShopData.ShopCode;
                            obj.CompanyName = ShopData.CompanyName;
                            obj.UpdatedBy = ShopData.UpdatedBy;
                            obj.UpdatedDate = ShopData.UpdatedDate;

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
