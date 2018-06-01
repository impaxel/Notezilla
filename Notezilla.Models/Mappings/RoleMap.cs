using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Notezilla.Models.Users;

namespace Notezilla.Models.Mappings
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(r => r.Id).GeneratedBy.Identity();
            Map(r => r.Name).Length(32);
            HasManyToMany(r => r.Users)
                .ParentKeyColumn("Role_id")
                .ChildKeyColumn("User_id");
        }
    }
}
