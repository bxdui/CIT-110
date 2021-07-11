// ************************************
// Title: Yelp Retriever
// Application Type: Console
// Description: Use the Yelp Fusion API to retrieve businesses based off of user input parameters
// Author: Steven Winkler
// Date Created: 7/6/2021
// Last Modified: 7/11/2021
// ************************************

using System;
using System.IO;
using System.Net;
using System.Net.Http;

class YelpRetriever
{
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
                    YelpGet();
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
   static async System.Threading.Tasks.Task YelpGet()
    {
        // Ask user input for number of businesses to return, pass as limit parameter

        // Decalre URL to use
        // string url = "http://webcode.me";
        string url = "https://api.yelp.com/v3/businesses/search";
         
        // string data = RetrieveCallInfo(url);

        HttpClient client = new HttpClient();
        string content = await client.GetStringAsync(url);

        Console.WriteLine(content);

        //Console.WriteLine(data);
        Console.ReadKey();

        // Also display results. Format as table that displays name, address, phone number, and website
    }

    //static async System.Threading.Tasks.Task<string> RetrieveCallInfoAsync(HttpClient client)
    //{
    //    string content = await client.GetStringAsync("http://webcode.me");

    //    return content;
    //}

    static string RetrieveCallInfo(string url)
    {
        // Declare request as var. WebRequest.Create() method makes a request
        WebRequest request = WebRequest.Create(url);

        // Set request.Method to "GET" (Seemingly not necessary due to use of GetResponse() and GetResponseStream methods inherently retrieving from call)
        // request.Method = "GET";

        // Declare webResponse as var which stores results from request.GetResponse() method. Stores WebResponse type that contains a response to an internet request
        WebResponse webResponse = request.GetResponse();

        // Declare webStream to return the data stream from the internet request
        Stream webStream = webResponse.GetResponseStream();

        // Declare reader as SteamReader object which takes in webStream
        StreamReader reader = new StreamReader(webStream);

        // Declare data as string to store the read webStream variable
        string data = reader.ReadToEnd();

        return data;
    }

    static void Main()
    {
        WelcomeScreenPrompt();
        MenuPrompt();

        // Closing screen
        HeaderPrompt("Thanks for using the business searcher");
        ContinuePrompt();
    }
}
