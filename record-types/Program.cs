using record_types;
using System;

var modelS = new Car("Red", "ModelS", 250);
Console.WriteLine(modelS);
var anotherModelS = new Car("Red", "ModelS", 250);
Console.WriteLine(anotherModelS);

Console.WriteLine($"Equal? {modelS == anotherModelS}"); // true

//var blueCar = modelS with { Color = "Blue" };
//Console.WriteLine(blueCar);