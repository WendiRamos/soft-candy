using SoftCandy.Data;
using SoftCandy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Utils
{
    public class CaixaUtils
    {
        public static bool IsAberto(SoftCandyContext _context) {
            return _context.Caixa.Any(c => c.EstaAberto);
        }

        public static bool IsFechado(SoftCandyContext _context)
        {
            return !IsAberto(_context);
        }

        public static int IdAberto(SoftCandyContext _context)
        {
            if (IsAberto(_context))
            {
                return _context.Caixa.Where(c => c.EstaAberto).First().IdCaixa;
            }
            return 0;
        }

        public static Caixa CaixaAberto(SoftCandyContext _context)
        {
            return _context.Caixa.FirstOrDefault(c => c.EstaAberto);
        }
    }
}
