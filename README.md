# CSharp-Features

## [Switch Expressions](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression)

## [Tuple](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-tuples)

## [Init only setters](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/init)

## [Deconstructing](https://docs.microsoft.com/en-us/dotnet/csharp/deconstruct#deconstructing-user-defined-types)

## [Index e Ranges](https://docs.microsoft.com/en-us/do)

## [Nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#nullable-reference-types)

## [Asynchronous streams](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#asynchronous-streams)

This feature was introduced in c# 8 and the purpose was enable to the developer create and consume streams asynchronously. The problem which this feature solved was: before `Asynchronous streams` c# was able to provide enumerables (which are synchronous) and task/async/await (which are asyncronous), but was not able to provide an asynchronous work during enumeration. Tasks only produce a result once, enumerables can generate multiple results the `Asynchronous streams` brings to the developer the possibility to generate multiple results asynchronously.

In the past, if you needed multiple results from a method probably the solution was to declare an `IEnumerable<>` return to the method together with the use of `yield return` modifier:

``` csharp
using System;
using System.Collections.Generic;

namespace asynchronous_streams
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Press any key to get the messages in an yield return fashion.");
            Console.ReadLine();

            foreach (var message in YieldReturnMessages())
                Console.WriteLine(message);
        }

        static IEnumerable<string> YieldReturnMessages()
        {
            yield return "Avenue";
            yield return "Code";
            yield return "Rocks!";
        }
    }
}
```

In an `asynchronous streams` fashion it's possible to consume multiple results asynchronously. The only thing you need to do is: declare an `IAsyncEnumerable<>` return to the method, and add an `await` modifier to consume the multiple messages:

``` csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace asynchronous_streams
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Press any key to get the messages in an await and yield return fashion.");
            Console.ReadLine();

            await foreach (var message in AwaitAndYieldReturnMessages())
                Console.WriteLine(message);
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
```

You can find the source code [here](https://github.com/alvarokramer/CSharp-Features).

## [Record types](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#record-types)
