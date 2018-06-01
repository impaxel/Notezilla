using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Notezilla.Models.Users;

namespace Notezilla.Models.Mappings
{
    public class PictureMap : ClassMap<Picture>
    {
        public PictureMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            References(p => p.User);
            Map(p => p.Content);
        }
    }
}
