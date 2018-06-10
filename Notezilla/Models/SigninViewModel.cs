using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notezilla.Models
{
    public class SigninViewModel
    {
        [StringLength(32)]
        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessage = "Необходимо указать имя пользователя")]
        public string Login { get; set; }

        [StringLength(32)]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Необходимо ввести пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить пароль")]
        public bool RememberMe { get; set; }
    }
}