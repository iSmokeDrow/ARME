using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARME
{
    public class Hexcnv
    {

        public static byte[] StringToByteArrayFastest(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            return val - (val < 58 ? 48 : 55);
        }

        public static int GetCoords(string name,int type)
        {
            int x=Convert.ToInt32(name.Substring(2,2));
            int y=Convert.ToInt32(name.Substring(6,2));
            if (type == 1)
                return x*16128;
            else
                return y*16128;


        }

        public static string StringToHex(string hexstring)
        {
            var sb = new StringBuilder();
            foreach (char t in hexstring)
                sb.Append(Convert.ToInt32(t).ToString("x") + " ");
            return sb.ToString();
        }
    }
}
