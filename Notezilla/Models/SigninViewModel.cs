using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notezilla.Models
{
    public class SigninViewModel
    {
        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessage = "Необходимо указать имя пользователя")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Необходимо ввести пароль")]
        public string Password { get; set; }
    }
}