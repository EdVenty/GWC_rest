using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GWC_rest.Errors
{
    public class Error
    {
        public int Code;
        public string Description;
        public Error(int code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
