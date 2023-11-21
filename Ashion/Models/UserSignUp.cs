using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ashion.Models
{
    public class UserSignUp
    {
        public int UserId { get; set; }
       
        [Required]
        public string UserName { get; set; }
        [Required]    
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Remote("IsAlreadySigned", "UserAccount", HttpMethod = "POST", ErrorMessage = "EmailId already exists!")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}