using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Notezilla.Models.Users;

namespace Notezilla.Models.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(u => u.Id).GeneratedBy.Identity();
            Map(u => u.UserName).Length(32);
            Map(u => u.Password).Column("PasswordHash");
            Map(u => u.Name).Length(32);
            HasManyToMany(u => u.Roles)
                .ParentKeyColumn("UserId")
                .ChildKeyColumn("RoleId");
            Map(u => u.Status);
            References(u => u.Picture);
        }
    }
}
