# C# Features from the lastest versions

C# 9 was recently released with new great features and shows once again the fast evolution of the programming language.
In this article we present a curated list of new and not so new features added to the C# over the years.

## [Switch Expressions](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression)

Switch Expressions is a feature added in C# 8 that provides a concise way of creating switch like statements. It's no longer necessary the use of the `case` and `break` keyworks and the result is a more plesent sintax for the programmer.

```c#
var interestingFact = DateTime.Today.DayOfWeek switch
{
	DayOfWeek.Monday => "Day of the moon",
	DayOfWeek.Tuesday => "The English name is derived from Old English Tiwesdæg",
	DayOfWeek.Wednesday => "It's named after Odin",
	DayOfWeek.Thursday => "It's named after Thor",
	DayOfWeek.Friday => "The name comes from the Old English Frīġedæġ",
	_ => "Yey, weekend!"
};
```

Combined with [pattern matching](https://docs.microsoft.com/en-us/dotnet/csharp/pattern-matching) it allows creating complex statements. It's important to note that the switch expression arms are evaluated in text order.

```c#
static decimal GetTollPrice(IVehicle vehicle)
{
    return vehicle switch
    {
        Motorcycle => 2.00m,
        Car car when car.Weight < 100 => 4.00m,
        Car car when car.Weight < 200 => 6.00m,
        Car => 8.00m,
        _ => 10.00m
    };
}
```

## [Tuple](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-tuples)

## [Init only setters](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/init)

The init only concept brings the flexibility for immutable model in C#.
It makes simpler the read-only for properties, structs and indexers once an object has been created.

In the code below, the constructor would no longer be necessary using unit:

## [Deconstructing](https://docs.microsoft.com/en-us/dotnet/csharp/deconstruct#deconstructing-user-defined-types)

## [Index e Ranges](https://docs.microsoft.com/en-us/do)

## [Nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#nullable-reference-types)

## [Asynchronous streams](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#asynchronous-streams)

This feature was introduced in c# 8 and the purpose was enable to create and consume streams asynchronously. The problem which this feature solved was: before `Asynchronous streams` c# was able to provide enumerables (which are synchronous) and task/async/await (which are asyncronous), but was not able to provide an asynchronous work during enumeration. Tasks only produce a result once, enumerables can generate multiple results the `Asynchronous streams` brings to the developer the possibility to generate multiple results asynchronously.

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
