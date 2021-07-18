// ************************************
// Title: Yelp Retriever
// Application Type: Console
// Description: Use the Yelp Fusion API to retrieve businesses based off of user input parameters.
// Author: Steven Winkler
// Date Created: 7/6/2021
// Last Modified: 7/18/2021
// ************************************


using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

class YelpRetriever
{
    #region Screen Prompts
    static void HeaderPrompt(string headerText)
    {
        Console.Clear();
        Console.WriteLine($"\n\t{headerText}");
    }

    static void ContinuePrompt()
    {
        Console.Write("\n\tPress any key to continue");
        Console.ReadLine();
        Console.Clear();
    }

    static void WelcomeScreenPrompt()
    {
        Console.WriteLine("\n\tWelcome to the business searcher");
        Console.WriteLine("\n\tThis application will take in your input to find local businesses based on pricing and location radius");
        ContinuePrompt();
    }

    static void MenuPrompt()
    {
        bool quitMenu = false;

        do
        {
            HeaderPrompt("Main Menu");

            Console.WriteLine("\n\ta) Enter Search Terms");
            Console.WriteLine("\tb) Enter Pricing");
            Console.WriteLine("\tc) Enter Location Radius");
            Console.WriteLine("\td) Search for Business");
            Console.WriteLine("\te) Quit");

            Console.Write("\n\t\tEnter Choice: ");
            string menuChoice = Console.ReadLine();

            switch (menuChoice.ToLower())
            {
                case "a":
                    Console.WriteLine("\n\tYou chose to enter search terms");
                    InputSearchTerms();
                    break;

                case "b":
                    Console.WriteLine("\n\tYou chose to enter pricing");
                    InputPricing();
                    break;

                case "c":
                    Console.WriteLine("\n\tYou chose to enter location radius");
                    InputLocationRadius();
                    break;

                case "d":
                    Console.WriteLine("\n\tYou chose to search for businesses");
                    break;

                case "e":
                    quitMenu = true;
                    break;

                default:
                    continue;
            }

        } while (!quitMenu);
    }

    static string InputSearchTerms()
    {
        return "Search Terms";
    }

    #endregion

    static int InputPricing()
    {
        // Range 1-4. API will return only what is passed in the parameters, so add ability to collect a range (written as each number separated by commas)
        return 1;
    }

    static int InputLocationRadius()
    {
        // Call uses meters, show both values to user after input but use meters for call
        // Max meter value of 40000 (just under 25 miles. If 40000 meters exceeded, search within 24.85 mile radius)
        return 3;
    }

    // Return async

    static async Task<string> GetAsync(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        // request.Headers.Add("Cache-Control", "no-cache");
        request.Headers.Add("Authorization: Bearer <API_KEY>");

        Console.WriteLine("Collecting...");

        using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            return await reader.ReadToEndAsync();
        }
    }

   
    static void Main()
    {
        Console.ReadKey();
        // string url = "http://webcode.me";
        // string baseUrl = "https://api.yelp.com/v3/";
        string url = "https://api.yelp.com/v3/businesses/WavvLdfdP6g8aZTtbBQHTw";
        var content = GetAsync(url);
        string result = content.Result;

        Console.WriteLine(result);
        Console.ReadKey();
        Environment.Exit(0);

        WelcomeScreenPrompt();
        MenuPrompt();

        // Closing screen
        HeaderPrompt("Thanks for using the business searcher");
        ContinuePrompt();
    }
}
