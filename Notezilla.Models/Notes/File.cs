using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notezilla.Models.Notes
{
    public class File
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Path { get; set; }

        public virtual Note Note { get; set; }

        public File()
        {

        }

        public File(string name, string path) : this()
        {
            Name = name;
            Path = path;
        }
    }
}
