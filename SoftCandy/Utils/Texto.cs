using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftCandy.Utils
{
    public static class Texto
    {
        public static bool CaseInsensitiveContains(string text, string value)
        {
            byte[] tmp = Encoding.GetEncoding("ISO-8859-8").GetBytes(value);
            string pesquisa = Encoding.UTF8.GetString(tmp);
            return text.IndexOf(pesquisa, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

    }
}
