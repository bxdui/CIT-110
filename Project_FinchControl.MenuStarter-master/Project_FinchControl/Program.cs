// ************************************
// Title: Finch Control
// Application Type: Console
// Description: An application that controls the Finch Robot's LED lights, speakers, and motors
// Author: Steven Winkler
// Date Created: 6/5/2021
// Last Modified: 6/16/2021
// ************************************

using System;
using System.Collections.Generic;
using FinchAPI;

class Program
{
    #region Generic Displays and Functions

    //----------------------------
    // Menu screens and prompts
    //----------------------------

    // Welcomes the user

    static void DisplayWelcomeScreen()
    {
        Console.WriteLine("\n\tWelcome to the Finch Robot Controller");
        Console.WriteLine("\n\tThis program is designed to interface with the Finch Robot and its sensors, motors, speakers, and LED lights.");
        DisplayContinuePrompt();
    }

    // Pauses for user input

    static void DisplayContinuePrompt()
    {
        Console.WriteLine("\n\t\tPress Enter to continue.");
        Console.ReadLine();
    }

    // Program main menu

    static void DisplayMenuScreen()
    {
        Finch finchRobot = new Finch();
        bool quitApplication = false;

        do
        {
            DisplayHeader("\n\t\tMain Menu");

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
                    DisplayContinuePrompt();
                    DisplayConnectFinchRobot(finchRobot);
                    break;

                case "b":
                    Console.WriteLine("\n\tYou chose Talent Show");
                    DisplayContinuePrompt();
                    TalentShowDisplayMenuScreen(finchRobot);
                    break;

                case "c":
                    Console.WriteLine("\n\tYou chose Data Recorder");
                    DisplayContinuePrompt();
                    DataRecorderDisplayMenuScreen(finchRobot);
                    break;

                case "d":
                    Console.WriteLine("\n\tYou chose Alarm System");
                    DisplayContinuePrompt();
                    AlarmSystemDisplayMenuScreen(finchRobot);
                    break;

                case "e":
                    Console.WriteLine("\n\tYou chose User Programming");
                    DisplayContinuePrompt();
                    UserProgrammingDisplayMenuScreen(finchRobot);
                    break;

                case "f":
                    Console.WriteLine("\n\tYou chose to disconnect the Finch Robot");
                    DisplayContinuePrompt();
                    DisplayDisconnectFinchRobot(finchRobot);
                    break;

                case "q":
                    DisplayDisconnectFinchRobot(finchRobot);
                    DisplayClosingScreen();
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine();
                    Console.WriteLine("\tPlease enter a letter that corresponds to a menu item");
                    DisplayMenuScreen();
                    break;
            }

        } while (!quitApplication);
    }

    // Clears console and displays a header based on what is calling

    static void DisplayHeader(string headerText)
    {
        Console.Clear();
        Console.WriteLine($"\n\t{headerText}");
    }

    // Exit message

    static void DisplayClosingScreen()
    {
        Console.Clear();
        Console.WriteLine("Thanks for using the Finch Robot application.");
        DisplayContinuePrompt();
    }

    //----------------------------
    // Connection functions
    //----------------------------

    // Disconnect the Finch Robot

    static bool DisplayDisconnectFinchRobot(Finch finchRobot)
    {
        DisplayHeader("The application will begin to disconnect to the Finch Robot");
        DisplayContinuePrompt();

        // Disconnect robot
        finchRobot.disConnect();

        Console.WriteLine("\tThe Finch Robot has been disconnected");
        DisplayContinuePrompt();
        return true;
    }

    // Connect the Finch Robot

    static bool DisplayConnectFinchRobot(Finch finchRobot)
    {
        DisplayHeader("\n\n\tThe application will begin to connect to the Finch Robot. Make sure the front LED blinks on.");
        DisplayContinuePrompt();

        bool cv = finchRobot.connect();

        if (cv)
        {
            Console.WriteLine("\n\tConnection successful!");
            DisplayContinuePrompt();
            return true;
        }
        else
        {
            Console.WriteLine("\n\tConnection failed. Ensure the Finch is properly plugged into your computer.");
            DisplayContinuePrompt();
            return false;
        }
    }

    #endregion

    #region Talent Show

    //----------------------------
    // Finch talent show functions
    //----------------------------

    // Talent show menu selection

    static void TalentShowDisplayMenuScreen(Finch finchRobot)
    {
        DisplayHeader("Welcome to the Finch Robot talent show");

        Console.WriteLine("\nTalent show menu");

        Console.WriteLine("\ta) Lights and sound");
        Console.WriteLine("\tb) Dance");
        Console.WriteLine("\tc) All of the above");

        Console.Write("\t\t\nEnter Choice:");
        string menuChoice = Console.ReadLine();

        switch (menuChoice.ToLower())
        {
            case "a":
                TalentShowDisplayLightAndSound(finchRobot);
                break;

            case "b":
                TalentShowDisplayDance(finchRobot);
                break;

            case "c":
                TalentShowDisplayMixingItUp(finchRobot);
                break;
        }
    }

    // Light and sound

    static void TalentShowDisplayLightAndSound(Finch finchRobot)
    {
        DisplayHeader("Talent show: light and sound");

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
        DisplayContinuePrompt();

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

            finchRobot.setLED((lightSoundLevel + n1 - n2), n1, n2);
            finchRobot.noteOn(lightSoundLevel + n2);
            finchRobot.wait(250);
        }

        finchRobot.noteOff();
    }

    // Movement

    static void TalentShowDisplayDance(Finch finchRobot)
    {

        DisplayHeader("Talent show: dance");

        Console.WriteLine("\tThe Finch robot will now dance");
        DisplayContinuePrompt();

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
        DisplayHeader("Talent show: dance and lights");

        Console.WriteLine("\tThe Finch robot will now dance and light up");
        DisplayContinuePrompt();

        for (int movementIteration = 0; movementIteration < 6; movementIteration++)
        {
            finchRobot.setLED(movementIteration + 42, movementIteration + 42, movementIteration + 42);
            finchRobot.setMotors(200, 50);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
        }
    }

    #endregion

    #region Data Recorder

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
        DisplayHeader("\n\t\tData Recorder Menu");

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
                DisplayContinuePrompt();
                break;

            default:
                break;
        }
    }

    // Collect the frequency of output collection from user
    static double DataRecorderDisplayGetDataPointFrequency()

    {
        double dataPointFrequency;

        DisplayHeader("What is the desired recording frequency in seconds?");
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

        DisplayHeader("How many data points do you want to collect?");
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
                DisplayContinuePrompt();
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
                DisplayContinuePrompt();
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
            Console.WriteLine($"{n + 1}".PadLeft(20) + $"{Math.Round(infoLst[n], 2)}".PadLeft(20));
        }
    }

    #endregion

    #region Alarm System

    //----------------------------
    // Alarm system function
    //----------------------------
    static void AlarmSystemDisplayMenuScreen(Finch finchRobot)
    {
        // Declare dataType to store user-input data type to collect
        string dataType = "";

        // Declare selectedSensors to store user-input sensors to monitor
        string selectedSensors = "";

        // Declare rangeType to store user-input threshold type (minimum or maximum number)
        string rangeType = "";

        // Declare thresholdValue to set a limit to rangeType
        int thresholdValue = 0;

        // Declare timeToMonitor to store user-input duration of data recording to be converted to seconds
        int timeToMonitor = 0;

        // Declare quitMenu to loop through menu and preserve user-input data
        bool quitMenu = false;

        do
        {
            Console.Clear();
            DisplayHeader("\n\t\tAlarm System Menu");

            // Display menu to user
            Console.WriteLine(
                  "\n\ta) Select Data to Collect"
                + "\n\tb) Select Sensors to Record"
                + "\n\tc) Set Range Type"
                + "\n\td) Set Threshold Value"
                + "\n\te) Set Time to Monitor"
                + "\n\tf) Set Alarm"
                + "\n\tg) Main Menu");

            // Receive user input
            Console.Write("\n\t\tEnter option: ");
            string menuIn = Console.ReadLine();

            // Validate and call functions according to menu selection
            switch (menuIn.ToLower())
            {
                case "a":
                    dataType = AlarmSystemDataType();
                    break;

                case "b":
                    selectedSensors = AlarmSystemSelectSensors(dataType);
                    break;

                case "c":
                    rangeType = AlarmSystemRangeType();
                    break;

                case "d":
                    thresholdValue = AlarmSetThreshold(rangeType, finchRobot);
                    break;

                case "e":
                    timeToMonitor = AlarmSystemSetTIme();
                    break;

                case "f":
                    AlarmSystemSetAlarm(finchRobot, dataType, selectedSensors, rangeType, thresholdValue, timeToMonitor);
                    break;

                case "g":
                    quitMenu = true;
                    break;

                default:
                    Console.WriteLine("\tPlease enter a valid input");
                    break;
            }

        } while (!quitMenu);
    }

    static string AlarmSystemDataType()
    {
        DisplayHeader("\n\tSelect data type to monitor");

        // Collect user-input data type selection
        Console.Write("\n\tWhich data type would you like to monitor?: T (temperature) or L (light)? ");
        string dataType = Console.ReadLine();

        // Validate and assign dataType accordingly
        switch (dataType.ToUpper())
        {
            case "T":
                dataType = "Temperature";
                break;

            case "L":
                dataType = "Light";
                break;

            //case "B":
            //    dataType = "Temperature and light";
            //    break;

            default:
                Console.WriteLine("\tPlease enter a valid input");
                AlarmSystemSelectSensors(dataType);
                break;
        }

        // Repeat input to user
        Console.WriteLine($"\n\t{dataType} will be collected.");
        DisplayContinuePrompt();
        return dataType;
    }

    static string AlarmSystemSelectSensors(string dataType)
    {
        // Specify sensors if collecting light data and redirect if only collecing temperature
        DisplayHeader("\n\tSelect sensor to monitor");

        // Return nothing if user selected only temperature
        if (dataType == "Temperature")
        {
            Console.WriteLine("\n\tTemperature will be read using the single onboard thermometer and so does not need to be specified.");
            DisplayContinuePrompt();
            return "Temperature";
        }

        // Collect user-input sensor selection
        Console.Write("\n\tWhich sensor(s) would you like to monitor?: L (left), R (right), or B (both)? ");
        string selectedSensors = Console.ReadLine();

        // Validate and assign selectedSensors accordingly
        switch (selectedSensors.ToUpper())
        {
            case "L":
                selectedSensors = "Left";
                break;

            case "R":
                selectedSensors = "Right";
                break;

            case "B":
                selectedSensors = "Both";
                break;

            default:
                Console.WriteLine("\tPlease enter a valid input");
                AlarmSystemSelectSensors(dataType);
                break;
        }

        // Repeat input to user
        Console.WriteLine($"\n\t{selectedSensors} sensor(s) will be read.");
        DisplayContinuePrompt();
        return selectedSensors;
    }

    static string AlarmSystemRangeType()
    {
        DisplayHeader("\n\tSet range type");

        // Prompt user for range type
        Console.Write("\n\tEnter range type: 'min' for minimum and 'max' for maximum: ");
        string rangeType = Console.ReadLine();

        // Validate and store response accordingly
        switch (rangeType.ToUpper())
        {
            case "MIN":
                // return value
                rangeType = "Minimum";
                break;

            case "MAX":
                // return value
                rangeType = "Maximum";
                break;

            default:
                Console.WriteLine("\tPlease enter a valid input");
                break;
        }

        // Repeat value to user
        Console.WriteLine($"\n\t{rangeType} value will be set for the threshold.");
        DisplayContinuePrompt();
        return rangeType;
    }

    static int AlarmSetThreshold(string rangeType, Finch finchRobot)
    {
        // Initialize thresholdValue to store user-input min/max light sensor value
        int thresholdValue;

        DisplayHeader("Set threshold value");

        // Display current temperature and light sensor values for reference
        Console.WriteLine($"\n\tTemperature reading in Farenheit: {finchRobot.getTemperature() * 1.8 + 32.0}");
        Console.WriteLine($"\tLeft light sensor value: {finchRobot.getLeftLightSensor()}");
        Console.WriteLine($"\tRight light sensor value: {finchRobot.getRightLightSensor()}");

        // Take input for min/max threshold value
        Console.Write($"\n\tEnter the {rangeType.ToLower()} threshold sensor value: ");
        Int32.TryParse(Console.ReadLine(), out thresholdValue);

        // Repeat value to user
        Console.WriteLine($"{rangeType} value: {thresholdValue}");

        return thresholdValue;
    }

    static int AlarmSystemSetTIme()
    {
        // Initialize timeToMonitor to collect user-input duration of data recording
        int timeToMonitor;

        DisplayHeader("\n\tSet time to monitor");

        // Take input for min/max threshold value
        Console.Write($"\n\tEnter the time to monitor in seconds: ");
        Int32.TryParse(Console.ReadLine(), out timeToMonitor);

        // Repeat value to user
        Console.WriteLine($"\n\tThe robot will record inputs for {timeToMonitor} seconds");

        DisplayContinuePrompt();
        return timeToMonitor;
    }

    static int AlarmSystemRetrieveLightData(Finch finchRobot, string selectedSensors)
    {
        // Collect and store initial left and right sensor inputs
        int[] sensorValues = finchRobot.getLightSensors();
        int currentSensorValue = 0;

        switch (selectedSensors)
        {
            // Select left value
            case "Left":
                currentSensorValue = sensorValues[0];
                break;

            // Select right value
            case "Right":
                currentSensorValue = finchRobot.getRightLightSensor();
                break;

            // Select average value of both sensors
            case "Both":
                currentSensorValue = (sensorValues[0] + sensorValues[1]) / 2;
                break;
        }

        return currentSensorValue;
    }

    static double AlarmSystemRetrieveTemperature(Finch finchRobot)
    {
        double currentTemperatureValue = finchRobot.getTemperature() * 1.8 + 32;
        return currentTemperatureValue;
    }

    static void AlarmSystemRealTimeOutput(string dataType, int currentSensorValue, double currentTemperature, int timeElapsed)
    {
        switch (dataType)
        {
            case "Temperature":
                // Display current temperature on fixed area of screen
                Console.SetCursorPosition(16, 10);
                Console.Write($"Thermometer readout: {currentTemperature}");
                break;

            case "Light":
                // Display current light level on fixed area of screen
                Console.SetCursorPosition(16, 10);
                Console.Write($"Sensor readout: {currentSensorValue}");
                break;

            //case "B":
            //    // Display current light level on fixed area of screen
            //    Console.SetCursorPosition(16, 10);
            //    Console.Write($"Sensor readout: {currentSensorValue}");

            //    // Display current temperature on fixed area of screen
            //    Console.SetCursorPosition(16, 11);
            //    Console.Write($"Thermometer readout: {currentTemperature}");
            //    break;
        }

        // Display elapsed time in seconds on fixed area of screen
        Console.SetCursorPosition(16, 11);
        Console.Write($"Time elapsed: {timeElapsed+1} seconds");
    }

    static void AlarmSystemSoundAlarm(Finch finchRobot, int currentSensorValue, int thresholdValue, double currentTemperature, int timeToMonitor, int messageIndex)
    {
        string[] messageArray = { $"\n\tThe current sensor value of {currentSensorValue} has fallen short of the threshold value ({thresholdValue})",
                                  $"\n\tThe maximum value of {thresholdValue} has been exceeded by the current sensor value ({currentSensorValue})",
                                  $"\n\tThe current temperature of {currentTemperature} degrees has fallen short of the threshold temperature ({thresholdValue} degrees)",
                                  $"\n\tThe maximum temperature of {thresholdValue} degrees has been exceeded by the current temperature ({currentTemperature} degrees)",
                                  $"\n\n\tThe time limit of {timeToMonitor} seconds has been exceeded."
                                };

        // Sound alarm when threshold reached
        finchRobot.noteOn(150);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{messageArray[messageIndex]}");
        Console.ResetColor();
        finchRobot.wait(2000);
        finchRobot.noteOff();
    }

    static void AlarmSystemSetAlarm(Finch finchRobot, string dataType, string selectedSensors, string rangeType, int thresholdValue, int timeToMonitor)
    {
        // Initialize timeElapsed to ensure time limit is reached and thresholdExceeded to check for threshold comparison
        int timeElapsed = 0;
        bool thresholdExceeded = false;
        int currentSensorValue = 0;
        double currentTemperature = 0;

        DisplayHeader("Set alarm");

        // Repeat values to the user
        Console.WriteLine($"\n\tTo collect {dataType.ToLower()} data from the Finch robot,");
        Console.WriteLine($"\t{selectedSensors} sensor(s) to be monitored");
        Console.WriteLine($"\tFor {timeToMonitor} seconds");
        Console.WriteLine($"\tWhile {rangeType.ToLower()} value of {thresholdValue} is satisfied.");

        DisplayContinuePrompt();

        while (timeElapsed < timeToMonitor && !thresholdExceeded)
        {
            switch (dataType)
            {
                case "Temperature":
                    currentTemperature = AlarmSystemRetrieveTemperature(finchRobot);
                    break;

                case "Light":
                    currentSensorValue = AlarmSystemRetrieveLightData(finchRobot, selectedSensors);
                    break;

                //case "B":
                //    currentSensorValue = AlarmSystemRetrieveLightData(finchRobot, selectedSensors);
                //    currentTemperature = AlarmSystemRetrieveTemperature(finchRobot);
                //    break;
            }

            AlarmSystemRealTimeOutput(dataType, currentSensorValue, currentTemperature, timeElapsed);

            if (dataType == "Light")
            {
                switch (rangeType)
                {
                    case "Minimum":
                        if (currentSensorValue < thresholdValue)
                        {
                            // Sound alarm when threshold reached
                            AlarmSystemSoundAlarm(finchRobot, currentSensorValue, thresholdValue, currentTemperature, timeToMonitor, 0);
                            thresholdExceeded = true;
                        }
                        break;

                    case "Maximum":
                        if (currentSensorValue > thresholdValue)
                        {
                            // Sound alarm when threshold reached
                            AlarmSystemSoundAlarm(finchRobot, currentSensorValue, thresholdValue, currentTemperature,timeToMonitor, 1);
                            thresholdExceeded = true;
                        }
                        break;
                }
            }
            else if (dataType == "Temperature")
            {
                switch (rangeType)
                {
                    case "Minimum":
                        if (currentTemperature < thresholdValue)
                        {
                            // Sound alarm when threshold reached
                            AlarmSystemSoundAlarm(finchRobot, currentSensorValue, thresholdValue, currentTemperature, timeToMonitor, 2);
                            thresholdExceeded = true;
                        }
                        break;

                    case "Maximum":
                        if (currentTemperature > thresholdValue)
                        {
                            // Sound alarm when threshold reached
                            AlarmSystemSoundAlarm(finchRobot, currentSensorValue, thresholdValue, currentTemperature, timeToMonitor, 3);
                            thresholdExceeded = true;
                        }
                        break;
                }
            }

            // Increment timeElapsed once a second has passed
            finchRobot.wait(1000);
            timeElapsed++;
        }

        if (timeElapsed == timeToMonitor)
        {
            // Sound alarm when time exhausted
            AlarmSystemSoundAlarm(finchRobot, currentSensorValue, thresholdValue, currentTemperature, timeToMonitor, 4);
        }

        DisplayContinuePrompt();
    }

    //----------------------------
    // User programming function
    //----------------------------

    #endregion

    #region User Programming

    //----------------------------
    // User Programming function
    //----------------------------

    static void UserProgrammingDisplayMenuScreen(Finch finchRobot)
    {
        DisplayHeader("Text from programming display");
        Console.WriteLine("\nThis function is under development");
        DisplayContinuePrompt();
    }

    // The home page of the application. Allows user to select course of action

    #endregion

    //----------------------------
    // Entry point
    //----------------------------

    static void Main()
    {
        Finch finchRobot = new Finch();
        DisplayWelcomeScreen();
        DisplayMenuScreen();
        DisplayClosingScreen();
    }
}
