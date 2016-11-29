using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.DbMigrator
{
    using System.Reflection;

    using DbUp;

    class Program
    {
        static int Main(string[] args)
        {
            var connectionString =
      args.FirstOrDefault()
      ?? "Server=(local); Database=DbUpTesting; Trusted_connection=true";
            EnsureDatabase.For.SqlDatabase(connectionString);
            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
    }
}
