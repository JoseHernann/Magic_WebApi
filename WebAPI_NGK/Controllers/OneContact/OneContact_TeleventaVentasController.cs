using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migesa_WebAPI.Controllers.OneContact
{
    public class OneContact_TeleventaVentasController : ApiController
    {
        // GET: api/OneContact_TeleventaVentas
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/OneContact_TeleventaVentas/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/OneContact_TeleventaVentas
        [HttpPost]
        public Entities.Response.OneContact.TeleventaVentas Post(Entities.Request.OneContact.TeleventaVentas RequestObj)
        {
            Entities.Response.OneContact.TeleventaVentas ResponseObj = new Entities.Response.OneContact.TeleventaVentas();
            BLL.Proyect.OneContact.TeleventaVentas LoginObj = new BLL.Proyect.OneContact.TeleventaVentas();

            if (RequestObj.OneContact_Process.process == "Televenta_Ventas_Insert")
            {
                ResponseObj = LoginObj.Televenta_Ventas_Insert(ResponseObj, RequestObj);
            }
            else if (RequestObj.OneContact_Process.process == "Televenta_ValidacionVenta_Update")
            {
                ResponseObj = LoginObj.Televenta_ValidacionVenta_Update(ResponseObj, RequestObj);
            }
            else if(RequestObj.OneContact_Process.process == "Televenta_ReabrirVenta_Update")
            {
                ResponseObj = LoginObj.Televenta_ReabrirVenta_Update(ResponseObj, RequestObj);
            }

            return ResponseObj;
        }

        // PUT: api/OneContact_TeleventaVentas/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/OneContact_TeleventaVentas/5
        public void Delete(int id)
        {
        }
    }
}
