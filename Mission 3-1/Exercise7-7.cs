// Create a program named HurricaneModularized that passes a user’s input wind speed to a method that returns the hurricane category.

// Hurricane categories:
// 1: 74-95 mph
// 2: 96-110 mph
// 3: 111-129 mph
// 4: 130-156 mph
// 5: >156 mph

using System;

class Hurricane
{
    static void Main()
    {
        // Console.WriteLine("Enter the hurricane's wind speed")
        string windSpeedIn = Console.ReadLine();
        int windSpeedOut = 0;

        while (!Int32.TryParse(windSpeedIn, out windSpeedOut))
        {
            // Console.WriteLine("Please enter a whole number without units");
            windSpeedIn = Console.ReadLine();
        }

        int category = Hurricane.GetCategory(windSpeedOut);
        Console.WriteLine($"Category {category}");
    }

    public static int GetCategory(int wind)
    {
        if (wind >= 74 && wind <= 95)
        {
            return 1;
        }
        else if (wind >= 96 && wind <= 110)
        {
            return 2;
        }
        else if (wind >= 111 && wind <= 129)
        {
            return 3;
        }
        else if (wind >= 130 && wind <= 156)
        {
            return 4;
        }
        else if (wind > 156)
        {
            return 5;
        }
        else
        {
            Console.WriteLine("Not a hurricane");
            return 0;
        }
    }
}
