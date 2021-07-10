
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
using RINOR_POS.Models;

namespace RINOR_POS.Controllers
{
    /// <summary>
    /// API For Licence with Merchant, Brand, Shop data by Device
    /// </summary>
    public class APIRegisterComputerNameController : ApiController
    {
        private ModelLicencePOSDB db = new ModelLicencePOSDB();

        /// <summary>
        /// Looks up some data Bill Tamplate.
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="DeviceKey">The Device Key.</param>
        public HttpResponseMessage Get(string KeyToken = "", string DeviceKey = "")
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (Token.isValidToken(KeyToken))
            {
                var _qry = (from a in db.pos_computer_name
                            where a.DeletedDate == null && a.DeviceCode == DeviceKey
                            select a).ToList();

                var json = JsonConvert.SerializeObject(_qry);

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return response;
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid Token");
                return response;
            }
        }

        public HttpResponseMessage Post(string KeyToken, ModelLicence.GCS_LICENSE_DEV data)
        {


            GCS_LICENSE_DEV license = new GCS_LICENSE_DEV();

            license.MerchantID = data.MerchantID;
            license.MerchantKey = data.MerchantKey;
            license.BrandID = data.BrandID;
            license.BrandKey = data.BrandKey;
            license.ShopID = data.ShopID;
            license.ShopKey = data.ShopKey;
            license.LicenceType = Convert.ToInt32(data.LicenceType);
            license.LicenceKey = data.LicenceKey;
            license.DeviceKey = data.DeviceKey;
            license.LicenceStart = data.LicenceStart;
            license.LicenceFinish = data.LicenceFinish;
            license.BuyerEmail = data.BuyerEmail;
            license.BuyerMobileNo = data.BuyerMobileNo;
            license.isActive = data.isActive;
            license.CreatedDate = data.CreatedDate;
            license.CreatedBy = data.CreatedBy;

            license = db.GCS_LICENSE_DEV.Add(license);
            db.SaveChanges();         

            var msg = Request.CreateResponse(HttpStatusCode.Created, data);
            return msg;

        }


    }

}
