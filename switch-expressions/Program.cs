using System;

Console.WriteLine(GetInterestingFact_SwithExpressions());

Console.WriteLine(GetTollPrice(new Car { Weight = 205 }));

string GetInterestingFact_SwithExpressions()
{
    return DateTime.Today.DayOfWeek switch
    {
        DayOfWeek.Monday => "Day of the moon",
        DayOfWeek.Tuesday => "Is derived from Old English for Tiw's day",
        DayOfWeek.Wednesday => "It's named after Odin",
        DayOfWeek.Thursday => "It's named after Thor",
        DayOfWeek.Friday => "Comes from the Old English that means day of Frige",
        _ => "Yey, weekend!"
    };
}

string GetInteresting_FactSwithCase()
{
    string interestingFact;
    switch (DateTime.Today.DayOfWeek)
    {
        case DayOfWeek.Monday:
            interestingFact = "Day of the moon";
            break;
        case DayOfWeek.Tuesday:
            interestingFact = "Is derived from Old English for Tiw's day";
            break;
        case DayOfWeek.Wednesday:
            interestingFact = "It's named after Odin";
            break;
        case DayOfWeek.Thursday:
            interestingFact = "It's named after Thor";
            break;
        case DayOfWeek.Friday:
            interestingFact = "Comes from the Old English that means day of Frige";
            break;
        default:
            interestingFact = "Yey, weekend!";
            break;
    };
    return interestingFact;
}

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

interface IVehicle { }

class Car : IVehicle
{
    public float Weight { get; set; }
}

class Motorcycle : IVehicle { }