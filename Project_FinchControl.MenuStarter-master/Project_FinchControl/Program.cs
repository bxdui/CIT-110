// ************************************
// Title: Finch Control
// Application Type: Console
// Description: An application that controls the Finch Robot's LED lights, speakers, and motors
// Author: Steven Winkler
// Date Created: 6/5/2021
// Last Modified:
// ************************************

using System;
using FinchAPI;

class Program
{
    //----------------------------
    // Menu screens and prompts
    //----------------------------

    // Pauses for user input

    static void DisplayContinuePrompt()
    {
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }

    // Clears console and displays a header based on what is calling

    static void DisplayHeader(string headerText)
    {
        Console.Clear();
        Console.WriteLine(headerText);
    }

    // Welcomes the user

    static void DisplayWelcomeScreen()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Finch Robot Talent Show");
        Console.WriteLine("\n\nThis program is designed to interface with the Finch Robot to activate LED lights and movement");
    }

    // Exit message

    static void DisplayClosingScreen()
    {
        Console.Clear();
        Console.WriteLine("Thanks for using the Finch Robot application.");
        Program.DisplayContinuePrompt();
    }

    // Disconnect the Finch Robot

    static bool DisplayDisconnectFinchRobot(Finch finchRobot)
    {
        Program.DisplayHeader("The application will begin to disconnect to the Finch Robot");
        Program.DisplayContinuePrompt();

        // Disconnect robot
        finchRobot.disConnect();

        Console.WriteLine("The Finch Robot has been disconnected");
        Program.DisplayContinuePrompt();
        return true;
    }

    // Connect the Finch Robot

    static bool DisplayConnectFinchRobot(Finch finchRobot)
    {
        Program.DisplayHeader("The application will begin to connect to the Finch Robot. Make sure the front LED blinks on");
        Program.DisplayContinuePrompt();

        bool cv = finchRobot.connect();

        if (cv)
        {
            Console.WriteLine("Connection successful");
            Program.DisplayContinuePrompt();
            return true;
        }
        else
        {
            Console.WriteLine("Connection failed. Ensure the Finch is properly plugged into your computer");
            Program.DisplayContinuePrompt();
            return false;
        }

        Program.DisplayContinuePrompt();        
    }

    // Talent show menu selection

    static void TalentShowDisplayMenuScreen(Finch finchRobot)
    {        
        Program.DisplayHeader("Welcome to the Finch Robot talent show");

        Console.WriteLine("\nTalent show menu");

        Console.WriteLine("\ta) Lights and sound");
        Console.WriteLine("\tb) Dance");
        Console.WriteLine("\tc) All of the above!");

        Console.Write("\t\t\nEnter Choice:");
        string menuChoice = Console.ReadLine().ToLower();

        switch (menuChoice)
        {
            case "a":
                Program.TalentShowDisplayLightAndSound(finchRobot);
                break;

            case "b":
                Program.TalentShowDisplayDance(finchRobot);
                break;

            case "c":
                Program.TalentShowDisplayMixingItUp(finchRobot);
                break;
        }
    }

    // Data recorder function

    static void DataRecorderDisplayMenuScreen(Finch finchRobot)
    {
        Program.DisplayHeader("Text from data recorder display");
        Console.WriteLine("\nThis function is under development");
        Program.DisplayContinuePrompt();
    }

    // Alarm system function

    static void AlarmSystemDisplayMenuScreen(Finch finchRobot)
    {
        Program.DisplayHeader("Text from alarm system display");
        Console.WriteLine("\nThis function is under development");
        Program.DisplayContinuePrompt();
    }

    // User programming function

    static void UserProgrammingDisplayMenuScreen(Finch finchRobot)
    {
        Program.DisplayHeader("Text from programming display");
        Console.WriteLine("\nThis function is under development");
        Program.DisplayContinuePrompt();
    }

    // The "home page" off the application. Allows user to select course of action

    static void DisplayMenuScreen()
    {
        Finch finchRobot = new Finch();
        bool quitApplication = false;

        do
        {
            Program.DisplayHeader("Main Menu");

            Console.WriteLine("\ta) Connect Finch Robot");
            Console.WriteLine("\tb) Talent Show (Connect Finch Robot first)");
            Console.WriteLine("\tc) Data Recorder");
            Console.WriteLine("\td) Alarm System");
            Console.WriteLine("\te) User Programming");
            Console.WriteLine("\tf) Disconnect Finch Robot");
            Console.WriteLine("\tq) Quit");
            Console.Write("\t\tEnter Choice:");
            string menuChoice = Console.ReadLine().ToLower();

            switch (menuChoice)
                {
                    case "a":
                        Console.WriteLine("\nYou chose to connect the Finch Robot");
                        Program.DisplayContinuePrompt();
                        Program.DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        Console.WriteLine("\nYou chose talent show");
                        Program.DisplayContinuePrompt();
                        Program.TalentShowDisplayMenuScreen(finchRobot);
                        break;

                    case "c":
                        Console.WriteLine("\nYou chose Data Recorder");
                        Program.DisplayContinuePrompt();
                        Program.DataRecorderDisplayMenuScreen(finchRobot);
                        break;

                    case "d":
                        Console.WriteLine("\nYou chose Alarm System");
                        Program.DisplayContinuePrompt();
                        Program.AlarmSystemDisplayMenuScreen(finchRobot);
                        break;

                    case "e":
                        Console.WriteLine("\nYou chose User Programming");
                        Program.DisplayContinuePrompt();
                        Program.UserProgrammingDisplayMenuScreen(finchRobot);
                        break;

                    case "f":
                        Console.WriteLine("\nYou chose to disconnect the Finch Robot");
                        Program.DisplayContinuePrompt();
                        Program.DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        Program.DisplayDisconnectFinchRobot(finchRobot);
                        Program.DisplayClosingScreen();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter that corresponds to a menu item");
                        menuChoice = Console.ReadLine();
                        break;
                }

            } while (!quitApplication);
        }

    //----------------------------
    // Entry point
    //----------------------------

    static void Main()
        {
            Finch finchRobot = new Finch();
            Program.DisplayWelcomeScreen();
            Program.DisplayMenuScreen();
            Program.DisplayClosingScreen();
        }

    //----------------------------
    // Finch talent show functions
    //----------------------------

    // Light and sound

    static void TalentShowDisplayLightAndSound(Finch finchRobot)
        {
            Program.DisplayHeader("Talent show: light and sound");

            Console.WriteLine("Enter the RGB values you want to start the show at! Enter a number between 0 and 255");
            string lightSoundLevelIn = Console.ReadLine();
            int lightSoundLevel = 0;

            while (!Int32.TryParse(lightSoundLevelIn, out lightSoundLevel))
            {
                Console.WriteLine("Enter a valid number");
                string lightSoungLevelIn = Console.ReadLine();
            }

            Console.WriteLine($"You entered {lightSoundLevel}");

            Console.WriteLine("\tThe Finch robot will now put on a light show");
            Program.DisplayContinuePrompt();

            for (int n = lightSoundLevel; n < 255; n++)
            {
                finchRobot.setLED(n, n, n);
                finchRobot.noteOn(n);
            }

        finchRobot.noteOff();
        }

        // Movement

        static void TalentShowDisplayDance(Finch finchRobot)
        {

            Program.DisplayHeader("Talent show: dance");

            Console.WriteLine("\tThe Finch robot will now dance");
            Program.DisplayContinuePrompt();

            for (int movementIteration = 0; movementIteration < 6; movementIteration++)
            {
                finchRobot.setMotors(200, 50);
                finchRobot.wait(1000);
                finchRobot.setMotors(0, 0);
            }
        }

        // Movement and lights
        static void TalentShowDisplayMixingItUp(Finch finchRobot)
        {
            Program.DisplayHeader("Talent show: dance and lights");

            Console.WriteLine("\tThe Finch robot will now dance and light up");
            Program.DisplayContinuePrompt();

            for (int movementIteration = 0; movementIteration < 6; movementIteration++)
            {
                finchRobot.setLED(movementIteration + 42, movementIteration + 42, movementIteration + 42);
                finchRobot.setMotors(200, 50);
                finchRobot.wait(1000);
                finchRobot.setMotors(0, 0);
            }
        }
}
