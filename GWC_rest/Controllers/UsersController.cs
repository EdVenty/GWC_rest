using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GWC_rest.Models; // класс Person
using GWC_rest.Data;
using GWC_rest.Errors;
using System.IO;
using GWC_rest.HttpBodyModels.Users;

namespace GWC_rest.Controllers
{
    public class UsersController : Controller
    {
        // тестовые данные вместо использования базы данных
        /*private List<Account> people = new List<Account>
        {
            new Account {Login="admin@gmail.com", Password="12345", Role = "admin" },
            new Account { Login="qwerty@gmail.com", Password="55555", Role = "user" }
        };*/

        UserContext db;

        public UsersController(UserContext accountContext)
        {
            db = accountContext;
            if (db.Users.FirstOrDefault(x => x.Login == "admin") == null)
            {
                db.Users.Add(new User { Login = "admin", Password = "admin", Role = AccountRoles.Admin, Nickname = "admin", RegistrationDate = DateTimeOffset.Now.ToUnixTimeSeconds() });
                db.SaveChanges();
            }
        }

        [Produces("application/json")]
        [HttpPost("/auth/register")]
        public IActionResult Register([FromBody] User user)
        {
            if (user.Login.Length < 6)
            {
                return UnprocessableEntity(AuthorizationError.LoginLength.Description);
            }
            else if (user.Password.Length < 8)
            {
                return UnprocessableEntity(AuthorizationError.PasswordLength.Description);
            }
            if (HasUserRegistered(user.Login))
            {
                return Conflict(AuthorizationError.AlreadyRegistered.Description);
            }
            /*List<ImageSize> imageSizes = new List<ImageSize>(){new ImageSize()
                {
                    Height = 462,
                    Width = 800,
                    ItemId = "https://srv2-vl.cloudusercontent.chkdev.ru:2243/otval/data/avatar.jpg"
                }
            };
            var avatar = new Image()
            {
                CreationDate = DateTimeOffset.Now.ToUnixTimeSeconds(),
                Description = "Test avatar",
                Title = "Test avatar",
                Sizes = imageSizes
            };*/
            db.Users.Add(new User {
                Login = user.Login,
                Password = user.Password,
                Role = AccountRoles.User,
                Nickname = user.Login,
                RegistrationDate = DateTimeOffset.Now.ToUnixTimeSeconds(),
                Birthday = -1
            });
            db.SaveChanges();
            return Ok("User registered");
        }

        [Produces("application/json")]
        [HttpPost("/auth/login")]
        public IActionResult Login([FromBody] User user)
        {
            var identity = GetIdentity(user.Login, user.Password);
            if (identity == null)
            {
                return Unauthorized(AuthorizationError.WrongLoginOrPassword.Description);
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(encodedJwt);
        }

        private bool HasUserRegistered(string username)
        {
            User person = db.Users.FirstOrDefault(x => x.Login == username);
            if (person != null)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        [HttpGet("/users/getinfo")]
        public IActionResult GetUserInfo(string UserId, string[] InfoFields)
        {
            var user = getUserById(int.Parse(UserId));
            if (user == null)
            {
                return NotFound(UsersError.UserNotFound.Description);
            }

            var jsonResult = new Dictionary<string, object>();
            foreach (string param in InfoFields)
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
                        jsonResult.Add("bithday", user.Birthday);
                        break;
                    default:
                        jsonResult.Add(param, null);
                        break;
                }
            }
            return Json(jsonResult);
        }

        [HttpGet("/users/getmyid")]
        [Authorize]
        public IActionResult GetMyId()
        {
            User person = db.Users.FirstOrDefault(x => x.Login == User.Identity.Name);
            if (person == null)
            {
                return NotFound(UsersError.UserNotFound.Description);
            }
            return Ok(person.Id);
        }

        [Authorize]
        [HttpGet("/users/getprivateinfo")]
        public IActionResult GetPrivateInfo(string[] InfoFields)
        {
            User user = db.Users.FirstOrDefault(x => x.Login == User.Identity.Name);
            if (user == null)
            {
                return NotFound(UsersError.UserNotFound.Description);
            }

            var jsonResult = new Dictionary<string, object>();
            foreach (string param in InfoFields)
            {
                switch (param.ToLower())
                {
                    case "id":
                        jsonResult.Add("id", user.Id);
                        break;
                    case "nickname":
                        jsonResult.Add("nickname", user.Nickname);
                        break;
                    case "birthday":
                        jsonResult.Add("birthday", user.Birthday);
                        break;
                    case "login":
                        jsonResult.Add("login", user.Login);
                        break;
                    case "registrationdate":
                        jsonResult.Add("registrationdate", user.RegistrationDate);
                        break;
                    case "avatar":
                        jsonResult.Add("avatar", user.Avatar);
                        break;
                    default:
                        jsonResult.Add(param, null);
                        break;
                }
            }
            return Json(jsonResult);
        }

        [HttpPost("/users/updatepublicinfo")]
        public IActionResult UpdatePublicInfo([FromBody] UpdatePublicInfo update)
        {
            User user = db.Users.FirstOrDefault(x => x.Login == User.Identity.Name);
            if (user == null)
            {
                return NotFound(UsersError.UserNotFound.Description);
            }
            if (update.Nickname != null) user.Nickname = update.Nickname;
            if (update.Birthday >= 0 || update.Birthday == -1) user.Birthday = update.Birthday;
            if (update.Avatar != null) user.Avatar = update.Avatar;
            db.SaveChanges();
            return Ok("Private information successfully updated");
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            //Account person = people.FirstOrDefault(x => x.Login == username && x.Password == password);
            User person = db.Users.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}