using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using SoftCandy.Enums;
using System.Threading;

namespace SoftCandy.Utils
{
    public static class LoginAtual
    {
        public static bool IsAdministrador(ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated && user.HasClaim(ClaimTypes.Actor, ((int)CargosEnum.ADMINISTRADOR).ToString());
        }

        public static bool IsEstoquista(ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated && user.HasClaim(ClaimTypes.Actor, ((int)CargosEnum.ESTOQUISTA).ToString());
        }

        public static bool IsVendedor(ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated && user.HasClaim(ClaimTypes.Actor, ((int)CargosEnum.VENDEDOR).ToString());
        }

        public static bool IsCaixa(ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated && user.HasClaim(ClaimTypes.Actor, ((int)CargosEnum.CAIXA).ToString());
        }
        public static int Id(ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
