using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GWC_rest.Errors
{
    public class AuthorizationError
    {
        public static object LoginLength = new { code = Codes.LoginLength, message = "Login must be 4 symbols at least" };
        public static object PasswordLength = new { code = Codes.PasswordLength, message = "Password must be 8 symbols at least" };
        public static object AlreadyRegistered = new { code = Codes.AlreadyRegistered, message = "User with this login has already registered" };

    }
}
