using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notezilla.Models.Notes
{
    public class Tag
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        //public virtual ICollection<Note> Notes { get; set; }

        //public Tag()
        //{
        //    Notes = new List<Note>();
        //}
    }
}
