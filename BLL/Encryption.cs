using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Encryption
    {
        public static string EncryptData(string Data)
        {
            //return Data;
            return Utilities.Security.EncryptData(Data);
        }

        public static string DencryptData(string Data)
        {
            //return Data;
            return Utilities.Security.DecryptData(Data);
        }
    }
}
