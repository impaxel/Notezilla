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
            Map(u => u.RegistrationDate);
            Map(u => u.Password).Column("PasswordHash");
            HasMany(u => u.Notes).KeyColumn("User_id");
            HasManyToMany(u => u.Roles)
                .ParentKeyColumn("User_id")
                .ChildKeyColumn("Role_id");
            Map(u => u.IsEnabled);
        }
    }
}
