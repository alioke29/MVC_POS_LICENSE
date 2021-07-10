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
    /// Computer Type Registration
    /// </summary>
    public class APIRegisterComputerTypeController : ApiController
    {
        /// <summary>
        /// Register New Merchant to Rinor License
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="ComputerTypeData">data.</param>
        /// <returns></returns>
        public HttpResponseMessage Post(string KeyToken, pos_computer_type ComputerTypeData)
        {
            try
            {
                if (Token.isValidToken(KeyToken))
                {
                    using (ModelLicencePOSDB db = new ModelLicencePOSDB())
                    {
                        pos_computer_type obj = db.pos_computer_type.Find(ComputerTypeData.ComputerTypeID);

                        if (obj == null)
                        {
                            db.pos_computer_type.Add(ComputerTypeData);
                            db.SaveChanges();
                        }
                        else
                        {
                            obj.ComputerTypeName = ComputerTypeData.ComputerTypeName;
                            obj.UpdatedBy = ComputerTypeData.UpdatedBy;
                            obj.UpdatedDate = ComputerTypeData.UpdatedDate;

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
