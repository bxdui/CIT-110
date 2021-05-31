using System;

// Write an application named DailyTemps that continuously prompts a user for a series of daily high temperatures until the user enters a sentinel value of 999.
// Valid temperatures range from -20 through 130 Fahrenheit. When the user enters a valid temperature, add it to a total;
// when the user enters an invalid temperature, display the error message:

// "Valid temperatures range from -20 to 130. Please reenter temperature."

// Before the program ends, display the number of temperatures entered and the average temperature.

class DailyTemps
{
    static void Main()
    {
        int sum = 0;
        int intOutput = 0;
        int totalNums = -1;

        while (intOutput != 999)
        {
            if (intOutput >= -20 & intOutput <= 130)
            {
                totalNums += 1;
                sum += intOutput;
                string intInput = Console.ReadLine();

                while (!Int32.TryParse(intInput, out intOutput))
                {
                    Console.WriteLine("Please enter an integer");
                    intInput = Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Valid temperatures range from -20 to 130. Please reenter temperature.");

                string intInput = Console.ReadLine();

                while (!Int32.TryParse(intInput, out intOutput))
                {
                    Console.WriteLine("Please enter an integer");
                    intInput = Console.ReadLine();
                }
            }
        }
        double avg = (double)sum / totalNums;

        Console.WriteLine($"{totalNums} {avg}");
    }
}