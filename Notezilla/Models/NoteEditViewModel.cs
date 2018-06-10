using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notezilla.Models
{
    public class NoteEditViewModel
    {
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Необходимо ввести название заметки")]
        [StringLength(64, ErrorMessage = "Длина заголовка не должна превышать 64 символа")]
        public string Title { get; set; }

        [AllowHtml]
        [Display(Name = "Заметка")]
        [DataType(DataType.MultilineText)]
        [StringLength(int.MaxValue, ErrorMessage = "Содержимое заметки слишком большое")]
        public string Text { get; set; }

        [Display(Name = "Теги")]
        [StringLength(255, ErrorMessage = "Содержимое тегов слишком большое")]
        public string Tags { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Добавить файлы")]
        public ICollection<HttpPostedFileBase> Files { get; set; }

        public NoteEditViewModel()
        {
            Files = new List<HttpPostedFileBase>();
        }
    }
}