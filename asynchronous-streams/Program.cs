using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace asynchronous_streams
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press any key to get the messages in an yield return fashion.");
            Console.ReadLine();

            foreach (var message in YieldReturnMessages())
                Console.WriteLine(message);

            Console.WriteLine("Press any key to get the messages in an await and yield return fashion.");
            Console.ReadLine();

            await foreach (var message in AwaitAndYieldReturnMessages())
                Console.WriteLine(message);
        }

        static IEnumerable<string> YieldReturnMessages()
        {
            yield return "Avenue";
            yield return "Code";
            yield return "Rocks!";
        }

        static async IAsyncEnumerable<string> AwaitAndYieldReturnMessages()
        {
            await Task.Delay(1000);
            yield return "Avenue";
            await Task.Delay(1000);
            yield return "Code";
            await Task.Delay(1000);
            yield return "Rocks!";
        }
    }
}
