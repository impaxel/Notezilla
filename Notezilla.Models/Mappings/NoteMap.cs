using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Notezilla.Models.Notes;

namespace Notezilla.Models.Mappings
{
    public class NoteMap : ClassMap<Note>
    {
        public NoteMap()
        {
            Id(n => n.Id).GeneratedBy.Identity();
            Map(n => n.Title).Length(64);
            Map(n => n.Text).Length(4001);
            Map(n => n.ChangeDate);
            Map(n => n.CreationDate);
            Map(n => n.Tags).Length(255);
            References(n => n.Author).Column("User_id");
            HasMany(n => n.Files).KeyColumn("Note_id").Cascade.All();
        }
    }
}
