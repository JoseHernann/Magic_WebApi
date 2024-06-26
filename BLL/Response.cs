using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Response
    {
        public static Entities.Generic.Error SendError(Exception exe)
        {
            Entities.Generic.Error ObjResponse = new Entities.Generic.Error()
            {
                number = "Pending to do",
                message = exe.Message
            };

            return ObjResponse;

        }
    }
}
