using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using SoftCandy.Enums;

namespace SoftCandy.Utils
{
    public static class LogadoComo
    {
        public static bool Administrador(ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated && user.HasClaim(ClaimTypes.Actor, Atores.ADMINISTRADOR.ToString());
        }

        public static bool Estoquista(ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated && user.HasClaim(ClaimTypes.Actor, Atores.ESTOQUISTA.ToString());
        }

        public static bool Vendedor(ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated && user.HasClaim(ClaimTypes.Actor, Atores.VENDEDOR.ToString());
        }
    }
}
