using System;

string[] animals = {
    "frog", "cat", "dog", "armadillo", "rabbit", "capybara"
};

Console.WriteLine(animals[^1]); // capybara
Console.WriteLine(animals[animals.Length - 1]); // capybara
Console.WriteLine(animals[^4]); // dog

WriteArray(animals[..]); // frog, cat, dog, armadillo, rabbit, capybara
WriteArray(animals[..4]); // frog, cat, dog, armadillo
WriteArray(animals[1..3]); // cat, dog
WriteArray(animals[1..^2]); // cat, dog, armadillo
WriteArray(animals[^3..]); // armadillo, rabbit, capybara

static void WriteArray(string[] strings) => Console.WriteLine(string.Join(", ", strings));