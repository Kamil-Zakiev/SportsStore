using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}