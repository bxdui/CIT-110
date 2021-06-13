// ************************************
// Title: Finch Control
// Application Type: Console
// Description: An application that controls the Finch Robot's LED lights, speakers, and motors
// Author: Steven Winkler
// Date Created: 6/5/2021
// Last Modified: 6/11/2021
// ************************************

using System;
using System.Collections.Generic;
using System.Collections;
using FinchAPI;

class Program
{
    //----------------------------
    // Menu screens and prompts
    //----------------------------

    // Pauses for user input

    static void DisplayContinuePrompt()
    {
        Console.WriteLine("\n\t\tPress Enter to continue.");
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
        Console.WriteLine("Welcome to the Finch Robot Controller");
        Console.WriteLine("\nThis program is designed to interface with the Finch Robot and its sensors, motors, speakers, and LED lights.");
        Program.DisplayContinuePrompt();
    }

    // Exit message

    static void DisplayClosingScreen()
    {
        Console.Clear();
        Console.WriteLine("Thanks for using the Finch Robot application.");
        Program.DisplayContinuePrompt();
    }

    //----------------------------
    // Functions
    //----------------------------

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
        Program.DisplayHeader("\n\n\tThe application will begin to connect to the Finch Robot. Make sure the front LED blinks on.");
        Program.DisplayContinuePrompt();

        bool cv = finchRobot.connect();

