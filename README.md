# C# Features from the latest versions

C# 9 was recently released with new great features and shows once again the fast evolution of the programming language.
In this article we present a curated list of new and not so new features added to the C# over the years.

## [Switch Expressions](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression)

Switch Expressions is a feature added in C# 8 that provides a concise way to create switch like statements. It's no longer necessary the use of the `case` and `break` keyworks and the result is a more pleasant sintax.

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

interface IVehicle {}

class Car : IVehicle
{
    public float Weight { get; set; }
}

class Motorcycle : IVehicle {}
```

## [Tuple](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-tuples)

The Tuple type is a C# feature that provides a good syntax to group multiple data elements or when you want to have a data structure containing the properties of an object but without having to create the object itself.

In the code below, we used the Tuple type to create a simple API filter validation class, with Initial Date, Final Date and Page Number, that applies the correct rules and returns the original data with possible error messages and a boolean indicating whether the validation went right or wrong. This was a good option for a filter without having to use any external libraries like FluentValidation, for a relatively simple API with low complexity.

```c#
public static class ParamsValidator
{
    public static Tuple<bool, string, DateTime?, DateTime?, string> Validate(DateTime? startDate, DateTime? endDate, int page)
    {
        //initiates the tuple with the types it must have
        var result = new Tuple<bool, string, DateTime?, DateTime?>(true, "", startDate, endDate);

        //Validates the date parameters
        if (startDate.HasValue && endDate.HasValue && endDate < startDate)
            result = new Tuple<bool, string, DateTime?, DateTime?>(false, "EndDate should be greater than StartDate.", startDate, endDate);

        if(!startDate.HasValue && !endDate.HasValue)
            result = new Tuple<bool, string, DateTime?, DateTime?>(true, "", DateTime.Now.AddDays(-7), DateTime.Now);

        //Validates the Page Number parameter
        if (page <= 0)
            result = new Tuple<bool, string, DateTime?, DateTime?>(false, "Page number should be greater than zero.", startDate, endDate);

        return result;
    }
}
```

With C# 7 changes, we can refactor our code to a much clearer syntax, by being able to name our variables inside the Tuple. So our code above should look like this: 

```c#
public static class ParamsValidator
{
    public static (bool IsValid, string ErrorMessage, DateTime? StartDate, DateTime? EndDate) Validate(DateTime? startDate, DateTime? endDate, int page)
    {
        var result = (IsValid: true, ErrorMessage: "", StartDate: startDate, EndDate: endDate);

        if (startDate.HasValue && endDate.HasValue && endDate < startDate)
            result = (false, "EndDate should be greater than StartDate.", startDate, endDate);

        if(!startDate.HasValue && !endDate.HasValue)
            result = (true, "", DateTime.Now.AddDays(-7), DateTime.Now);

        if (page <= 0)
            result = (false, "Page number should be greater than zero.", startDate, endDate);

        return result;
    }
}
```

And if we want to access the values inside the new Tuple, we can just do this: 

```c#
var isValid = result.IsValid;
```

## [Deconstructing](https://docs.microsoft.com/en-us/dotnet/csharp/deconstruct#deconstructing-user-defined-types)

The deconstructing is a way of consume tuples. A declaration of deconstructing is the syntax for splitting a value into its parts and assigning those parts individually to other variables. You can do that in one of the following ways:

* Using var for the whole variables declared:

```c#
var (destination, distance) = route;
```

* Using var for each individual variables declared:

```c#
(var destination, var distance) = route;
```

* You can deconstruct into existing variables;

```c#
string destination;
double distance;
(destination, distance) = route;
```

* Explicity declare the type of the variables;

```c#
(string destination, double distance) = route;
```

### [Deconstructing user-defined types](https://docs.microsoft.com/en-us/dotnet/csharp/deconstruct#deconstructing-tuple-elements-with-discards)

C# also provides the possibility to implement one or more Deconstruct methods to manipulate user-defined types. The method returns void, and each value to be deconstructed is indicated by an out parameter in the method signature. For example, the following Deconstruct method of a Route class returns the destination, and distance

```c#
public class Route
{
    public string Destination { get; set; }
    public double Distance { get; set; }
    public DateTime  Interval { get; set; }

    public Route(string destination, double distance, DateTime interval) 
    {
        Destination = destination;
        Distance = distance;
        Interval = interval;
    }

    public void Deconstruct(out string dest, out double dist)
    {
        dest = Destination;
        dist = Distance;
    }

    public void Deconstruct(out string dest, out double dist, out DateTime interval)
    {
        dest = Destination;
        dist = Distance;
        interval = Interval;
    }
}
```

## [Init only setters](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#init-only-setters)

The `init` only concept brings the flexibility for immutable model in C#.
It makes simpler the `readonly` for properties, structs(value types) and indexers once an object has been created.
A `init` setter property should be declared in place of the `set` keyword.

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

In the code below using `init`, the constructor would no longer be necessary using unit only properties:

``` csharp
struct Dimension
{
    public int Width { get; init; }
    public int Height { get; init; }
}
```

Then the object initializer can be used like this:

``` csharp
var dimension = new Dimension { Width = 10, Height = 10 };
```

When a base class has a `init` virtual property, the derived classes overriding it must also have `init`

```c#
class BaseClass
{
    public virtual int BaseClassProperty { get; init; }
}

