using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Notifications.Messages.Events;
using Notifications.Messages.Events.Person;

namespace Notifications.Domain.ReadModell
{
    public interface IPersonalNumberAndTokenReadModell
    {
        IEnumerable<PersonWithToken> GetTokenUsage(string firebaseToken);

        IEnumerable<PersonWithToken> GetPerson(string personalNumber);

        IEnumerable<PersonWithToken> GetAll();
    }


}