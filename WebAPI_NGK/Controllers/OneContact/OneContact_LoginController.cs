using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migesa_WebAPI.Controllers.OneContact
{
    public class OneContact_LoginController : ApiController
    {
        // GET: api/OneContact_Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/OneContact_Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/OneContact_Login
        [HttpPost]
        public Entities.Response.OneContact.Login Post(Entities.Request.OneContact.Login RequestObj)
        {
            Entities.Response.OneContact.Login ResponseObj = new Entities.Response.OneContact.Login();
            BLL.Proyect.OneContact.Login LoginObj = new BLL.Proyect.OneContact.Login();

            ResponseObj = LoginObj.GetLogIn(ResponseObj, RequestObj);

            return ResponseObj;
        }

        // PUT: api/OneContact_Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/OneContact_Login/5
        public void Delete(int id)
        {
        }
    }
}
