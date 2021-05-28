using System;

class InchesToCentimeters
{
  const double cmPerIn = 2.54;

  static void Main()
  {
    int inches = 3;
    Console.WriteLine($"{inches} inches is {inches * cmPerIn} centimeters.");
  }
}