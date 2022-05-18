using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Utils
{
    public sealed class Orcamento : Models.Pedido
    {

        private static readonly Orcamento instance = new Orcamento();

        private Orcamento() { }

        public static Orcamento Instance
        {
            get
            {
                return instance;
            }
        }
    }
}