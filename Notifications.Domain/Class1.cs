using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEventStore;
using NEventStore.Persistence.Sql;

namespace Notifications.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    using NEventStore.Client;

    using NLog;

    using Notifications.Messages;

    public class EventStoreFactory
    {
        public IStoreEvents GetStore()
        {
            var ddc = new PipeLineHook();
         //   ddc.Subscribe(new SimpleObserver());
            return Wireup.Init()
                .UsingSqlPersistence(
                    "Persist Security Info=False;Integrated Security=SSPI;database=Notifications;server=(local)")
                .UsingJsonSerialization()
                .HookIntoPipelineUsing(ddc)
                .Build();
        }
    }

    public class PersonWithToken
    {
        public PersonWithToken(string personalNumber, string token)
        {
            PersonalNumber = personalNumber;
            Token = token;
        }
        

        private sealed class PersonalNumberTokenEqualityComparer : IEqualityComparer<PersonWithToken>
        {
            public bool Equals(PersonWithToken x, PersonWithToken y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.PersonalNumber, y.PersonalNumber) && string.Equals(x.Token, y.Token);
            }

            public int GetHashCode(PersonWithToken obj)
            {
                unchecked
                {
                    return ((obj.PersonalNumber != null ? obj.PersonalNumber.GetHashCode() : 0) * 397) ^ (obj.Token != null ? obj.Token.GetHashCode() : 0);
                }
            }
        }


        private static readonly IEqualityComparer<PersonWithToken> PersonalNumberTokenComparerInstance = new PersonalNumberTokenEqualityComparer();

        public static IEqualityComparer<PersonWithToken> PersonalNumberTokenComparer
        {
            get
            {
                return PersonalNumberTokenComparerInstance;
            }
        }

        protected bool Equals(PersonWithToken other)
        {
            return string.Equals(PersonalNumber, other.PersonalNumber) && string.Equals(Token, other.Token);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PersonWithToken)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((PersonalNumber != null ? PersonalNumber.GetHashCode() : 0) * 397) ^ (Token != null ? Token.GetHashCode() : 0);
            }
        }

        public string PersonalNumber { get; }

        public string Token { get; }
    }

    public class PeopleReadModelsHandler
    {
        public PeopleReadModelsHandler()
        {
            PeopleWithTokens = new List<PersonWithToken>();
        }
        public List<PersonWithToken> PeopleWithTokens { get; }

        public void Handle(Event evt)
        {
            TokenAdd(evt);
            TokenRemove(evt);
        }

        private void TokenAdd(Event evt)
        {
            var tokenAdded = evt as FirebaseTokenAdded;
            if (tokenAdded != null)
            {
                var pwt = new PersonWithToken(tokenAdded.PersonalNumber, tokenAdded.FirebaseToken);
                if (PeopleWithTokens.Contains(pwt) == false)
                {
                    PeopleWithTokens.Add(pwt);
                }
            }
        }

        private void TokenRemove(Event evt)
        {
            var firebaseTokenRemoved = evt as FirebaseTokenRemoved;
            if (firebaseTokenRemoved != null)
            {
                var pwt = new PersonWithToken(firebaseTokenRemoved.PersonalNumber, firebaseTokenRemoved.FirebaseToken);

                if (PeopleWithTokens.Contains(pwt))
                {
                    PeopleWithTokens.Remove(pwt);
                }
            }
        }
    }

    public class ObserverForPollingClient : IObserver<ICommit>
    {
        private readonly IObserveCommits _observeCommits;

        public ObserverForPollingClient(IObserveCommits observeCommits)
        {
            _observeCommits = observeCommits;

        }

        public void OnNext(ICommit value)
        {
            _observeCommits.PollNow();
        }

        public void OnError(Exception error)
        {
           
        }

        public void OnCompleted()
        {
           _observeCommits.Dispose();
        }
    }

    public class PipeLineHook : PipelineHookBase
    {
        private readonly List<IObserver<ICommit>> _messageObservers;
        
        private Logger log;

        public PipeLineHook()
        {
            _messageObservers = new List<IObserver<ICommit>>();
            log = NLog.LogManager.GetCurrentClassLogger();
        }

        public override void PostCommit(ICommit committed)
        {
            
            log.Info($"Post commit {committed.BucketId} {committed.StreamId}");
            foreach (var messageObserver in _messageObservers)
            {
                messageObserver.OnNext(committed);
            }
        }

        public IDisposable Subscribe(IObserver<ICommit> observer)
        {
            if (!_messageObservers.Contains(observer))
            {
                _messageObservers.Add(observer);
            }

            return new Unsubscriber(_messageObservers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<ICommit>> _observers;
            private readonly IObserver<ICommit> _observer;

            public Unsubscriber(List<IObserver<ICommit>> observers, IObserver<ICommit> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if ((_observer != null) && _observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }
    }
}
