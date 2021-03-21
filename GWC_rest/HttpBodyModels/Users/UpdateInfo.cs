using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GWC_rest.Models;

namespace GWC_rest.HttpBodyModels.Users
{
    public class UpdatePublicInfo
    {
        public string Nickname { get; set; }
        public long Birthday { get; set; }
        public Image Avatar { get; set; }
    }
}
