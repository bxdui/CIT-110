// ************************************
// Title: Yelp Retriever
// Application Type: Console
// Description: Use the Yelp Fusion API to retrieve businesses based off of user input parameters.
// Author: Steven Winkler
// Date Created: 7/6/2021
// Last Modified: 7/24/2021
// ************************************

using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

#region JSON Formatting Classes

// Create class Location to mirror JSON response
public class Location
{
    public List<string> display_address { get; set; }
}

// Create class Business to mirror JSON response
public class Business
{
    public string name { get; set; }
    public Location location { get; set; }
    public string display_phone { get; set; }
}

public class Root
{
    public List<Business> businesses { get; set; }
}

#endregion

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
        Console.ReadKey();
        Console.Clear();
    }

    static void WelcomeScreenPrompt()
    {
        Console.WriteLine("\n\tWelcome to the business searcher");
        Console.WriteLine("\n\tThis application will take in your input to find local businesses");
        Console.WriteLine("\n\tBy default, the program will return 20 businesses, and the max search radius is ~24 miles");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("\tIt's important to note that you need to enter location values to get a result");
        Console.WriteLine("\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.ResetColor();
        ContinuePrompt();
    }

    static async Task MenuPromptAsync()
    {
        // Declare parameters as string to hold entered query information
        string parameters = "";
        // Set URL to search
        string url = "https://api.yelp.com/v3/businesses/search?";
        // quitMenu as bool to loop through menu choices
        bool quitMenu = false;

        do
        {
            HeaderPrompt("Main Menu");

            Console.WriteLine("\n\ta) Enter Search Terms");
            Console.WriteLine("\tb) Enter Location");
            Console.WriteLine("\tc) Enter Pricing");
            Console.WriteLine("\td) Enter Result Limit");
            Console.WriteLine("\te) Search for Businesses");
            Console.WriteLine("\tf) Quit");

            Console.Write("\n\t\tEnter Choice: ");
            string menuChoice = Console.ReadLine();

            switch (menuChoice.ToLower())
            {
                case "a":
                    // Enter and append search terms to parameters
                    parameters += InputSearchTerms();
                    break;

                case "b":
                    // Enter and append location to parameters
                    parameters += InputLocation();
                    break;

                case "c":
                    // Enter and append pricing to parameters
                    parameters += InputPricing();
                    break;

                case "d":
                    // Enter and append return limit to parameters
                    parameters += InputLimit();
                    break;

                case "e":
                    try
                    {
                        // Attempt to retrieve based on user-input parameters
                        var response = await GetAsync(url + parameters);
                        FormatAsyncResponse(response);
                        ContinuePrompt();
                    }
                    catch
                    {
                        // If an error returns, display the following message and restart menu
                        Console.Clear();
                        Console.WriteLine($"\n\tUnable to retrieve results.\n\tEither no businesses fit your criteria, or the entered parameters were formatted improperly.");
                        ContinuePrompt();
                        await MenuPromptAsync();
                    }
                    break;

                case "f":
                    quitMenu = true;
                    break;

                default:
                    continue;
            }

        } while (!quitMenu);
    }

    #endregion

    static string InputSearchTerms()
    {
        // Prompt user for search terms and return in request format
        Console.Clear();
        Console.Write("\n\tEnter the basic terms you would like to search (such as food, music, art, etc.): ");
        string termRaw = Console.ReadLine();
        string term = termRaw.Replace(' ', '+');
        return $"term={term}&";
    }

    static string InputPricing()
    {
        // Prompt user for pricing, verify number is between 1-4, and return in request format
        Console.Clear();
        Console.Write("\n\tEnter a number from 1-4 to specify pricing (1 is lowest, 4 is highest): ");
        Int32.TryParse(Console.ReadLine(), out int price);

        while (price >= 5 || price <= 0)
        {
            Console.Clear();
            Console.Write("\n\tPlease enter a value from 1-4 (1 lowest, 4 highest): ");
            Int32.TryParse(Console.ReadLine(), out price);
        }

        return $"price={price}&";
    }

    static string InputLocation()
    {
        // Prompt user for location details and return in request format
        Console.Clear();
        Console.Write("\n\tEnter the location information you would like to search (49686, Traverse City, etc.): ");
        string locationRaw = Console.ReadLine();
        string location = locationRaw.Replace(' ', '+');

        Console.Write("\n\tNext, enter the location radius in miles: ");
        Int32.TryParse(Console.ReadLine(), out int radius);

        // Confirm radius is a positive whole number
        while (radius < 1)
        {
            Console.Clear();
            Console.Write("\n\tEnter a positive value for the search radius: ");
            Int32.TryParse(Console.ReadLine(), out radius);
        }

        // Limit max radius to 24, as Yelp limits to ~24.85 miles
        if (radius > 24)
        {
            radius = 24;
        }

        // Return values. Radius is converted to meters for the API
        return $"location={location}&radius={radius*1609}&";
    }

    static string InputLimit()
    {
        // Enter return limit
        Console.Clear();
        Console.Write("\n\tEnter the max number of businesses to retrieve (max 50): ");
        Int32.TryParse(Console.ReadLine(), out int limit);

        // Ensure limit is a positive whole number under 50. Max limit value is 50
        while (limit > 50 || limit < 1)
        {
            Console.Clear();
            Console.Write("\n\tEnter a positive number lower than 50 for your limit: ");
            Int32.TryParse(Console.ReadLine(), out limit);
        }

        Console.WriteLine($"\n\tLimit entered: {limit}");
        return $"limit={limit}&";
    }

    static async Task<string> GetAsync(string url)
    {
        // Call the Yelp API using set parameters
        Console.Clear();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.Headers.Add("Authorization: Bearer <API_KEY>");

        Console.WriteLine($"\n\tCollecting...");

        // Create and execute the call. Return JSON response
        using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            return await reader.ReadToEndAsync();
        }
    }

    static void FormatAsyncResponse(string response)
    {
        // Grab useful information from JSON response and format accordingly
        Console.Clear();
        Root businessInfo = JsonConvert.DeserializeObject<Root>(response);
        Console.WriteLine("\n\t\tResults:");

        foreach (var item in businessInfo.businesses)
        {
            Console.WriteLine($"\n\tName: {item.name}");
            Console.WriteLine($"\tAddress: {item.location.display_address[0]}");
            Console.WriteLine($"\tPhone: {item.display_phone}");
        }
    }

    static async Task Main()
    {
        // Entry point
        WelcomeScreenPrompt();
        await MenuPromptAsync();
        HeaderPrompt("Thanks for using the business searcher");
        ContinuePrompt();
    }
}
