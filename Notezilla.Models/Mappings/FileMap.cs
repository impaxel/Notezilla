using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Notezilla.Models.Notes;

namespace Notezilla.Models.Mappings
{
    public class FileMap : ClassMap<File>
    {
        public FileMap()
        {
            Id(f => f.Id).GeneratedBy.Identity();
            Map(f => f.Name).Length(32);
            Map(f => f.Path).Length(int.MaxValue);
        }
    }
}
