using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migesa_WebAPI.Controllers.Connext
{
    public class Connext_WidgetController : ApiController
    {
        //// GET: api/Connext_Widget
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Connext_Widget/5
        public Entities.Response.Connext.Widgets Get(Entities.Request.Connext.Widgets RequestObj)
        {
            Entities.Response.Connext.Widgets ResponseObj = new Entities.Response.Connext.Widgets();
            BLL.Proyect.Connext.Widgets WidgetsObj = new BLL.Proyect.Connext.Widgets();

            ResponseObj = WidgetsObj.GetUserWidgets(ResponseObj, RequestObj);

            return ResponseObj;
        }

        // POST: api/Connext_Widget
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Connext_Widget/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Connext_Widget/5
        public void Delete(int id)
        {
        }
    }
}
