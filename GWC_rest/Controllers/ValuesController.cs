using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GWC_rest.Models;
using GWC_rest.Data;
using System.Linq;
using GWC_rest.Errors;
using System.Collections.Generic;

namespace GWC_rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        UserContext db;
        
        public ValuesController(UserContext accountContext)
        {
            db = accountContext;
        }

        [Authorize]
        [Route("getlogin")]
        [HttpPost]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        private User getUserById(int id)
        {
            //Account person = people.FirstOrDefault(x => x.Login == username && x.Password == password);
            User person = db.Users.FirstOrDefault(x => x.Id == id);
            if (person != null)
            {
                return person;
            }

            // если пользователя не найдено
            return null;
        }

        [Route("getuserinfo")]
        [HttpPost]
        public IActionResult GetUserInfo(int userId, string[] userInfoFields)
        {
            var user = getUserById(userId);
            if (user == null)
            {
                return StatusCode(UsersError.UserNotFound.Code, UsersError.UserNotFound.Description);
            }

            var jsonResult = new Dictionary<string, object>();
            foreach (string param in userInfoFields)
            {
                switch (param.ToLower())
                {
                    case "id":
                        jsonResult.Add("id", user.Id);
                        break;
                    case "nickname":
                        jsonResult.Add("nickname", user.Nickname);
                        break;
                    case "bithday":
                        jsonResult.Add("bithday", user.Bithday);
                        break;
                }
            }
            return Json(jsonResult);
        }

        /*[Authorize]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok($"Ваша роль: {}");
        }*/
    }
}