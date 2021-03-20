using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GWC_rest.Models;
using GWC_rest.Data;
using System.Linq;
using GWC_rest.Errors;
using System.Collections.Generic;
using System.IO;

namespace GWC_rest.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class ValuesController : Controller
    {
        

        /*[Authorize]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok($"Ваша роль: {}");
        }*/
    }
}