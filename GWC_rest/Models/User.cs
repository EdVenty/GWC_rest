using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [MinLength(6, ErrorMessage = "Login length must be 6 symbols at least")]
        [MaxLength(32, ErrorMessage = "Login length must be 32 symbols maximum")]
        public string Login { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password length must be 8 symbols at least")]
        [MaxLength(32, ErrorMessage = "Password length must be 32 symbols maximum")]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public long RegistrationDate { get; set; }
        [Column("Bithday")]
        public long Birthday { get; set; }
        public virtual Image Avatar { get; set; }
    }
}
