using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GWC_rest.Models
{
    public class AccountRoles
    {
        public static string User = "user";
        public static string Admin = "admin";
    }
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public int RegistrationDate { get; set; }
        public int Bithday { get; set; }
        public Image Avatar { get; set; }
    }
}
