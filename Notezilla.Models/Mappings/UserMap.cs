using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernate.AspNet.Identity;
using Notezilla.Models.Users;

namespace Notezilla.Models.Mappings
{
    public class UserMap : SubclassMap<User>
    {
        public UserMap()
        {
            Map(u => u.RegistrationDate).Length(32);
            HasMany(u => u.Notes).KeyColumn("User_id");
        }
    }
}
