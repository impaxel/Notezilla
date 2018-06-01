using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notezilla.Models
{
    public class SignupViewModel
    {
        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessage = "Необходимо указать имя пользователя")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Необходимо ввести пароль")]
        [StringLength(32, ErrorMessage = "{0} должен иметь длину хотя бы {2} символов", MinimumLength = 5)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Required(ErrorMessage = "Необходимо подтвердить пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}