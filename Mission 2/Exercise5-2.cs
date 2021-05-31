using System;

// Write an application named SumInts that allows the user to enter any number of integers continuously until the user enters 999. Display the sum of the values entered, not including 999.

class SumInts
{
   static void Main()
    {
        int sum = 0;
        int intOutput = 0;

        while (intOutput != 999)
        {
            sum += intOutput;
            string intInput = Console.ReadLine();

            while (!Int32.TryParse(intInput, out intOutput))
            {
                Console.WriteLine("Please enter an integer");
                intInput = Console.ReadLine();
            }
        }

        Console.WriteLine($"{sum}");
    }
}
