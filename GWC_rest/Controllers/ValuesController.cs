using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GWC_rest.Models;

namespace GWC_rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        /*[Authorize]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok($"Ваша роль: {}");
        }*/
    }
}