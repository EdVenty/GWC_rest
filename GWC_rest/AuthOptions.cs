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
        const string KEY = "dVa gEyA_Regha1uT,kTo_Samii_geyskiI_ge!1";// ключ для шифрации
        public const int LIFETIME = 525600; // время жизни токена - год
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
