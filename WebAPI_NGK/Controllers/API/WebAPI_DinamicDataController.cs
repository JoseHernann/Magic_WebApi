using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using WebAPI_NGK;

namespace WebAPI_NGK.Controllers.WebAPI_NGK
{
    public class WebAPI_DinamicDataController : ApiController
    {
        // POST: api/WebAPI_DinamicData
        [Route("api/DinamicData/GetDinamicData")]
        [HttpPost]
        [Obsolete]
        public JsonResult<string> Post(Entities.Request.WebAPI_NGK.DinamicData RequestObj)
        {
            BLL.Proyect.WebAPI_NGK.DinamicData DinamicDataObj = new BLL.Proyect.WebAPI_NGK.DinamicData();

            String resultJSON = string.Empty;
            String process = RequestObj.process;


            if (process == "WebAPI_GetDinamicData")
            {
                DataTable dt = DinamicDataObj.WebAPI_GetDinamicData(RequestObj);
                resultJSON = JsonConvert.SerializeObject(
                    DinamicDataObj.GetDataTableDictionaryList(dt),
                    Newtonsoft.Json.Formatting.Indented);
            }
            if (process == "WebAPI_GetDinamicData_QRY")
            {
                DataTable dt = DinamicDataObj.WebAPI_GetDinamicDataQRY(RequestObj);
                resultJSON = JsonConvert.SerializeObject(
                    DinamicDataObj.GetDataTableDictionaryList(dt),
                    Newtonsoft.Json.Formatting.Indented);
            }
            if (process == "WebAPI_GetDinamicData_O")
            {
                DataTable dt = DinamicDataObj.WebAPI_GetDinamicData_O(RequestObj);
                resultJSON = JsonConvert.SerializeObject(
                    DinamicDataObj.GetDataTableDictionaryList(dt),
                    Newtonsoft.Json.Formatting.Indented);
            }
            if (process == "WebAPI_GetDinamicData_QRY_O")
            {
                DataTable dt = DinamicDataObj.WebAPI_GetDinamicDataQRY_O(RequestObj);
                resultJSON = JsonConvert.SerializeObject(
                    DinamicDataObj.GetDataTableDictionaryList(dt),
                    Newtonsoft.Json.Formatting.Indented);
            }
            if (process == "WebAPI_GetDinamicData_M")
            {
                DataTable dt = DinamicDataObj.WebAPI_GetDinamicData_M(RequestObj);
                resultJSON = JsonConvert.SerializeObject(
                    DinamicDataObj.GetDataTableDictionaryList(dt),
                    Newtonsoft.Json.Formatting.Indented);
            }

            if (process == "Encrypt")
            {
                String Encrypt = Utilities.Security.EncryptData(RequestObj.dataString);

                Object JSON = new { Encryptado = RequestObj.dataString, Descryptado = Encrypt, };

                resultJSON = JsonConvert.SerializeObject(JSON, Newtonsoft.Json.Formatting.Indented);
            }

            if (process == "Decrypt")
            {
                String Encrypt = Utilities.Security.DecryptData(RequestObj.dataString);
                Object JSON = new { Encryptado = RequestObj.dataString, Descryptado = Encrypt, };
                resultJSON = JsonConvert.SerializeObject(JSON, Newtonsoft.Json.Formatting.Indented);
            }


            if (process == "Login")
            {
                DataTable dt = DinamicDataObj.WebAPI_GetDinamicData_Login(RequestObj);
                resultJSON = JsonConvert.SerializeObject(DinamicDataObj.GetDataTableDictionaryList(dt), Newtonsoft.Json.Formatting.Indented);
            }

            if (process == "LoginVW")
            {
                DataTable dt = DinamicDataObj.WebAPI_GetDinamicData_Login_V(RequestObj);
                resultJSON = JsonConvert.SerializeObject(DinamicDataObj.GetDataTableDictionaryList(dt), Newtonsoft.Json.Formatting.Indented);
            }



            return Json(resultJSON);
        }
    }
}
