using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notezilla.Models.Users
{
    public class Picture
    {
        public virtual long Id { get; set; }

        public virtual User User { get; set; }

        public virtual byte[] Content { get; set; }
    }
}
