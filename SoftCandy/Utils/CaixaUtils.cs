using SoftCandy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Utils
{
    public class CaixaUtils
    {
        private static bool IsAberto(SoftCandyContext _context) {
            return _context.Caixa.Any(c => c.EstaAberto);
        }

        public static int IdAberto(SoftCandyContext _context)
        {
            if (IsAberto(_context))
            {
                return _context.Caixa.Where(c => c.EstaAberto).First().IdCaixa;
            }
            return 0;
        }
    }
}
