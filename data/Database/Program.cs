using DbUp;
using System;
using System.IO;
using System.Linq;

namespace Database
{
    class Program
    {
        static int Main(string[] args)
        {
            var connectionString =
                args.FirstOrDefault() ?? "Server=localhost; Port=5432;Database=;User Id=;Password=;";

            EnsureDatabase.For.PostgresqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .PostgresqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(Path.Combine(Environment.CurrentDirectory.Replace(@"bin\Debug\net5.0", ""), "Scripts"))
                    //.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .WithTransaction()
                    .LogToConsole()
                    .LogScriptOutput()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadKey();
#endif
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            Console.ReadKey();
            return 0;
        }
    }
}
