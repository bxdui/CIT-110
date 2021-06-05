// Create a program named PaintingEstimate whose Main() method prompts a user for length and width of a room in feet. Create a method that accepts the values and then computes the cost of painting the room, assuming the room is rectangular and has four full walls and 9-foot ceilings. The price of the job is $6 per square foot. Return the price to the Main() method, and display it.

// An example of the program is shown below:

// Enter length of the room in feet >> 30
// Enter width of room in feet >> 16
// Cost of job for 30 X 16 foot room is $4,

using System;

class PaintingEstimate
{
    public static int[] infoArr = new int[2];
    public static string[] messArr = { "width", "length" };

    static void Main()
    {
        int output;

        for (int n = 0; n < infoArr.Length; n++)
        {
            Console.WriteLine($"Enter {messArr[n]} of the room in feet >> ");
            string input = Console.ReadLine();

            while (!Int32.TryParse(input, out output))
            {
                // Console.WriteLine("Please enter a number")
                input = Console.ReadLine();
            }

         infoArr[n] = output;
        }

        double paintingCost = PaintingEstimate.ComputeCost(infoArr[0], infoArr[1]);

        Console.WriteLine($"Cost of job for {infoArr[0]} X {infoArr[1]} foor room is {paintingCost.ToString("C")}");
    }

    public static double ComputeCost(int length, int width)
    {
        const int CeilingHeight = 9;
        const int LaborRate = 6;

        double paintingCost = (((infoArr[0] * 2) + (infoArr[1] * 2)) * CeilingHeight * LaborRate);

        return paintingCost;
    }
}
