using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.SimplePerformanceTester
{
    using System.Diagnostics;

    using NEventStore;

    using Notifications.Domain;
    using Notifications.Messages.Commands;
    using Notifications.Storage;

    class Program
    {
        static void Main(string[] args)
        {
            var max = 10000;
            var ints = new List<int>(max);
            for (int i = 0; i < max; i++)
            {
                ints.Add(i);
            }
            var comdHandler = FakedDi.GetPersonCommandHandler;
            var sw = new Stopwatch();
            Console.WriteLine("Will add some data to database..");
            sw.Start();
            Parallel.ForEach(
                ints,
                (i) =>
                    {
                        var cmd = GetRandomAddCommands();
                        foreach (var addFireBaseTokenCommand in cmd)
                        {
                            comdHandler.Handle(addFireBaseTokenCommand);
                        }
                    });
            sw.Stop();  
            Console.WriteLine("Testing done.. it took " + sw.Elapsed + " or " + sw.ElapsedMilliseconds + " milliseconds to add " + max + " people with three notifications each");
            Console.ReadLine();
        }

        private static IEnumerable<AddFireBaseTokenCommand> GetRandomAddCommands()
        {
            var pno = RandomString(12);
            var firebaseToken = RandomString(150);

            yield return new AddFireBaseTokenCommand(pno, firebaseToken, Guid.NewGuid(),  "not1");
            yield return new AddFireBaseTokenCommand(pno, firebaseToken, Guid.NewGuid(), "not2");
            yield return new AddFireBaseTokenCommand(pno, firebaseToken, Guid.NewGuid(), "not3");

        }

        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    

    public static class FakedDi
    {
        private static readonly object LockObject = new object();

        private static PipeLineHook pipleLineHook;

        private static IStoreEvents store;

        private static PersonalNumberAndTokenReadModell personalNumberAndTokenReadModell;

        private static EventStoreSubscriber eventStoreSubscriber;

        private static PersonExecutor personExecutor;

        private static bool isReady = false;

        private static PersonCommandHandler personCommandHandler;

        public static PersonCommandHandler GetPersonCommandHandler
        {
            get
            {
                if (isReady == false)
                {
                    SetUp();
                }
                return personCommandHandler;
            }
        }

        public static PersonalNumberAndTokenReadModell PersonalNumberAndTokenReadModell
        {
            get
            {
                if (isReady == false)
                {
                    SetUp();
                }
                return personalNumberAndTokenReadModell;
            }
        }

        private static void SetUp()
        {
            lock (LockObject)
            {
                pipleLineHook = new PipeLineHook();
                store = new EventStoreFactory(pipleLineHook).GetStore();
                personalNumberAndTokenReadModell = new PersonalNumberAndTokenReadModell();
                PeopleReadModellsSubscriber peopleReadModellsSubscriber =
                    new PeopleReadModellsSubscriber(personalNumberAndTokenReadModell);
                eventStoreSubscriber = new EventStoreSubscriber(store, pipleLineHook);
                eventStoreSubscriber.Subscribe(peopleReadModellsSubscriber);

                personExecutor = new PersonExecutor(store, null);
                personCommandHandler = new PersonCommandHandler(personExecutor, personalNumberAndTokenReadModell);
                isReady = true;
            }
        }
    }
}
