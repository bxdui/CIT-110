using System;

// ------------------------------
// Application: The Conversation
// Author: Steven Winkler
// Description: An application that prompts a conversation with the user about cooking
// Date Created: 5/23/2021
// Date Revised: 5/28/2021
// ------------------------------

class Conversation
{
    static void Main()
    {
        string rating;
        int ratingInt;
        string favoriteDish;
        string favoriteFlavor;
        string spiceFavor;
        string weirdestDish;

        // ------------------------------
        // Format UI
        // ------------------------------
        Console.CursorVisible = false;
        Console.WindowWidth = 600;
        Console.WindowHeight = 300;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Clear();

        // ------------------------------
        // Opening screen
        // ------------------------------

        Console.WriteLine("The Conversation");
        Console.WriteLine("\nPress any key to continue");
        Console.ReadKey();
        Console.Beep();

        // ------------------------------
        // Request user's name
        // ------------------------------

        Console.WriteLine("\nWhat is your name?");
        string userName = Console.ReadLine();
        Console.WriteLine($"\nNice to meet you, {userName}. Let's talk about cooking!");

        // ------------------------------
        // Prompt user's opinion on cooking
        // ------------------------------

        Console.WriteLine("\nHow much do you like cooking on a scale from 1 to 5, 5 being highest?");
        rating = Console.ReadLine();

        // ------------------------------
        // Ensure input is an integer
        // ------------------------------

        while (!int.TryParse(rating, out ratingInt))
        {
            Console.WriteLine("Please enter your rating as a single-digit number.");
            rating = Console.ReadLine();
        }

        // ------------------------------
        // Tailor response to rating
        // ------------------------------

        if (ratingInt <= 2)
        {
            Console.WriteLine($"{ratingInt}/5 = {(double)ratingInt / 5}, ranking you at {100 * (double)ratingInt / 5}%. I dislike cooking at times too, mostly because of the mess it makes.");
            Console.Write("This is the reason I love to ferment food, since it's a simple way to cook without making a mess.");

            // ------------------------------
            // Ask user for their favorite dish
            // ------------------------------

            Console.WriteLine("\n\nWhether or not you enjoy cooking, you must enjoy food. What's your favorite dish?");
            favoriteDish = Console.ReadLine();
        }
        else
        {
            if (ratingInt >= 6)
            {
                Console.WriteLine("You like cooking more than the scale can handle!");
            }
            Console.WriteLine($"{ratingInt}/5 = {(double)ratingInt / 5}, ranking you at {100 * (double)ratingInt / 5}%. I love cooking too, especially making new dishes whether or not it works out.");
            
            // ------------------------------
            // Ask user for their favorite dish
            // ------------------------------

            Console.WriteLine("\nSpeaking of dishes, what's your favorite one to make?");
            favoriteDish = Console.ReadLine();
        }

        if (favoriteDish.ToUpper() is "CURRY")
        {
            Console.WriteLine($"{favoriteDish} is also my favorite to make and eat! Good choice!");
        }
        else
        {
            Console.WriteLine($"{favoriteDish} sounds great! There's a good chance I've never made {favoriteDish} since I'm pretty new to cooking. My favorite to make and eat is curry.");
        }

        // ------------------------------
        // Ask user for their favorite flavor of food
        // ------------------------------

        Console.WriteLine($"\nSo {favoriteDish} is your favorite dish, what's your favorite flavor? Personally, I have a pretty insatiable sweet tooth.");
        favoriteFlavor = Console.ReadLine();

        if (favoriteFlavor.ToUpper() is "SWEET")
        {
            Console.WriteLine($"We share a love for {favoriteFlavor} food! It's really easy to love.");
        }
        else
        {
            Console.WriteLine($"\n{favoriteFlavor} food is delicious! Whatever flavor type is your favorite, there's always really good food that fits that description.");
        }

        Console.WriteLine($"\nSo you like {favoriteDish} and {favoriteFlavor} food. Makes sense!");

        // ------------------------------
        // Ask user if they tolerate spicy foods
        // ------------------------------

        Console.WriteLine($"\nI mentioned earlier that I love curry, but my spice tolerance is pretty low. Y or N: are you able to handle spicy food?");
        spiceFavor = Console.ReadLine();

        if (spiceFavor.ToUpper() is "Y")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"You tolerate spicy food: {true}. I envy your ability to tolerate spicy food, since so much good food is spicy.");
        }
        else if (spiceFavor.ToUpper() is "N")
        {
            Console.WriteLine($"You tolerate spicy food: {false}. Join the club! Whenever I make curry or any other spicy food, I take care to keep the spice managable.");
        }
        else
        {
            Console.WriteLine($"I'm not sure how to interpret {spiceFavor}, but I'll assume you're like me and aren't great with spicy food. Join the club!");
        }

        // ------------------------------
        // Ask user for their weirdest dish
        // ------------------------------

        Console.WriteLine("\nI'm always curious about different styles of cooking and how they manifest themselves in certain dishes. To learn more about different types of food, I want to ask what is the weirdest thing you've eaten?");
        weirdestDish = Console.ReadLine();

        if (weirdestDish.ToUpper() is "PICKLED EGGS")
        {
            Console.WriteLine($"\n{weirdestDish} is about the weirdest I've gone too, which really isn't that crazy when you think about it. I personally love pickled eggs and continue to make them.");
        }
        else
        {
            Console.WriteLine($"While I don't know exactly what {weirdestDish} is, it's probably crazier than pickled eggs, which is probably the weirdest dish I've eaten and tend to love.");
        }

        if (favoriteDish.ToUpper() is "CURRY")
        {
            if (weirdestDish.ToUpper() is "PICKLED EGGS")
            {
                Console.WriteLine("You have great taste if you like curry and have tried pickled eggs!");
            }
        }

        // ------------------------------
        // Wrap up the conversation
        // ------------------------------

        Console.WriteLine("\nWell, that's about all I have. Thanks for talking about cooking and food with me!");
    }
}