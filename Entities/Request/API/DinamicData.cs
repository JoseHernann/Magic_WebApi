using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Request.WebAPI_NGK
{
    public class DinamicData
    {
        public string process { get; set; }
        public string dataString { get; set; }
        public string encryptedSP { get; set; }
        public string encryptedConnection { get; set; }
        public List<paramValues> paramValues { get; set; }

        public string User { get; set; }
        public string Pass { get; set; }
    }

    public class paramValues
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public string type_out { get; set; }


    }

}
