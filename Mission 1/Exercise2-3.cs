using System;

class InchesToCentimeters
{
  const double cmPerIn = 2.54;

  static void Main()
  {
    int inches = 0;
    Console.WriteLine("Enter inches (as a whole number) to convert to centimeters:");
    string inputInches = Console.ReadLine();

    while (!int.TryParse(inputInches, out inches))
    {
      Console.WriteLine("Enter a whole number");
      inputInches = Console.ReadLine();
    }

    Console.WriteLine($"{inches} inches is {inches * cmPerIn} centimeters.");
  }
}
