using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GWC_rest.Errors
{
    public class UsersError
    {
        public static Error UserNotFound = new Error(Codes.UserNotFound, "User with specified id not found");
    }
}
