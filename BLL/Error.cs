using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Error
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

        public static Entities.Generic.Error SendSpecificError(string errorMessage)
        {
            Entities.Generic.Error ObjResponse = new Entities.Generic.Error()
            {
                number = "Pending to do",
                message = errorMessage
            };

            return ObjResponse;

        }

    }
}
