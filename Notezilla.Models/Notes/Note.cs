using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notezilla.Models.Users;

namespace Notezilla.Models.Notes
{
    public class Note
    {
        public virtual long Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Text { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual DateTime ChangeDate { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual string Tags { get; set; }

        public Note()
        {
            Files = new List<File>();
        }
    }
}
