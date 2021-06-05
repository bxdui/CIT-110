// Write a program named TestScoreList that accepts eight int values representing student test scores.

// Display each of the values along with a message that indicates how far it is from the average.

// An example of how the results should be output is as follows:

// Test # 0:    89 From average:    6
// Test # 1:    78 From average:    -5
// ...

using System;
using System.Linq;

class TestScoreList
{
    public static int[] infoArr = new int[8];
    static void Main()
    {
        TestScoreList.Collect();
        TestScoreList.Calculate();
    }

    static int Collect()
    {
        int scoreOut;
        int avgSum = 0;

        for (int n = 0; n < 8; n++)
        {
            // Console.WriteLine("Enter your test score:");
            string scoreIn = Console.ReadLine();

            while (!Int32.TryParse(scoreIn, out scoreOut))
            {
                // Console.WriteLine("Enter a whole number:");
                scoreIn = Console.ReadLine();
            }

            infoArr[n] = scoreOut;
            avgSum += scoreOut;
        }

        int average = avgSum / 8;
        return average;
    }

    static void Calculate()
    {
        double average = (double)Enumerable.Sum(infoArr) / 8;

        for (int n = 0; n < 8; n++)
        {
            Console.WriteLine($"Test # {n}:" + $"{infoArr[n]}".PadLeft(5) + "\tFrom average:" + $"\t{infoArr[n] - average}".PadLeft(5));
        }
    }
}
