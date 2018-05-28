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

        public virtual string Name { get; set; }

        public virtual File Picture { get; set; }

        public virtual Role Role { get; set; }

        public virtual Status Status { get; set; }
    }
}
