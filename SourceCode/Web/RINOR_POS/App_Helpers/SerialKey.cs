using System.Security.Cryptography;
using System.Text;

namespace RINOR_POS.App_Helpers
{
    public class SerialKey
    {
        public static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }

        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;

            for (int i = 0; i < bt.Length - 8; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 5) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'1')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'1')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length - 8 && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }
    }
}