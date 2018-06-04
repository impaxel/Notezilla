using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Notezilla.Models.Notes;

namespace Notezilla.Models.Users
{
    public class User : IUser<long>
    {
        public virtual long Id { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }

        public virtual DateTime RegistrationDate { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        public virtual bool IsEnabled { get; set; } = true;

        public User()
        {
            Roles = new List<Role>();
            Notes = new List<Note>();
        }

        public User(string userName) : this()
        {
            UserName = userName;
        }
    }
}
