using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using RINOR_POS.Models;
using RINOR_POS.App_Helpers;
using System.IO;
using System.Net.Http.Headers;
using System.Web;
namespace RINOR_POS.Controllers
{
    /// <summary>
    /// API Receipt Temp Image
    /// </summary>
    public class APIReceiptTempImageController : ApiController
    {
        private ModelPOSDB db = new ModelPOSDB();

        /// <summary>
        /// Looks up some data image Receipt.
        /// </summary>
        /// <param name="KeyToken">Token.</param>
        /// <param name="ReceiptTemplateImageID">The ID of the data.</param>
        public HttpResponseMessage Get(string KeyToken = "", int ReceiptTemplateImageID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (Token.isValidToken(KeyToken))
            {
                pos_receipt_template_image _qry = new pos_receipt_template_image();
                _qry = (from a in db.pos_receipt_template_image.Where(a => a.ReceiptTemplateImageID == ReceiptTemplateImageID) select a).FirstOrDefault();

                if (_qry != null)
                {
                    string domain = "/" + Url.Content("~").Replace(Request.RequestUri.Scheme + "://", "").Replace(Request.RequestUri.Authority, "").Replace("/api/~", "");
                    string filepath = HttpContext.Current.Server.MapPath(domain + _qry.LogoHeaderFile.Substring(1, _qry.LogoHeaderFile.Length - 1));

                    if (!File.Exists(filepath))
                    {
                        var responseFile = Request.CreateResponse(HttpStatusCode.BadRequest);
                        responseFile.Content = new StringContent("File Not Found : " + filepath);
                        return responseFile;
                    }

                    byte[] bytes = File.ReadAllBytes(filepath);
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new ByteArrayContent(bytes);
                    response.Content.Headers.ContentLength = bytes.LongLength;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                    return response;
                }
                else
                {
                    var responseNotFound = Request.CreateResponse(HttpStatusCode.BadRequest);
                    responseNotFound.Content = new StringContent("Data Not Found");
                    return responseNotFound;
                }
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid Token");
                return response;
            }
        }
    }
}
