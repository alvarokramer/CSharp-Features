using nullable;
using System;

Person person = new Person { Name = "Person1" };

PrintName(person);

person = null;

Person? otherPerson = null;

PrintName(otherPerson);

void PrintName(Person person)
{
    Console.WriteLine(person.Name);
}
