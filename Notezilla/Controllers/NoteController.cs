using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notezilla.Models;
using Notezilla.Models.Notes;
using Notezilla.Models.Repositories;

namespace Notezilla.Controllers
{
    [Authorize]
    public class NoteController : BaseController
    {
        private readonly NoteRepository noteRepository;

        public NoteController(NoteRepository noteRepository, UserRepository userRepository) : base(userRepository)
        {
            this.noteRepository = noteRepository;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NoteEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var files = new List<Models.Notes.File>();
                string filePath = Server.MapPath($"~/Uploaded Files/{ User.Identity.Name }/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                foreach (var file in model.Files)
                {
                    if (file != null)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        file.SaveAs(filePath + fileName);
                        files.Add(new Models.Notes.File(fileName, filePath));
                    }
                }
                var user = userRepository.GetCurrentUser(User);
                var note = new Note
                {
                    Title = model.Title,
                    Text = model.Text,
                    Tags = model.Tags,
                    Author = user,
                    Files = files
                };
                noteRepository.Save(note);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Note
        public ActionResult Index(FetchOptions options)
        {
            var user = userRepository.GetCurrentUser(User);
            var notes = noteRepository.GetAllByUser(user, options);
            var model = new NoteListViewModel
            {
                Notes = notes
            };
            return View(model);
        }
    }
}