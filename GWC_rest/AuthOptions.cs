using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWC_rest
{
    public class AuthOptions
    {
        public const string ISSUER = "GWCAuthServer"; // издатель токена
        public const string AUDIENCE = "GWCClient"; // потребитель токена
        const string KEY = "g435aYSegZ893g(83D;']]3=2-9unG;'Geo';;OO43';[Pllp45o4gng4509)-0MAshy4";// ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
