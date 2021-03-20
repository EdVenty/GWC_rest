using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GWC_rest.Errors
{
    public class Codes
    {
        public static int Unknown = 0;
        public static int LoginLength = 422;
        public static int PasswordLength = 422;
        public static int AlreadyRegistered = 409;
        public static int WrongLoginOrPassword = 401;
        public static int UserNotFound = 404;
    }
}
