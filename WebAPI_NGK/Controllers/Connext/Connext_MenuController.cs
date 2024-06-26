using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migesa_WebAPI.Controllers.Connext
{
    public class Connext_MenuController : ApiController
    {
        //// GET: api/Connext_Menu
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Connext_Menu/5
        public Entities.Response.Connext.Menu Get(Entities.Request.Connext.Menu RequestObj)
        {
            Entities.Response.Connext.Menu ResponseObj = new Entities.Response.Connext.Menu();
            BLL.Proyect.Connext.Menu MenuObj = new BLL.Proyect.Connext.Menu();

            ResponseObj = MenuObj.GetMenu(ResponseObj, RequestObj);

            return ResponseObj;
        }

        // POST: api/Connext_Menu
        public Entities.Response.Connext.Menu Post(Entities.Request.Connext.Menu RequestObj)
        {
            Entities.Response.Connext.Menu ResponseObj = new Entities.Response.Connext.Menu();
            BLL.Proyect.Connext.Menu MenuObj = new BLL.Proyect.Connext.Menu();

            ResponseObj = MenuObj.GetMenu(ResponseObj, RequestObj);

            return ResponseObj;
        }

        // PUT: api/Connext_Menu/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Connext_Menu/5
        public void Delete(int id)
        {
        }
    }
}
