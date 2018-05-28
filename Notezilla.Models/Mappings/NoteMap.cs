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
            Map(n => n.Title).Length(255);
            Map(n => n.Text).Length(int.MaxValue);
            Map(n => n.ChangeDate);
            Map(n => n.CreationDate);
            References(n => n.Author);
            HasMany(n => n.Files).KeyColumn("File");
            HasManyToMany(n => n.Tags);
        }
    }
}
