using System;
using System.Security.Cryptography;
using System_Text = System.Text;
using System.IO;

namespace Utilities
{
    public class Security
    {
        private static Byte[] key = { };
        private static Byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
        private static String sEncryptionKey = "!51m3x4?";

        ///<summary>
        ///Realiza el encriptado de un valor numérico (Encriptado Base 64)
        ///</summary>
        ///<param name="stringToEncrypt">Cadena de tipo numérico a encriptar</param>
        ///<returns>Retorna la cadena encriptada en Base 64</returns>
        public static String EncryptData(String stringToEncrypt)
        {
            try
            {

                key = System_Text.Encoding.UTF8.GetBytes(Left(sEncryptionKey, 8));
                var des = new DESCryptoServiceProvider(); // Remplazarlo con AesCryptoServiceProvider
                Byte[] inputByteArray = System_Text.Encoding.UTF8.GetBytes(stringToEncrypt);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                return "X_X" + Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        ///<summary>
        ///Desencripta un valor numérico (Encriptado Base 64)
        ///</summary>
        ///<param name="stringToDecrypt">Cadena de tipo numérico a desencriptar</param>
        ///<returns>Retorna la cadena desencriptada en Base 64</returns>
        public static String DecryptData(String stringToDecrypt)
        {
            try
            {

                Byte[] inputByteArray = new Byte[stringToDecrypt.Length];
                stringToDecrypt = stringToDecrypt.Replace("X_X", "");
                stringToDecrypt = stringToDecrypt.Replace(" ", "+");
                key = System_Text.Encoding.UTF8.GetBytes(Left(sEncryptionKey, 8));
                var des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System_Text.Encoding encoding = System_Text.Encoding.UTF8;

                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return String.Empty;
                //throw new Exception(ex.Message);
            }
        }

        private static string Left(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }

    }
}