class DerivedClass1 : BaseClass
{
    public override int BaseClassProperty { get; init; }
}

class DerivedClass2 : BaseClass
{
    // Compilation Error: Property must have init to override
    public override int BaseClassProperty { get; set; }
}
```

## [Index e Ranges](https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/ranges-indexes)

C# 8 introduces two new operators:

- The `^` operator provides a better syntax to access elements counting from the end:

``` csharp
string[] animals = {
    "frog", "cat", "dog", "armadillo", "rabbit", "capybara"
};

Console.WriteLine(animals[^1]); // capybara
Console.WriteLine(animals[animals.Length - 1]); // capybara
Console.WriteLine(animals[^4]); // dog
```

- The range operator `..` can be used to get a subset of a sequence based on the start and end values. Ranges are exclusive, meaning the end isn't included in the range.

``` csharp
string[] animals = {
    "frog", "cat", "dog", "armadillo", "rabbit", "capybara"
};

WriteArray(animals[..]); // frog, cat, dog, armadillo, rabbit, capybara
WriteArray(animals[..4]); // frog, cat, dog, armadillo
WriteArray(animals[1..3]); // cat, dog
WriteArray(animals[1..^2]); // cat, dog, armadillo
WriteArray(animals[^3..]); // armadillo, rabbit, capybara

static void WriteArray(string[] strings) => Console.WriteLine(string.Join(", ", strings));
```

This funcionality relies on the new types `Index` and `Range`:

- `System.Index`: represents a type that can be used to index a collection either from the start or the end.
- `System.Range`: represents a range that has start and end indexes.

To support the operators a type must provide an [indexer](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/) with `Index` and `Range` parameters or have a property named `Length` or `Count` that returns an `int`.

## [Nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#nullable-reference-types)

Firstly it's important to separate two concepts, [nullable value types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types) are available since c# 2 and it's different from what we want to approach. This topic is about [nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#nullable-reference-types), for more information on the two main categories of c# types, check the [Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-types).

The nullable and non-nullable reference types were introduced in c# 8. This feature works based on compiler enforcement rules, that is:

1) When the variable isn't supposed to be null:

    - the compiler enforces the variable to be initialized with a non-null value;

   - it can never be assigned the value null;

   - the compiler doesn't issue any warnings when reference types are dereferenced;

   - the compiler issues warnings if a variable is set to an expression that may be null.

2) On the other hand, to the nullable variables:

   - it may be initialized with null value;

   - can be assigned to null after initialization;

   - the variable may only be dereferenced when the compiler can guarantee that the value isn't null;

   - the compiler doesn't issue any warnings when the variable is initialized to null;

   - the compiler doesn't issue any warnings when the variable is assigned to null;

   - the compiler issues warnings when the variable is dereferenced without null checks.

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

Console.WriteLine("Press any key to get the messages in an yield return fashion.");
Console.ReadLine();

foreach (var message in YieldReturnMessages())
    Console.WriteLine(message);

static IEnumerable<string> YieldReturnMessages()
{
    yield return "Avenue";
    yield return "Code";
    yield return "Rocks!";
}
```

In an `asynchronous streams` fashion it's possible to consume multiple results asynchronously. The only thing you need to do is: declare an `IAsyncEnumerable<>` return to the method, and add an `await` modifier to consume the multiple messages:

``` csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

Console.WriteLine("Press any key to get the messages in an await and yield return fashion.");
Console.ReadLine();

await foreach (var message in AwaitAndYieldReturnMessages())
    Console.WriteLine(message);

static async IAsyncEnumerable<string> AwaitAndYieldReturnMessages()
{
    await Task.Delay(1000);
    yield return "Avenue";
    await Task.Delay(1000);
    yield return "Code";
    await Task.Delay(1000);
    yield return "Rocks!";
}
```

You can find the source code [here](https://github.com/alvarokramer/CSharp-Features).

## [Record types](https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/exploration/records)

C# 9 introduced a new feature named `Record Type`, which is a keyword to make an object immutable and to make it behave like a value type. We have the following record:

```c#
public record Car
{
    string Color { get; }

    string Model { get; }
    
    int Horsepower { get; }
}
```

Its properties are implicitly public, so it's not necessary to write the `public` modifier before the type.
When creating an object from the record, we get a code like the one below:

```c#
var modelS = new Car("Red", "ModelS", 250);
```

Works just like a normal object, but when it's created, we cannot change its properties' values anymore.
If we wanted to create then a new record that is just like the first "Car", but with a different "Color", the new keyword `with` can be used.

```c#
var blueCar = modelS with {Color = "Blue"};
```
