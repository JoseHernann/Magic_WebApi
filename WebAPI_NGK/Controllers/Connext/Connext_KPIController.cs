using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migesa_WebAPI.Controllers.Connext
{
    public class Connext_KPIController : ApiController
    {
        // GET: api/Connext_KPI
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Connext_KPI/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Connext_KPI
        public Entities.Response.Connext.KPI Post(Entities.Request.Connext.KPI RequestObj)
        {
            Entities.Response.Connext.KPI ResponseObj = new Entities.Response.Connext.KPI();
            BLL.Proyect.Connext.KPI KPIObj = new BLL.Proyect.Connext.KPI();

            if (RequestObj.KPIProcess.processName == "GetKPIDashboardBonosKPIIndividual")
            {
                ResponseObj = KPIObj.GetKPIDashboardBonosKPIIndividual(ResponseObj, RequestObj.KPIProcess.KPIDashboardBonosKPIIndividual);
            }
            else if(RequestObj.KPIProcess.processName == "")
            {

            }
            
            return ResponseObj;
        }

        // PUT: api/Connext_KPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Connext_KPI/5
        public void Delete(int id)
        {
        }
    }
}
