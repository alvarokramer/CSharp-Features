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

This is a sample just using get in properties to make them read only:

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

In the code below using init, the constructor would no longer be necessary using unit only properties:

struct Dimension
{
   public int Width { get; init; }
   public int Height { get; init; }
}

And in both cases, the object initializer remais the same:

var dimension = new Dimension() { Width = 10, Height = 10 };

## [Deconstructing](https://docs.microsoft.com/en-us/dotnet/csharp/deconstruct#deconstructing-user-defined-types)

## [Index e Ranges](https://docs.microsoft.com/en-us/do)

## [Nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#nullable-reference-types)

## [Asynchronous streams](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#asynchronous-streams)

## [Record types](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#record-types)
