using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace SistemaRepuesto.Models
{
    public static class Utils
    {
        public static string Encriptar(string texto)
        {
            string result = string.Empty;
            byte[] encryted = Encoding.Unicode.GetBytes(texto);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public static string DesEncriptar(string texto)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(texto);
            //result = Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = Encoding.Unicode.GetString(decryted);
            return result;
        }

    }
}