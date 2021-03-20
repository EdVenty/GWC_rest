using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GWC_rest.Models; // класс Person
using GWC_rest.Data;
using GWC_rest.Errors;

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
                db.Users.Add(new User { Login = "admin", Password = "admin", Role = AccountRoles.Admin });
                db.SaveChanges();
            }
        }

        [HttpPost("/register")]
        public IActionResult Register(string username, string password)
        {
            if (username.Length < 4)
            {
                return StatusCode(AuthorizationError.LoginLength.Code, AuthorizationError.LoginLength.Description);
            }
            else if (password.Length < 8)
            {
                return StatusCode(AuthorizationError.PasswordLength.Code, AuthorizationError.PasswordLength.Description);
            }
            if (HasUserRegistered(username))
            {
                return StatusCode(AuthorizationError.AlreadyRegistered.Code, AuthorizationError.AlreadyRegistered.Description);
            }
            db.Users.Add(new User { 
                Login = username, 
                Password = password, 
                Role = AccountRoles.User, 
                Nickname = username,  
                RegistrationDate = DateTimeOffset.Now.ToUnixTimeSeconds()
            });
            db.SaveChanges();
            return Ok();
        }

        [HttpPost("/login")]
        public IActionResult Login(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return StatusCode(AuthorizationError.WrongLoginOrPassword.Code, AuthorizationError.WrongLoginOrPassword.Description);
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

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
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