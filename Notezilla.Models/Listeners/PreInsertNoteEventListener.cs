using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Event;
using Notezilla.Models.Notes;
using Notezilla.Models.Repositories;

namespace Notezilla.Models.Listeners
{
    [Listener]
    public class PreInsertNoteEventListener : IPreInsertEventListener
    {
        public bool OnPreInsert(PreInsertEvent @event)
        {
            return SetCreationProps(@event);
        }

        public Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
        {
            return Task.FromResult(SetCreationProps(@event));
        }

        private bool SetCreationProps(PreInsertEvent @event)
        {
            var note = @event.Entity as Note;
            if (note != null)
            {
                note.CreationDate = DateTime.Now;
            }
            return false;
        }
    }
}
