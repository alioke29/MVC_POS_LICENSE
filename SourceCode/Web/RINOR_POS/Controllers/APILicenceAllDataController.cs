
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

namespace RINOR_POS.Controllers
{
    /// <summary>
    /// API For Licence with Merchant, Brand, Shop data by Device
    /// </summary>
    public class APILicenceAllDataController : ApiController
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
                var _qry = (from a in db.pos_merchant_data
                            where a.DeletedDate == null

                            join b in db.pos_brand_data
                            on a.MerchantID equals b.MerchantID


                            join c in db.pos_shop_data
                            on b.BrandID equals c.BrandID


                            join d in db.GCS_LICENSE_DEV
                            on c.ShopID equals d.ShopID
                            where d.DeviceKey == DeviceKey && d.DeletedDate == null

                            select new
                            {
                                a.MerchantID,
                                a.MerchantKey,
                                a.MerchantName,
                                b.BrandID,
                                b.BrandKey,
                                b.BrandName,
                                c.ShopID,
                                c.ShopKey,
                                c.ShopName,
                                d.BuyerEmail,
                                d.BuyerMobileNo
                            }).ToList();

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

        /// <summary>
        /// Update to Active table GCS_LICENSE_DEV
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="licence">Single data GCS_LICENSE_DEV </param>
        /// <returns></returns>
        public HttpResponseMessage Post(string KeyToken, GCS_LICENSE_DEV licence)
        {
            try
            {
                using (ModelLicencePOSDB db = new ModelLicencePOSDB())
                {

                    GCS_LICENSE_DEV oLicence = db.GCS_LICENSE_DEV.Find(licence.LicenceDataID);
                    oLicence.TenggoStart = null;
                    oLicence.TenggoFinish = null;
                    oLicence.LicenceStart = licence.LicenceStart;
                    oLicence.LicenceFinish = licence.LicenceFinish;
                    oLicence.Period = licence.Period;
                    oLicence.isActive = licence.isActive;
                    db.Entry(oLicence).State = EntityState.Modified;
                    db.SaveChanges();

                    var msg = Request.CreateResponse(HttpStatusCode.Created, oLicence);
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
