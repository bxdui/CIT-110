using System;

// Write an application named SumFiveInts that allows the user to enter five integers and displays their sum.

class SumFiveints
{
    static void Main()
    {
        int sum;
        int numQuant;
        int intOutput;

        for (numQuant = 0; numQuant < 5; numQuant++)
        {
            string intInput = Console.ReadLine();

            while (!Int32.TryParse(intInput, out intOutput))
            {
                Console.WriteLine("Please enter an integer");
                intInput = Console.ReadLine();
            }
            
            sum += intOutput;
        }

        Console.WriteLine($"{sum}");
    }
}