        if (cv)
        {
            Console.WriteLine("\n\tConnection successful!");
            Program.DisplayContinuePrompt();
            return true;
        }
        else
        {
            Console.WriteLine("\n\tConnection failed. Ensure the Finch is properly plugged into your computer.");
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
        string menuChoice = Console.ReadLine();

        switch (menuChoice.ToLower())
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

    //----------------------------
    // Data recorder function
    //----------------------------

    // inputArr declared to hold input values
    public static double[] inputArr = new double[2];

    // Declare infoLst as array with user-input length to hold temp readings
    public static List<double> infoLst = new List<double>();

    static void DataRecorderDisplayMenuScreen(Finch finchRobot)
    {
        Console.Clear();
        Program.DisplayHeader("\n\t\tData Recorder Menu");

        Console.WriteLine(
              "\n\ta) Number of Data Points"
            + "\n\tb) Frequency of Data Points"
            + "\n\tc) Get Data"
            + "\n\td) Show Data"
            + "\n\te) Main Menu");

        Console.Write("\n\t\tEnter option: ");
        string menuIn = Console.ReadLine();

        switch (menuIn.ToLower())
        {
            case "a":
                inputArr[0] = DataRecorderDisplayGetNumberOfDataPoints();
                Console.WriteLine("\nPress any key to return to the Data Recorder Selection Menu");
                Console.ReadLine();
                DataRecorderDisplayMenuScreen(finchRobot);
                break;

            case "b":
                inputArr[1] = DataRecorderDisplayGetDataPointFrequency();
                Console.WriteLine("\nPress any key to return to the Data Recorder Selection Menu");
                Console.ReadLine();
                DataRecorderDisplayMenuScreen(finchRobot);
                break;

            case "c":
                DataRecorderDisplayGetData(finchRobot);
                DataRecorderDisplayMenuScreen(finchRobot);
                break;

            case "d":
                DataRecorderDisplayDataTable();
                Program.DisplayContinuePrompt();
                break;

            default:
                break;
        }
    }

    // Collect the frequency of output collection from user
    static double DataRecorderDisplayGetDataPointFrequency()

    {
        double dataPointFrequency;

        Program.DisplayHeader("What is the desired recording frequency in seconds?");
        string dataPointFrequencyIn = Console.ReadLine();

        while (!Double.TryParse(dataPointFrequencyIn, out dataPointFrequency))
        {
            Console.WriteLine("Please enter the value as a number without units");
            dataPointFrequencyIn = Console.ReadLine();
        }

        Console.WriteLine($"\nReadings will occur every {dataPointFrequency} seconds");

        return dataPointFrequency;
    }

    // Collect the number of outputs from the user
    static int DataRecorderDisplayGetNumberOfDataPoints()
    {
        int numberOfDataPoints;

        Program.DisplayHeader("How many data points do you want to collect?");
        string numberOfDataPointsIn = Console.ReadLine();

        while (!Int32.TryParse(numberOfDataPointsIn, out numberOfDataPoints))
        {
            Console.WriteLine("Please enter the value as a whole number without units");
            numberOfDataPointsIn = Console.ReadLine();
        }

        Console.WriteLine($"\nWe will collect {numberOfDataPoints} data points.");

        return numberOfDataPoints;
    }

    // Convert temperature readings from celcius to farenheit
    static double ConvertCelciusToFarenheit(double tempReading)
    {
        double tempReadingFarenheit = tempReading * 1.8 + 32;
        return tempReadingFarenheit;
    }

    // Collect data from the Finch Robot
    static void DataRecorderDisplayGetData(Finch finchRobot)
    {
        Console.Clear();
        Console.WriteLine($"{inputArr[0]} measurements will be taken at {inputArr[1]} second intervals.");
        Console.WriteLine("\n\tWhich type of data would you like to collect?");
        Console.WriteLine("\n\ta) Temperature");
        Console.WriteLine("\tb) Light Sensor");

        Console.Write("\n\t\tEnter your choice: ");
        string dataTypeIn = Console.ReadLine();

        switch (dataTypeIn.ToLower())
        {
            case "a":

                // Clear infoLst of the previously collected data and collect temperature from Finch Robot
                infoLst.Clear();
                Console.WriteLine("\n\t\tReading".PadLeft(20) + "Temperature".PadLeft(20));
                Console.WriteLine("\t----------------------------".PadLeft(40));

                // Collect the amount of data points requested by user
                for (int index = 0; index < inputArr[0]; index++)
                {
                    double tempReading = finchRobot.getTemperature();
                    double tempReadingFarenheit = ConvertCelciusToFarenheit(tempReading);
                    infoLst.Add(tempReadingFarenheit);
                    finchRobot.wait((int)(inputArr[1]) * 1000);
                    Console.WriteLine($"{index + 1}".PadLeft(20) + $"{Math.Round(infoLst[index], 2)}".PadLeft(20));
                }

                Console.WriteLine("\n\t\tData collection complete.");
                Program.DisplayContinuePrompt();
                break;

            case "b":
                // Clear infoLst of the previously collected data and collect average light info from Finch Robot
                infoLst.Clear();

                Console.WriteLine("\n\t\tReading".PadLeft(20) + "Light Info".PadLeft(20));
                Console.WriteLine("\t----------------------------".PadLeft(40));

                // Collect the amount of data points requested by user
                for (int index = 0; index < inputArr[0]; index++)
                {
                    int lightLeft = finchRobot.getLeftLightSensor();
                    int lightRight = finchRobot.getRightLightSensor();
                    int avgLight = (lightLeft + lightRight) / 2;
                    infoLst.Add((double)avgLight);
                    finchRobot.wait((int)(inputArr[1]) * 1000);
                    Console.WriteLine($"{index + 1}".PadLeft(20) + $"{Math.Round(infoLst[index], 2)}".PadLeft(20));
                }

                Console.WriteLine("\n\t\tData collection complete.");
                Program.DisplayContinuePrompt();
                break;

            default:
                break;
        }

        Console.Clear();
        Console.WriteLine($"\t\t\nRecording {inputArr[0]} measurements at {inputArr[1]} second intervals...");
    } 

    static void DataRecorderDisplayDataTable()
    {
        Console.Clear();
        Console.WriteLine("Reading".PadLeft(20) + "Output".PadLeft(20));
        Console.WriteLine("----------------------------".PadLeft(40));

        for (int n = 0; n < infoLst.Count; n++)
        {
            Console.WriteLine($"{n+1}".PadLeft(20) + $"{Math.Round(infoLst[n], 2)}".PadLeft(20));
        }
    }

    //----------------------------
    // Alarm system function
    //----------------------------


    static void AlarmSystemDisplayMenuScreen(Finch finchRobot)
    {
        Program.DisplayHeader("Text from alarm system display");
        Console.WriteLine("\nThis function is under development");
        Program.DisplayContinuePrompt();
    }

    //----------------------------
    // User programming function
    //----------------------------

    static void UserProgrammingDisplayMenuScreen(Finch finchRobot)
    {
        Program.DisplayHeader("Text from programming display");
        Console.WriteLine("\nThis function is under development");
        Program.DisplayContinuePrompt();
    }

    // The home page of the application. Allows user to select course of action

    static void DisplayMenuScreen()
    {
        Finch finchRobot = new Finch();
        bool quitApplication = false;

        do
        {
            Program.DisplayHeader("\n\t\tMain Menu");

            // Inform the user connection is needed for Finch functions
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tFirst connecting to the Finch Robot is necesarry for all below functions!");
            Console.ResetColor();

            Console.WriteLine("\n\ta) Connect Finch Robot");
            Console.WriteLine("\tb) Talent Show");
            Console.WriteLine("\tc) Data Recorder");
            Console.WriteLine("\td) Alarm System");
            Console.WriteLine("\te) User Programming");
            Console.WriteLine("\tf) Disconnect Finch Robot");
            Console.WriteLine("\tq) Quit");
            Console.Write("\n\t\tEnter Choice: ");
            string menuChoice = Console.ReadLine().ToLower();

            switch (menuChoice)
                {
                    case "a":
                        Console.WriteLine("\n\tYou chose to connect the Finch Robot");
                        Program.DisplayContinuePrompt();
                        Program.DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        Console.WriteLine("\n\tYou chose Talent Show");
                        Program.DisplayContinuePrompt();
                        Program.TalentShowDisplayMenuScreen(finchRobot);
                        break;

                    case "c":
                        Console.WriteLine("\n\tYou chose Data Recorder");
                        Program.DisplayContinuePrompt();
                        Program.DataRecorderDisplayMenuScreen(finchRobot);
                        break;

                    case "d":
                        Console.WriteLine("\n\tYou chose Alarm System");
                        Program.DisplayContinuePrompt();
                        Program.AlarmSystemDisplayMenuScreen(finchRobot);
                        break;

                    case "e":
                        Console.WriteLine("\n\tYou chose User Programming");
                        Program.DisplayContinuePrompt();
                        Program.UserProgrammingDisplayMenuScreen(finchRobot);
                        break;

                    case "f":
                        Console.WriteLine("\n\tYou chose to disconnect the Finch Robot");
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
            int lightSoundLevel;

            while (!Int32.TryParse(lightSoundLevelIn, out lightSoundLevel))
            {
                Console.WriteLine("Enter a valid number");
                lightSoundLevelIn = Console.ReadLine();
            }

            Console.WriteLine($"You entered {lightSoundLevel}");

            Console.WriteLine("\tThe Finch robot will now put on a light show");
            Program.DisplayContinuePrompt();

            // Prevent inducing hearing loss if lightSoundLevel is irresponsibly high
            if (lightSoundLevel > 255)
            {
                lightSoundLevel = 100;
            }

            Random rng = new Random();

            for (int i = 0; i < 25; i++)
            {
                int n1 = rng.Next(1, 255);
                int n2 = rng.Next(1, 255);
               
                finchRobot.setLED((lightSoundLevel+n1-n2), n1, n2);
                finchRobot.noteOn(lightSoundLevel+n2);
                finchRobot.wait(250);
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
