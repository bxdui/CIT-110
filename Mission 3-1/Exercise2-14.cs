// Create an enumeration named Planet that holds the names for the eight planets in our solar system, starting with MERCURY equal to 1 and ending with NEPTUNE.

// Write a program named Planets that prompts the user for a numeric position, and display the name of the planet that is in the requested position.

// For example, if 3 is input, the output would be: EARTH is 3 planet(s) from the sun

using System;

class Planets
{
  enum Planet
  {
    Mercury,
    Venus,
    Earth,
    Mars,
    Jupiter,
    Saturn,
    Uranus,
    Neptune
  }
   static void Main()
   {
        string monthIn = Console.ReadLine();

        switch (monthIn)
        {
            case "1":
                Console.WriteLine($"{Planet.Mercury} is {monthIn} planet(s) from the sun");
                break;

            case "2":
                Console.WriteLine($"{Planet.Venus} is {monthIn} planet(s) from the sun");
                break;

            case "3":
                Console.WriteLine($"{Planet.Earth} is {monthIn} planet(s) from the sun");
                break;

            case "4":
                Console.WriteLine($"{Planet.Mars} is {monthIn} planet(s) from the sun");
                break;

            case "5":
                Console.WriteLine($"{Planet.Jupiter} is {monthIn} planet(s) from the sun");
                break;

            case "6":
                Console.WriteLine($"{Planet.Saturn} is {monthIn} planet(s) from the sun");
                break;

            case "7":
                Console.WriteLine($"{Planet.Uranus} is {monthIn} planet(s) from the sun");
                break;

            case "8":
                Console.WriteLine($"{Planet.Neptune} is {monthIn} planet(s) from the sun");
                break;

            default:
                Console.WriteLine("Enter a planet's number (1-8)");
                break;
        }
   }
}
