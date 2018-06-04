using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notezilla.Models
{
    public class NoteEditViewModel
    {
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Необходимо ввести название заметки")]
        public string Title { get; set; }

        [Display(Name = "Заметка")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Display(Name = "Теги")]
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