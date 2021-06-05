// Write a program for The Carefree Resort named ResortPrices that prompts the user to enter the number of days for a resort stay. Then display the price per night and the total price. Nightly rates are:

// $200 for one or two nights
// $180 for three or four nights
// $160 for five, six, or seven nights
// $145 for eight nights or more
// For example, if the input is 1, the output should be:

// Price per night is $200.00
// Total for 1 night(s) is $200.00

using System;

class ResortPrices
{
    static void Main()
    {
        Console.WriteLine("How many nights will you stay?");
        string nightIn = Console.ReadLine();
        int nightOut = 0;

        while (!Int32.TryParse(nightIn, out nightOut))
        {
            Console.WriteLine("Please enter a whole number");
            nightIn = Console.ReadLine();
        }

        switch (nightOut)
        {
            case 1:
            case 2:
                Console.WriteLine("Price per night is $200.00");
                Console.WriteLine($"Total for {nightIn} night(s) is {(nightOut * 200).ToString("C")}");
                break;

            case 3:
            case 4:
                Console.WriteLine("Price per night is $180.00");
                Console.WriteLine($"Total for {nightIn} night(s) is {(nightOut * 180).ToString("C")}");
                break;

            case 5:
            case 6:
                Console.WriteLine("Price per night is $160.00");
                Console.WriteLine($"Total for {nightIn} night(s) is {(nightOut * 160).ToString("C")}");
                break;

            case 8:
                Console.WriteLine("Price per night is $145.00");
                Console.WriteLine($"Total for {nightIn} night(s) is {(nightOut*145).ToString("C")}");
                break;

            default:
                if (Int32.Parse(nightIn) > 8)
                {
                    Console.WriteLine("Price per night is $145.00");
                    Console.WriteLine($"Total for {nightIn} nights is {(nightOut * 145).ToString("C")}");
                }
                else
                {
                    Console.WriteLine("Invalid number of nights");
                }
                break;
        }
    }
}