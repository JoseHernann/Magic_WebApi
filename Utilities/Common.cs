using System;
using System.Text;

namespace Utilities
{
    public static class Common
    {
        private const long maxLL = 9223372036854775807; // max lenght of long object
        private static long ExceptionNumber = 0;

        public static string ExceptionID { get { return GetExceptionID(); } }

        private static string GetExceptionID()
        {
            if (ExceptionNumber >= maxLL)
                ExceptionNumber = 0;
            ExceptionNumber += 1;
            return string.Format("ts:{0}{1}{2}{3}{4}{5}{6}-id:{7}", System.DateTime.UtcNow.Year.ToString(), System.DateTime.UtcNow.Month.ToString(), System.DateTime.UtcNow.Day.ToString(),
                System.DateTime.UtcNow.Hour.ToString(), System.DateTime.UtcNow.Minute.ToString(), System.DateTime.UtcNow.Second.ToString(), System.DateTime.UtcNow.Millisecond.ToString(), ExceptionNumber.ToString());
        }

        public static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

        public static string Decryptdata(string text)
        {
            string res = Encoding.UTF8.GetString(Convert.FromBase64String(text));
            res = res.Replace("**", "\\");
            return res;
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }

        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        /// <summary>
        /// Casts any type to another type
        /// </summary>
        /// <typeparam name="T">The destination type</typeparam>
        /// <param name="o">The object to cast</param>
        /// <returns>The casted object</returns>
        public static T To<T>(this object o)
        {
            try
            {
                return (T)Convert.ChangeType(o, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }
    }
}
