﻿namespace Notifications.SimplePerformanceTester
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Messages.Commands;

    using Notifications.Storage;

    public class Program
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private static readonly Random Random = new Random();

        public static void Main(string[] args)
        {
            Console.WriteLine("warm up");
            var max = 10000;
            var ints = new List<int>(max);
            for (int i = 0; i < max; i++)
            {
                ints.Add(i);
            }
            var swStartup = new Stopwatch();
            swStartup.Start();
            var comdHandler = FakedDi.GetPersonCommandHandler;
            swStartup.Stop();
            var sw = new Stopwatch();
            Console.WriteLine("Warmup took " + swStartup.Elapsed + " or " + swStartup.ElapsedMilliseconds + "ms");
            
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
            Console.WriteLine("Testing done.. it took " + sw.Elapsed + " or " + sw.ElapsedMilliseconds + " milliseconds to add " + max + " people with three notifications each, startup was " + swStartup.ElapsedMilliseconds);
           Console.WriteLine("Done..");
            Console.ReadLine();
        }

        private static IEnumerable<AddFireBaseTokenCommand> GetRandomAddCommands()
        {
            var pno = RandomString(12);
            var firebaseToken = RandomString(150);

            yield return new AddFireBaseTokenCommand(pno, firebaseToken, Guid.NewGuid(), "not1");
            yield return new AddFireBaseTokenCommand(pno, firebaseToken, Guid.NewGuid(), "not2");
            yield return new AddFireBaseTokenCommand(pno, firebaseToken, Guid.NewGuid(), "not3");
        }

        private static string RandomString(int length)
        {
            return new string(Enumerable.Repeat(Chars, length)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
