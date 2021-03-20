using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GWC_rest.Errors
{
    public class AuthorizationError
    {
        public static Error LoginLength = new Error(Codes.LoginLength, "Login must be 6 symbols at least");
        public static Error PasswordLength = new Error(Codes.PasswordLength, "Password must be 8 symbols at least");
        public static Error AlreadyRegistered = new Error(Codes.AlreadyRegistered, "User with this login has already registered");
        public static Error WrongLoginOrPassword = new Error(Codes.WrongLoginOrPassword, "Invalid username or password.");
        public static Error Unknown = new Error(Codes.Unknown, "Unknown error");
    }
}
