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
    public class PreUpdateNoteEventListener : IPreUpdateEventListener
    {
        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            return SetProps(@event);
        }

        public Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
        {
            return Task.FromResult(SetProps(@event));
        }

        private bool SetProps(PreUpdateEvent @event)
        {
            var note = @event.Entity as Note;
            if (note != null)
            {
                note.ChangeDate = DateTime.Now;
            }
            return false;
        }
    }
}
