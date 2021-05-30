using System;

// Exercise prompt:
// Write a program named CheckCredit that prompts users to enter a purchase price for an item.
// If the value entered is greater than a credit limit of $8,000, display You have exceeded the credit limit; otherwise, display Approved.

class CheckCredit
{
    static void Main()
    {
        Console.WriteLine("Enter the purchase price:");
        string priceInput = Console.ReadLine();
        int price;

        while (!Int32.TryParse(priceInput, out price))
        {
            Console.WriteLine("Please enter a non-comma integer without $");
            priceInput = Console.ReadLine();
        }


        if (price > 8000)
        {
            Console.WriteLine("You have exceeded the credit limit");
        }
        else
        {
            Console.WriteLine("Approved");
        }
    }
}
