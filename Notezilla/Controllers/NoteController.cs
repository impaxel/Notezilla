using System;
using System.Collections.Generic;
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
                var user = userRepository.GetCurrentUser(User);
                var note = new Note
                {
                    Title = model.Title,
                    Text = model.Text,
                    Tags = model.Tags,
                    Author = user
                };
                noteRepository.Save(note);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Note
        public ActionResult Index()
        {
            var user = userRepository.GetCurrentUser(User);
            var model = new NoteListViewModel
            {
                Notes = noteRepository.GetAll(user)
            };
            return View(model);
        }
    }
}