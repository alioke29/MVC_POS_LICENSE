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
    /// Computer Name Registration
    /// </summary>
    public class APIRegisterComputerNameController : ApiController
    {
        /// <summary>
        /// Register New Merchant to Rinor License
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="ComputerNameData">data.</param>
        /// <returns></returns>
        public HttpResponseMessage Post(string KeyToken, pos_computer_name ComputerNameData)
        {
            try
            {
                if (Token.isValidToken(KeyToken))
                {
                    using (ModelLicencePOSDB db = new ModelLicencePOSDB())
                    {
                        pos_computer_name obj = db.pos_computer_name.Find(ComputerNameData.ComputerNameId);

                        if (obj == null)
                        {
                            db.pos_computer_name.Add(ComputerNameData);
                            db.SaveChanges();
                        }
                        else
                        {
                            obj.ComputerName = ComputerNameData.ComputerName;
                            obj.UpdatedBy = ComputerNameData.UpdatedBy;
                            obj.UpdatedDate = ComputerNameData.UpdatedDate;

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
