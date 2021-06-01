// ************************************
// Title: Flooring Cost Calculator
// Application Type: Console
// Description: Calculates the cost of flooring two floors given the user-input width and length
// Author: Steven Winkler
// Date Created: 6/1/2021
// Last Modified:
// ************************************

using System;

class FlooringCalc
{
    // infoArr will hold the information to be calculated. 3 elements for 2 rooms = 6 elements total
    // The end calculation will refer to idicies of the infoArr array
    public static double[] infoArr = new double[6];

    // greetArr holds the prompts for collecting data. These will be cycled through using a for loop while the user inputs room data
    public static string[] greetArr =
    {
        "Please enter the first room's width in FEET:",
        "Please enter the first room's length in FEET:",
        "Finally, enter the first room's flooring cost per SQUARE FOOT without the $ sign:",
        "Please enter the second room's width in FEET:",
        "Please enter the second room's length in FEET:",
        "Finally, enter the second room's flooring cost per SQUARE FOOT without the $ sign:"
    };

    // indexId will select the index to add our verified data to
    public static int indexId = 0;
    static void Main()
    {
        // Set console colors
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Clear();

        // Prompt welcome screen
        Console.WriteLine("Welcome to the Flooring Cost Calculator!");
        Console.WriteLine("\n\nPress any key to continue");
        Console.ReadKey();
        Console.Clear();

        // Greet user with brief description
        Console.WriteLine("Hello!");
        Console.WriteLine("\nThis application will allow you to calculate flooring cost given the width, length, and material cost of two floors in your home.");
        Console.WriteLine("\n\nPress any key to continue");
        Console.ReadKey();

        // Reset background and text color once application begins
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Clear();

        // Get the user's name and greet
        Console.WriteLine("What is your name? ");
        string userName = Console.ReadLine();
        Console.WriteLine($"\nGood to meet you, {userName}");

        // Ensure the user would like to calculate flooring cost
        Console.WriteLine($"\n{userName}, are you interested in calculating the cost of flooring two rooms in your home? Y or N:");
        string verifyFloorRoom = Console.ReadLine();

        // Ensure valid input
        while (verifyFloorRoom.ToUpper() != "Y" & verifyFloorRoom.ToUpper() != "N")
        {
            Console.WriteLine("Please enter Y or N");
            verifyFloorRoom = Console.ReadLine();
        }

        // Begin collecting data and calculation when confirmed by user
        if (verifyFloorRoom.ToUpper() == "Y")
        {
            Console.WriteLine($"\nGreat! Let's get stated!");
            Console.WriteLine("\n\nPress any key to begin");
            Console.ReadKey();
            Console.Clear();

            // Cycle through the room data prompts
            for (int n = 0; n < 6; n++)
            {
                // When the loop has occured 3 times, the first floor is complete and the user is prompted for the second floor info
                if (n == 3)
                {
                    Console.WriteLine("That's all I need for the first floor, let's move on to the second.");
                }

                // greetArr[n] cycles through the array that holds the messages for the user declared on line 12
                Console.WriteLine($"\n\n{greetArr[n]}");
                string input = Console.ReadLine();

                // Pass the data through the verify method which ensures the input type is correct and saves it to infoArr
                FlooringCalc.Verify(input);
            }

            FlooringCalc.Calculate(infoArr);
            // Once the calculation has been completed, the application ends
            FlooringCalc.Close();
        }
        else
        {
            // If the user answers "N", begin closing the app
            Console.Clear();
            Console.WriteLine("I'm sorry you are not looking to floor your rooms.");
            Console.WriteLine("This app will be unable to help you.");
            Console.WriteLine("\n\nPress any key to continue");
            Console.ReadKey();
            Console.Clear();
            FlooringCalc.Close();
        }
    }

    // Define Verify as a method to take in the user-prompted data, referred to as idIn
    static void Verify(string idIn)
    {
        // Define idOut to track the number that Program.Verify will output
        double idOut = 0;

        // Define firstIt as a boolean to see if this is the loop's first iteration
        bool firstIt = true;

        // Define verifyRedo as "I" for Incorrect (could be any character except C)
        // verifyRedo will allow us to give the user a chance to re-enter information if they made a mistake the first time
        string verifyRedo = "I";

        // Check to see if the user needs to re-enter (will loop at least once by default (verifyRedo is set to "I") to gather info)
        while (verifyRedo.ToUpper() != "C")
        {
            // If this is not the first iteration, allow the user to correct the value 
            if (!firstIt)
            {
                Console.WriteLine("Enter the correct value:");
                idIn = Console.ReadLine();
            }

            // Verify the input can be turned to a double and prompt re-entry if not
            while (!Double.TryParse(idIn, out idOut))
            {
                Console.WriteLine("Please enter a number with no foreign signs or letters (such as $ or ft):");
                idIn = Console.ReadLine();
            }

            // Confirm the value
            Console.WriteLine($"Great, the value is {idOut}.");

            // Prompt optional re-entry
            Console.WriteLine($"\n\nIf {idOut} is correct, please enter C. Otherwise, enter any other character to re-enter:");
            verifyRedo = Console.ReadLine();

            // Set firstIt to false, prompting re-entry if any character but "C" entered in line 77
            firstIt = false;
            Console.Clear();
        }

        // After the value is verified, store it in an array for later access
        infoArr[indexId] = idOut;

        // Increment indexId to select the next index to store to
        indexId++;
    }

    static void Calculate(double[] args)
    {
        // Display data as a table. This will show the dimensions, sq. ft flooring cost, and total flooring cost of each room
        Console.WriteLine(
            $"First Room".PadLeft(20) + "Second Room".PadLeft(20)
            + "\nWidth:".PadRight(10) + $"{infoArr[0]}".PadLeft(10) + $"{infoArr[3]}".PadLeft(20)
            + "\nLength:".PadRight(10) + $"{infoArr[1]}".PadLeft(10) + $"{infoArr[4]}".PadLeft(20)
            + "\nFlooring:".PadRight(10) + $"{infoArr[2].ToString("C")}".PadLeft(10) + $"{infoArr[5].ToString("C")}".PadLeft(20)
            + "\nTotal:".PadRight(10) + $"{(infoArr[0] * infoArr[1] * infoArr[2]).ToString("C")}".PadLeft(10) + $"{(infoArr[3] * infoArr[4] * infoArr[5]).ToString("C")}".PadLeft(20)
            );
        Console.WriteLine("\n\nPress any key to exit");
        Console.ReadKey();
    }

    static void Close()
    {
        // Closing Screen

        // Reset background and text color
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Clear();

        // Prompt closing message and end application
        Console.WriteLine("You have finished using the Flooring Cost Calculator.");
        Console.WriteLine("\n\nHave a great day!");
    }
}
