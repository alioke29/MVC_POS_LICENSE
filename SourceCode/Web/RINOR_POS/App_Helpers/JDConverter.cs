using System;
using System.Globalization;
using System.Threading;

namespace RINOR_POS.App_Helpers
{
    /// <summary>
    /// Julian Date Converter
    /// </summary>
    public class JDConverter
    {
        /// <summary>
        /// Convert From Date to Julian Date
        /// </summary>
        /// <param name="date">datetime</param>
        /// <returns>Double as Julian</returns>
        public static double DateToJulian(DateTime date)
        {
            CultureInfo ci = new CultureInfo("en-EN");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            return date.ToOADate() + 2415018.5;
        }

        /// <summary>
        /// Convert JulianDate
        /// </summary>
        /// <param name="JD"></param>
        /// <returns></returns>
        public static DateTime JulianToDate(double JD)
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