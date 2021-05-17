using System.Globalization;
using System;

// Find estimated cost for moving
// Base cost of $200, $150 per mile, $2 per hour
class MoveEstimator
{
    static void Main()
    {
        int cost = 200;
        int hours = 0;
        int miles = 0;

        // Ask for quantity of hours
        // Console.WriteLine("How many hours (whole number)?");
        string inputHours = Console.ReadLine();
        
        while (!int.TryParse(inputHours, out hours))
        {
            Console.WriteLine("Enter a whole number");
            inputHours = Console.ReadLine();
        }

        // Ask for quantity of miles
        // Console.WriteLine("How many miles (whole number)?");
        string inputMiles = Console.ReadLine();

        while (!int.TryParse(inputMiles, out miles))
        {
            Console.WriteLine("Enter a whole number");
            inputMiles = Console.ReadLine();
        }

        // Find total cost
        int total = cost+(hours*150)+(miles*2);

        // Output total cost as type $ using CultureInfo
        Console.WriteLine(total.ToString("C", CultureInfo.GetCultureInfo("en-US")));
    }
}
