using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migesa_WebAPI.Controllers.Connext
{
    public class Connext_LoginController : ApiController
    {
        //// GET: api/Connext_Login
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Connext_Login/5
        public Entities.Response.Connext.Login Get(Entities.Request.Connext.Login RequestObj)
        {
            Entities.Response.Connext.Login ResponseObj = new Entities.Response.Connext.Login();
            BLL.Proyect.Connext.Login LoginObj = new BLL.Proyect.Connext.Login();

            ResponseObj = LoginObj.GetLogIn(ResponseObj, RequestObj);

            return ResponseObj;
        }

        // POST: api/Connext_Login
        [HttpPost]
        public Entities.Response.Connext.Login Post(Entities.Request.Connext.Login RequestObj)
        {
            Entities.Response.Connext.Login ResponseObj = new Entities.Response.Connext.Login();
            BLL.Proyect.Connext.Login LoginObj = new BLL.Proyect.Connext.Login();

            ResponseObj = LoginObj.GetLogIn(ResponseObj, RequestObj);

            return ResponseObj;
        }

        // PUT: api/Connext_Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Connext_Login/5
        public void Delete(int id)
        {
        }

    }
}
