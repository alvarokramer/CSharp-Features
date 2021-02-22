using System;
using System.Collections.Generic;
using System.Threading.Tasks;

Console.WriteLine("Press any key to get the messages in an yield return fashion.");
Console.ReadLine();

foreach (var message in YieldReturnMessages())
    Console.WriteLine(message);

Console.WriteLine("Press any key to get the messages in an await and yield return fashion.");
Console.ReadLine();

await foreach (var message in AwaitAndYieldReturnMessages())
    Console.WriteLine(message);

static IEnumerable<string> YieldReturnMessages()
{
    yield return "Juntos";
    yield return "Somos";
    yield return "Mais!";
}

static async IAsyncEnumerable<string> AwaitAndYieldReturnMessages()
{
    await Task.Delay(1000);
    yield return "Juntos";
    await Task.Delay(1000);
    yield return "Somos";
    await Task.Delay(1000);
    yield return "Mais!";
}