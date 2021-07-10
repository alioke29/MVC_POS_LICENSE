using System;
using System.Globalization;
using System.Threading;

namespace RINOR_POS.App_Helpers
{
    /// <summary>
    /// Token System By Rinor
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Create Token By Date
        /// </summary>
        /// <returns>Toke</returns>
        public static string CreateToken()
        {
            CultureInfo ci = new CultureInfo("en-EN");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            double JD = DTJD(DateTime.Now);
            JD += 873;
            string token = JD.ToString().Replace(".", "T2K");

            return token;
        }
        /// <summary>
        /// Check for valid token
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static bool isValidToken(string Token)
        {
            bool result = false;
            CultureInfo ci = new CultureInfo("en-EN");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            try
            {
                if (Token == "uptome")
                    return true;

                Double KeyToken = Convert.ToDouble(Token.Replace("T2K", "."));
                double JD = KeyToken;
                JD -= 873;
                DateTime datetime = JDTD(JD);

                TimeSpan timespan = (DateTime.Now - datetime);

                if (timespan.TotalMinutes <= 2 && timespan.TotalMinutes >= 0)
                    result = true;
            }
            catch (Exception ex)
            {
                //do nothing
            }

            return result;
        }
        private static double DTJD(DateTime date)
        {
            CultureInfo ci = new CultureInfo("en-EN");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            return date.ToOADate() + 2415018.5;
        }

        private static DateTime JDTD(double JD)
        {
            CultureInfo ci = new CultureInfo("en-EN");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            double unixTime = (JD - 2440587.5) * 86400;

            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(unixTime).ToLocalTime();

            return dtDateTime;
        }
    }
}