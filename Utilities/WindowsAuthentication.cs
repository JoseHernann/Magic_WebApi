using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace Utilities
{
    public class WindowsAuthentication
    {
        private string sRuta;
        public string Error;

        public bool Validate(string sDominio, string sUsuario, string sPassword)
        {
            string sError = "";
            bool bResult = false;
            string sDomainUser = sDominio + @"\" + sUsuario;
            DirectoryEntry deEntry = new DirectoryEntry(sRuta, sDomainUser, sPassword);


            try
            {
                object objObjecto = deEntry.NativeObject;
                DirectorySearcher dsSearcher = new DirectorySearcher(deEntry);
                dsSearcher.Filter = "(SAMAccountName=" + sUsuario + ")";
                SearchResult srSearcher = dsSearcher.FindOne();

                if (srSearcher != null)
                    bResult = true;
                else
                    bResult = false;
                dsSearcher.Dispose();
            }
            catch (Exception ex)
            {
                sError = ex.Message.ToString();
                bResult = false;
            }

            finally
            {
                deEntry.Dispose();
            }


            return bResult;
        }

        public WindowsAuthentication(string sPath)
        {
            sRuta = sPath;
        }
    }
}
