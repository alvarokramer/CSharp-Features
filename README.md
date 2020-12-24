# C# Features from the latest versions

C# 9 was recently released with new great features and shows once again the fast evolution of the programming language.
In this article we present a curated list of new and not so new features added to the C# over the years.

## [Switch Expressions](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression)

Switch Expressions is a feature added in C# 8 that provides a concise way of creating switch like statements. It's no longer necessary the use of the `case` and `break` keyworks and the result is a more plesent sintax for the programmer.

``` csharp
var interestingFact = DateTime.Today.DayOfWeek switch
{
    DayOfWeek.Monday => "Day of the moon",
    DayOfWeek.Tuesday => "Is derived from Old English for Tiw's day",
    DayOfWeek.Wednesday => "It's named after Odin",
    DayOfWeek.Thursday => "It's named after Thor",
    DayOfWeek.Friday => "Comes from the Old English that means day of Frige",
    _ => "Yey, weekend!"
};
```

Combined with [pattern matching](https://docs.microsoft.com/en-us/dotnet/csharp/pattern-matching) it allows creating complex statements. It's important to note that the switch expression arms are evaluated in text order.

``` csharp
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

The Tuple type is a C# feature available from C# 7 and provides a good syntax to group multiple data elements or when you want to have a data structure containing the properties of an object but without having to create the object itself.

In the code below, we used the Tuple type to create a simple API filter validation class, with Initial Date, Final Date and Page Number, that applies the correct rules and returns the original data with possible error messages and a boolean indicating whether the validation went right or wrong. This was a good option for a filter without having to use any external libraries like FluentValidation, for a relatively simple API with low complexity.

```c#
public static class ParamsValidator
    {
        public static Tuple<bool, string, DateTime?, DateTime?, string> Validate(DateTime? startDate, DateTime? endDate, int page)
        {
            //initiates the tuple with the types it must have
            var result = new Tuple<bool, string, DateTime?, DateTime?, string>(true, "", startDate, endDate, sellerToken);

            //Validates the date parameters
            if (startDate.HasValue && endDate.HasValue && endDate < startDate)
                result = new Tuple<bool, string, DateTime?, DateTime?, string>(false, "EndDate should be greater than StartDate.", startDate, endDate, sellerToken);

            if(!startDate.HasValue && !endDate.HasValue)
                result = new Tuple<bool, string, DateTime?, DateTime?, string>(true, "", DateTime.Now.AddDays(-7), DateTime.Now, sellerToken);

            //Validates the Page Number parameter
            if (page <= 0)
                result = new Tuple<bool, string, DateTime?, DateTime?, string>(false, "Page number should be greater than zero.", startDate, endDate, sellerToken);

            return result;
        }
    }
```

It's important to remember that the Tuple Type can only hold 8(eight) parameters at a time and it will throw an exception if you try to add more values to it. 

## [Init only setters](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/init)

The init only concept brings the flexibility for immutable model in C#.
It makes simpler the read-only for properties, structs and indexers once an object has been created.

This is a sample just using get in properties to make them read only:

``` csharp
struct Dimension
{
   public int Width { get; }
   public int Height { get; }

   public Dimension(int width, int height)
   {
      this.Width = width;
      this.Height = height;
   }
}
```

Then the object initializer has to be via constructor:

``` csharp
var dimension = new Dimension(10, 10);
```

In the code below using init, the constructor would no longer be necessary using unit only properties:

``` csharp
struct Dimension
{
   public int Width { get; init; }
   public int Height { get; init; }
}
```

Then the object initializer can be used like this:

``` csharp
var dimension = new Dimension() { Width = 10, Height = 10 };
```

## [Deconstructing](https://docs.microsoft.com/en-us/dotnet/csharp/deconstruct#deconstructing-user-defined-types)

## [Index e Ranges](https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/ranges-indexes)

## [Nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#nullable-reference-types)

Firstly it's important to separate two concepts, [nullable value types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types) are available since c# 2 and it's different from what we want to approach. This topic is about [nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#nullable-reference-types), for more information on the two main categories of c# types, check the [Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-types).

The nullable and non-nullable reference types were introduced in c# 8. This feature works based on compiler enforcement rules, that is:

1) When the variable isn't supposed to be null:

- the compiler enforces the variable to be initialized with a non-null value;

- it can never be assigned the value null;

- the compiler doesn't launch any warnings when the variable is initialized to null;

- the compiler doesn't launch any warnings when the variable is assigned to null;

- the compiler launches warnings when the variable is dereferenced without null checks.

2) On the other hand, to the nullable variables:

- it may be initialized with null value;

- can be assigned to null after initialization;

- the variable may only be dereferenced when the compiler can guarantee that the value isn't null;

- the compiler doesn't launch any warnings when reference types are dereferenced;

- the compiler launches warnings if a variable is set to an expression that may be null.

The `nullable reference type` syntax notation is the same as the `nullable value types` notation: just insert a `?` appended to the variable's type as the example below:

``` csharp
  string? name;
```

## [Asynchronous streams](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#asynchronous-streams)

This feature was introduced in c# 8 and the purpose was enable to create and consume streams asynchronously. `Asynchronous streams` solved a problem which was: before it, the c# was able to provide enumerables (which are synchronous) and task/async/await (which are asyncronous), but was not able to provide an asynchronous work during enumeration. Tasks only produce a result once, enumerables can generate multiple results the `Asynchronous streams` bring the possibility to generate multiple results asynchronously.

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
